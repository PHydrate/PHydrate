using System;

namespace PHydrate.Attributes
{
    /// <summary>
    /// Specify a custom IObjectHydrator for the class
    /// </summary>
    [ AttributeUsage( AttributeTargets.Class, AllowMultiple = false, Inherited = false ) ]
    public class ObjectHydratorAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the type of the hydrator.
        /// </summary>
        /// <value>The type of the hydrator.</value>
        public Type HydratorType { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectHydratorAttribute"/> class.
        /// </summary>
        /// <param name="hydratorType">Type of the hydrator.</param>
        public ObjectHydratorAttribute(Type hydratorType)
        {
            if (hydratorType.GetInterface("PHydrate.IObjectHydrator`1"  ) == null)
                throw new PHydrateException( "The type specified as an [ObjectHydrator] does not implement IObjectHydrator<T>" );

            HydratorType = hydratorType;
        }
    }
}