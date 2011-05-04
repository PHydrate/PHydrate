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
  internal class BehaviorElementHandler : IElementHandler
  {
    readonly BehaviorFactory _behaviorFactory;
    readonly BehaviorSpecificationFactory _behaviorSpecificationFactory;

    public BehaviorElementHandler(BehaviorFactory behaviorFactory,
                                  BehaviorSpecificationFactory behaviorSpecificationFactory)
    {
      _behaviorFactory = behaviorFactory;
      _behaviorSpecificationFactory = behaviorSpecificationFactory;
    }

    public bool Accepts(IElement element)
    {
      IDeclaration declaration = element as IDeclaration;
      if (declaration == null)
      {
        return false;
      }

      return declaration.DeclaredElement.IsBehavior();
    }

    public IEnumerable<UnitTestElementDisposition> AcceptElement(IElement element, IFile file)
    {
      IDeclaration declaration = (IDeclaration)element;
      var behaviorElement = _behaviorFactory.CreateBehavior(declaration.DeclaredElement);

      if (behaviorElement == null)
      {
        yield break;
      }

      yield return new UnitTestElementDisposition(behaviorElement,
                                                  file.ProjectFile,
                                                  declaration.GetNavigationRange().TextRange,
                                                  declaration.GetDocumentRange().TextRange);

      var behaviorSpecifications =
        _behaviorSpecificationFactory.CreateBehaviorSpecificationsFromBehavior(behaviorElement,
                                                                               declaration.DeclaredElement);

      foreach (var behaviorSpecificationElement in behaviorSpecifications)
      {
        yield return new UnitTestElementDisposition(new UnitTestElementLocation[0],
                                                    behaviorSpecificationElement);
      }
    }
  }
}