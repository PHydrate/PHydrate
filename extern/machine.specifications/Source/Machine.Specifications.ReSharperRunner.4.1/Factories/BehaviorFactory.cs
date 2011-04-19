<<<<<<< HEAD
using JetBrains.Metadata.Reader.API;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
#if RESHARPER_5
using JetBrains.ReSharper.UnitTestFramework;
#else
using JetBrains.ReSharper.UnitTestExplorer;
#endif

using Machine.Specifications.ReSharperRunner.Presentation;

namespace Machine.Specifications.ReSharperRunner.Factories
{
  internal class BehaviorFactory
  {
    readonly IProjectModelElement _project;
    readonly IUnitTestProvider _provider;
    readonly ContextCache _cache;

    public BehaviorFactory(IUnitTestProvider provider, IProjectModelElement project, ContextCache cache)
    {
      _provider = provider;
      _cache = cache;
      _project = project;
    }

    public BehaviorElement CreateBehavior(IDeclaredElement field)
    {
      IClass clazz = field.GetContainingType() as IClass;
      if (clazz == null)
      {
        return null;
      }

      ContextElement context;
      _cache.Classes.TryGetValue(clazz, out context);
      if (context == null)
      {
        return null;
      }

      return new BehaviorElement(_provider,
                                 context,
                                 _project,
                                 clazz.CLRName,
                                 field.ShortName,
                                 field.IsIgnored());
    }

    public BehaviorElement CreateBehavior(ContextElement context, IMetadataField behavior)
    {
      IMetadataTypeInfo typeContainingBehaviorSpecifications = behavior.GetFirstGenericArgument();

      return new BehaviorElement(_provider,
                                 context,
                                 _project,
                                 behavior.DeclaringType.FullyQualifiedName,
                                 behavior.Name,
                                 behavior.IsIgnored() || typeContainingBehaviorSpecifications.IsIgnored());
    }
  }
}
=======
using JetBrains.Metadata.Reader.API;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
#if RESHARPER_5
using JetBrains.ReSharper.UnitTestFramework;
#else
using JetBrains.ReSharper.UnitTestExplorer;
#endif

using Machine.Specifications.ReSharperRunner.Presentation;

namespace Machine.Specifications.ReSharperRunner.Factories
{
  internal class BehaviorFactory
  {
    readonly IProjectModelElement _project;
    readonly IUnitTestProvider _provider;
    readonly ContextCache _cache;

    public BehaviorFactory(IUnitTestProvider provider, IProjectModelElement project, ContextCache cache)
    {
      _provider = provider;
      _cache = cache;
      _project = project;
    }

    public BehaviorElement CreateBehavior(IDeclaredElement field)
    {
      IClass clazz = field.GetContainingType() as IClass;
      if (clazz == null)
      {
        return null;
      }

      ContextElement context;
      _cache.Classes.TryGetValue(clazz, out context);
      if (context == null)
      {
        return null;
      }

      return new BehaviorElement(_provider,
                                 context,
                                 _project,
                                 clazz.CLRName,
                                 field.ShortName,
                                 field.IsIgnored());
    }

    public BehaviorElement CreateBehavior(ContextElement context, IMetadataField behavior)
    {
      IMetadataTypeInfo typeContainingBehaviorSpecifications = behavior.GetFirstGenericArgument();

      return new BehaviorElement(_provider,
                                 context,
                                 _project,
                                 behavior.DeclaringType.FullyQualifiedName,
                                 behavior.Name,
                                 behavior.IsIgnored() || typeContainingBehaviorSpecifications.IsIgnored());
    }
  }
}
>>>>>>> feature/externs-subtree
