using JetBrains.Metadata.Reader.API;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.UnitTestFramework;

using Machine.Specifications.ReSharperRunner.Presentation;

namespace Machine.Specifications.ReSharperRunner.Factories
{
  internal class ContextSpecificationFactory
  {
    readonly ProjectModelElementEnvoy _projectEnvoy;
    readonly IUnitTestProvider _provider;
    readonly ContextCache _cache;

    public ContextSpecificationFactory(IUnitTestProvider provider, ProjectModelElementEnvoy projectEnvoy, ContextCache cache)
    {
      _provider = provider;
      _cache = cache;
      _projectEnvoy = projectEnvoy;
    }

    public ContextSpecificationElement CreateContextSpecification(IDeclaredElement field)
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

      return new ContextSpecificationElement(_provider,
                                             context,
                                             _projectEnvoy,
                                             clazz.CLRName,
                                             field.ShortName,
                                             clazz.GetTags(),
                                             field.IsIgnored());
    }

    public ContextSpecificationElement CreateContextSpecification(ContextElement context, IMetadataField specification)
    {
      return new ContextSpecificationElement(_provider,
                                             context,
                                             _projectEnvoy,
                                             specification.DeclaringType.FullyQualifiedName,
                                             specification.Name,
                                             specification.DeclaringType.GetTags(),
                                             specification.IsIgnored());
    }
  }
}
