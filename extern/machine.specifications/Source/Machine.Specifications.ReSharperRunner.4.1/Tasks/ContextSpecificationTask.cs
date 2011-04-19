<<<<<<< HEAD
using System;
using System.Xml;

namespace Machine.Specifications.ReSharperRunner.Tasks
{
  [Serializable]
  internal class ContextSpecificationTask : Task, IEquatable<ContextSpecificationTask>
  {
    readonly string _specificationFieldName;

    public ContextSpecificationTask(XmlElement element) : base(element)
    {
      _specificationFieldName = GetXmlAttribute(element, "SpecificationFieldName");
    }

    public ContextSpecificationTask(string providerId,
                                    string assemblyLocation,
                                    string contextTypeName,
                                    string specificationFieldName,
                                    bool runExplicitly)
      : base(providerId, assemblyLocation, contextTypeName, runExplicitly)
    {
      _specificationFieldName = specificationFieldName;
    }

    public string SpecificationFieldName
    {
      get { return _specificationFieldName; }
    }

    public override void SaveXml(XmlElement element)
    {
      base.SaveXml(element);

      SetXmlAttribute(element, "SpecificationFieldName", SpecificationFieldName);
    }

    public override bool Equals(object other)
    {
      return Equals(other as ContextSpecificationTask);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        int result = base.GetHashCode();
        result = (result * 397) ^ SpecificationFieldName.GetHashCode();
        return result;
      }
    }

    public bool Equals(ContextSpecificationTask other)
    {
      if (other == null || !base.Equals(other))
      {
        return false;
      }

      return Equals(SpecificationFieldName, other.SpecificationFieldName);
    }
  }
=======
using System;
using System.Xml;

namespace Machine.Specifications.ReSharperRunner.Tasks
{
  [Serializable]
  internal class ContextSpecificationTask : Task, IEquatable<ContextSpecificationTask>
  {
    readonly string _specificationFieldName;

    public ContextSpecificationTask(XmlElement element) : base(element)
    {
      _specificationFieldName = GetXmlAttribute(element, "SpecificationFieldName");
    }

    public ContextSpecificationTask(string providerId,
                                    string assemblyLocation,
                                    string contextTypeName,
                                    string specificationFieldName,
                                    bool runExplicitly)
      : base(providerId, assemblyLocation, contextTypeName, runExplicitly)
    {
      _specificationFieldName = specificationFieldName;
    }

    public string SpecificationFieldName
    {
      get { return _specificationFieldName; }
    }

    public override void SaveXml(XmlElement element)
    {
      base.SaveXml(element);

      SetXmlAttribute(element, "SpecificationFieldName", SpecificationFieldName);
    }

    public override bool Equals(object other)
    {
      return Equals(other as ContextSpecificationTask);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        int result = base.GetHashCode();
        result = (result * 397) ^ SpecificationFieldName.GetHashCode();
        return result;
      }
    }

    public bool Equals(ContextSpecificationTask other)
    {
      if (other == null || !base.Equals(other))
      {
        return false;
      }

      return Equals(SpecificationFieldName, other.SpecificationFieldName);
    }
  }
>>>>>>> feature/externs-subtree
}