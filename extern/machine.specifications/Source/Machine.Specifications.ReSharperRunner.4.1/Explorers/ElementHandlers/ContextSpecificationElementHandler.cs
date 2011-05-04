using System.Collections.Generic;

using JetBrains.ReSharper.Psi.Tree;
#if RESHARPER_5
using JetBrains.ReSharper.UnitTestFramework;
#else
using JetBrains.ReSharper.UnitTestExplorer;
#endif

using Machine.Specifications.ReSharperRunner.Factories;

namespace Machine.Specifications.ReSharperRunner.Explorers.ElementHandlers
{
  internal class ContextSpecificationElementHandler : IElementHandler
  {
    readonly ContextSpecificationFactory _contextSpecificationFactory;

    public ContextSpecificationElementHandler(ContextSpecificationFactory contextSpecificationFactory)
    {
      _contextSpecificationFactory = contextSpecificationFactory;
    }

    public bool Accepts(IElement element)
    {
      IDeclaration declaration = element as IDeclaration;
      if (declaration == null)
      {
        return false;
      }

      return declaration.DeclaredElement.IsSpecification();
    }

    public IEnumerable<UnitTestElementDisposition> AcceptElement(IElement element, IFile file)
    {
      IDeclaration declaration = (IDeclaration)element;
      var contextSpecificationElement =
        _contextSpecificationFactory.CreateContextSpecification(declaration.DeclaredElement);

      if (contextSpecificationElement == null)
      {
        yield break;
      }

      yield return new UnitTestElementDisposition(contextSpecificationElement,
                                                  file.ProjectFile,
                                                  declaration.GetNavigationRange().TextRange,
                                                  declaration.GetDocumentRange().TextRange);
    }
  }
}