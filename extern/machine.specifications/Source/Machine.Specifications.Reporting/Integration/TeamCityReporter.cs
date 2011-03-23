using System;
using Machine.Specifications.Runner;
using Machine.Specifications.Runner.Impl;

namespace Machine.Specifications.Reporting.Integration
{

  public class TeamCityReporter : ISpecificationRunListener, ISpecificationResultProvider
  {
    readonly TimingRunListener _timingListener;

    readonly TeamCityServiceMessageWriter _writer;
    string _currentAssembly;
    string _currentContext;
    string _currentNamespace;
    bool _failureOccured;
    //string _failures;

    public TeamCityReporter(Action<string> writer, TimingRunListener listener)
    {
      _timingListener = listener;
      _failureOccured = false;
      _writer = new TeamCityServiceMessageWriter(writer);
    }

    protected string GetSpecificationName(SpecificationInfo specification)
    {
      return _currentContext + " > " + specification.Name;
    }

    public void OnAssemblyStart(AssemblyInfo assembly)
    {
      _currentAssembly = assembly.Name;
      _writer.WriteProgressStart("Running specifications in " + assembly.Name);
    }

    public void OnAssemblyEnd(AssemblyInfo assembly)
    {
      if (!string.IsNullOrEmpty(_currentNamespace))
      {
        _writer.WriteTestSuiteFinished(_currentNamespace);
      }
      _writer.WriteProgressFinish("Running specifications in " + assembly.Name);
      _currentAssembly = "";
    }

    public void OnRunStart()
    {
      _writer.WriteProgressStart("Running specifications.");
    }

    public void OnRunEnd()
    {
      _writer.WriteProgressFinish("Running specifications.");
    }

    public void OnContextStart(ContextInfo context)
    {
      if (context.Namespace != _currentNamespace)
      {
        if (!string.IsNullOrEmpty(_currentNamespace))
        {
          _writer.WriteTestSuiteFinished(_currentNamespace);
        }
        _currentNamespace = context.Namespace;
        _writer.WriteTestSuiteStarted(_currentNamespace);
      }
      _currentContext = context.FullName;
    }

    public void OnContextEnd(ContextInfo context)
    {
      _currentContext = "";
    }

    public void OnSpecificationStart(SpecificationInfo specification)
    {
      _writer.WriteTestStarted(GetSpecificationName(specification), false);
    }

    public void OnSpecificationEnd(SpecificationInfo specification, Result result)
    {
      switch (result.Status)
      {
        case Status.Passing:
          break;
        case Status.NotImplemented:
          _writer.WriteTestIgnored(GetSpecificationName(specification), "(Not Implemented)");
          break;
        case Status.Ignored:
          _writer.WriteTestIgnored(GetSpecificationName(specification), "(Ignored)");
          break;
        default:
          if (result.Exception != null)
          {
            _writer.WriteTestFailed(GetSpecificationName(specification), 
             result.Exception.Message, result.Exception.ToString());
          }
          else
          {
            _writer.WriteTestFailed(GetSpecificationName(specification), "FAIL", "");
          }
          _failureOccured = true;
          break;
      }
      var duration = TimeSpan.FromMilliseconds(_timingListener.GetSpecificationTime(specification));

      _writer.WriteTestFinished(GetSpecificationName(specification), duration);
    }

    public void OnFatalError(ExceptionResult exception)
    {
      _writer.WriteError(exception.Message, exception.ToString());
      _failureOccured = true;
    }

    public bool FailureOccured
    {
      get { return _failureOccured; }
    }
  }
}