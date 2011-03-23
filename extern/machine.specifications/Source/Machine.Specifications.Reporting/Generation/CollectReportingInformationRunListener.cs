﻿using System.Collections.Generic;

using Machine.Specifications.Runner;

namespace Machine.Specifications.Reporting.Generation
{
  public class CollectReportingInformationRunListener : ISpecificationRunListener
  {
    string _currentAssemblyName;
    ContextInfo _currentContext;
    readonly Dictionary<string, List<ContextInfo>> _contextsByAssembly;
    readonly Dictionary<ContextInfo, List<SpecificationInfo>> _specificationsByContext;
    readonly Dictionary<SpecificationInfo, Result> _resultsBySpecification;

    public CollectReportingInformationRunListener()
    {
      _currentAssemblyName = "";
      _currentContext = null;
      _contextsByAssembly = new Dictionary<string, List<ContextInfo>>();
      _specificationsByContext = new Dictionary<ContextInfo, List<SpecificationInfo>>();
      _resultsBySpecification = new Dictionary<SpecificationInfo, Result>();
    }

    public Dictionary<SpecificationInfo, Result> ResultsBySpecification
    {
      get { return _resultsBySpecification; }
    }

    public Dictionary<ContextInfo, List<SpecificationInfo>> SpecificationsByContext
    {
      get { return _specificationsByContext; }
    }

    public Dictionary<string, List<ContextInfo>> ContextsByAssembly
    {
      get { return _contextsByAssembly; }
    }

    public virtual void OnAssemblyStart(AssemblyInfo assembly)
    {
      _currentAssemblyName = assembly.Name;
      _contextsByAssembly.Add(_currentAssemblyName, new List<ContextInfo>());
    }

    public virtual void OnAssemblyEnd(AssemblyInfo assembly)
    {
    }

    public virtual void OnRunStart()
    {
    }

    public virtual void OnRunEnd()
    {
    }

    public virtual void OnContextStart(ContextInfo context)
    {
      _contextsByAssembly[_currentAssemblyName].Add(context);
      _currentContext = context;
      _specificationsByContext.Add(_currentContext, new List<SpecificationInfo>());
    }

    public virtual void OnContextEnd(ContextInfo context)
    {
    }

    public virtual void OnSpecificationStart(SpecificationInfo specification)
    {
    }

    public virtual void OnSpecificationEnd(SpecificationInfo specification, Result result)
    {
      _specificationsByContext[_currentContext].Add(specification);
      _resultsBySpecification.Add(specification, result);
    }

    public void OnFatalError(ExceptionResult exception)
    {
    }
  }
}


