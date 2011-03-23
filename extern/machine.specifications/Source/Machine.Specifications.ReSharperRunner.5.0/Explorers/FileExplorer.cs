using System;
using System.Collections.Generic;
using System.Linq;

using JetBrains.Application;
using JetBrains.Application.Progress;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.UnitTestFramework;

using Machine.Specifications.ReSharperRunner.Explorers.ElementHandlers;
using Machine.Specifications.ReSharperRunner.Factories;

namespace Machine.Specifications.ReSharperRunner.Explorers
{
  internal class FileExplorer : IRecursiveElementProcessor
  {
    readonly UnitTestElementLocationConsumer _consumer;
    readonly IEnumerable<IElementHandler> _elementHandlers;
    readonly IFile _file;
    readonly CheckForInterrupt _interrupted;

    public FileExplorer(IUnitTestProvider provider,
                        UnitTestElementLocationConsumer consumer,
                        IFile file,
                        CheckForInterrupt interrupted)
    {
      if (file == null)
      {
        throw new ArgumentNullException("file");
      }

      if (provider == null)
      {
        throw new ArgumentNullException("provider");
      }

      _consumer = consumer;
      _file = file;
      _interrupted = interrupted;

      IProject project = file.ProjectFile.GetProject();
      var projectEnvoy = new ProjectModelElementEnvoy(project);
      string assemblyPath = UnitTestManager.GetOutputAssemblyPath(project).FullPath;

      var cache = new ContextCache();
      var contextFactory = new ContextFactory(provider, projectEnvoy, assemblyPath, cache);
      var contextSpecificationFactory = new ContextSpecificationFactory(provider, projectEnvoy, cache);
      var behaviorFactory = new BehaviorFactory(provider, projectEnvoy, cache);
      var behaviorSpecificationFactory = new BehaviorSpecificationFactory(provider, projectEnvoy);

      _elementHandlers = new List<IElementHandler>
                         {
                           new ContextElementHandler(contextFactory),
                           new ContextSpecificationElementHandler(contextSpecificationFactory),
                           new BehaviorElementHandler(behaviorFactory, behaviorSpecificationFactory)
                         };
    }

    public bool InteriorShouldBeProcessed(IElement element)
    {
      if (element is ITypeMemberDeclaration)
      {
        return element is ITypeDeclaration;
      }

      return true;
    }

    public void ProcessBeforeInterior(IElement element)
    {
      IElementHandler handler = _elementHandlers.Where(x => x.Accepts(element)).FirstOrDefault();
      if (handler == null)
      {
        return;
      }

      foreach (var elementDisposition in handler.AcceptElement(element, _file))
      {
        if (elementDisposition != null && elementDisposition.UnitTestElement != null)
        {
          _consumer(elementDisposition);
        }
      }
    }

    public void ProcessAfterInterior(IElement element)
    {
    }

    public bool ProcessingIsFinished
    {
      get
      {
        if (_interrupted())
        {
          throw new ProcessCancelledException();
        }

        return false;
      }
    }
  }
}
