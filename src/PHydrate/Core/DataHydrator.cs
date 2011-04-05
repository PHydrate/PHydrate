using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using PHydrate.Attributes;
using PHydrate.Util;

namespace PHydrate.Core
{
    public partial class Session
    {
        private class DataHydrator<T> where T : class
        {
            private readonly IDefaultObjectHydrator _defaultObjectHydrator;
            private readonly WeakReferenceObjectCache _hydratedObjects;

            public DataHydrator(IDefaultObjectHydrator defaultObjectHydrator, WeakReferenceObjectCache hydratedObjects)
            {
                _defaultObjectHydrator = defaultObjectHydrator;
                _hydratedObjects = hydratedObjects;
            }

            public IEnumerable<T> HydrateFromDataReader(IDataReader dataReader)
            {
                IEnumerable<IMemberInfo> internalRecordsets =
                    typeof(T).GetMembersWithAttribute<RecordsetAttribute>();
                return internalRecordsets.Any()
                           ? HydrateRecordsetWithInternals(dataReader, internalRecordsets)
                           : HydrateRecordset(dataReader);
            }

            /// <exception cref="PHydrateException">Missing expected recordset from stored procedure</exception>
            [NotNull]
            private IEnumerable<T> HydrateRecordsetWithInternals(IDataReader dataReader,
                                                                    IEnumerable<IMemberInfo> internalRecordsets)
            {
                IDictionary<int, T> aggregateRoot =
                    HydrateRecordset(dataReader).ToDictionary(x => x.GetObjectsHashCodeByPrimaryKeys());

                foreach (IMemberInfo internalRecordset in internalRecordsets)
                {
                    if (!dataReader.NextResult())
                        throw new PHydrateException("Missing expected recordset from stored procedure");

                    IEnumerable enumerable =
                        internalRecordset.Type.ExecuteGenericMethod<DataHydrator<T>, IEnumerable>(
                            x => x.HydrateFromDataReader(dataReader),
                            _defaultObjectHydrator,
                            _hydratedObjects
                            );

                    object obj = enumerable.Cast<object>().FirstOrDefault();
                    if (obj == null)
                        continue;

                    Type typeToCastTo = obj.GetType();
                    if (internalRecordset.Type.IsAssignableFrom(typeToCastTo)) // Simple type
                        SetSimpleTypeInAggregateRoot(internalRecordset, obj, aggregateRoot);
                    else if (
                        internalRecordset.Type.IsAssignableFrom(
                            typeof(IEnumerable<>).MakeGenericType(typeToCastTo))) // IEnumerable, IList
                    {
                        T found = GetAggregateRootFromSecondaryObject(internalRecordset, obj, aggregateRoot);
                        if (found == null)
                            continue;

                        var list = internalRecordset.GetEnumerableOrList(enumerable, typeToCastTo);
                        internalRecordset.SetValue(found, list);
                    }
                }
                return aggregateRoot.Values;
            }

            private static T GetAggregateRootFromSecondaryObject(IMemberInfo internalRecordset, object obj,
                                                                  IDictionary<int, T> aggregateRoot)
            {
                string[] primaryKeyMembers =
                    internalRecordset.Type.GetMembersWithAttribute<PrimaryKeyAttribute>().Select(
                        x => x.Wrapped.Name).ToArray();

                int lookupHash = internalRecordset.GetLookupHash<T>(obj, primaryKeyMembers);

                return aggregateRoot.ContainsKey(lookupHash) ? aggregateRoot[lookupHash] : null;
            }

            private static void SetSimpleTypeInAggregateRoot(IMemberInfo internalRecordset, object obj,
                                                              IDictionary<int, T> aggregateRoot)
            {
                int objectHash = obj.GetObjectsHashCodeByPrimaryKeys();

                // Find the member(s) in aggregateRoot that contains this member
                foreach (
                    T o in
                        aggregateRoot.Values.AsEnumerable().Where(
                            x =>
                            internalRecordset.GetValue(x) != null &&
                            internalRecordset.GetValue(x).GetObjectsHashCodeByPrimaryKeys() == objectHash))
                    internalRecordset.SetValue(o, obj);
            }

            private IEnumerable<T> HydrateRecordset(IDataReader dataReader)
            {
                Func<IDictionary<string, object>, T> hydratorFunction = GetHydratorFunction();

                while (dataReader.Read())
                {
                    T hydratedObject = hydratorFunction(dataReader.ToDictionary());
                    yield return GetHydratedObjectFromCache(hydratedObject);
                }
            }

            [NotNull]
            private Func<IDictionary<string, object>, T> GetHydratorFunction()
            {
                IObjectHydrator<T> hydrator = GetHydrator();
                return hydrator == null
                           ? (Func<IDictionary<string, object>, T>)
                             (x => _defaultObjectHydrator.Hydrate<T>(x))
                           : (hydrator.Hydrate);
            }

            [CanBeNull]
            private static IObjectHydrator<T> GetHydrator()
            {
                var objectHydratorAttribute = typeof(T).GetAttribute<ObjectHydratorAttribute>();
                return objectHydratorAttribute == null
                           ? null
                           : objectHydratorAttribute.HydratorType.ConstructUsingDefaultConstructor
                                 <IObjectHydrator<T>>();
            }

            private T GetHydratedObjectFromCache(T hydratedObject)
            {
                if (_hydratedObjects.Contains(hydratedObject))
                    return
                        (_hydratedObjects[hydratedObject].Target ??
                          (_hydratedObjects[hydratedObject].Target = hydratedObject)) as T;

                _hydratedObjects.Add(hydratedObject);
                return hydratedObject;
            }
        }
    }
}