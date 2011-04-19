<<<<<<< HEAD
using System;

using JetBrains.ReSharper.TaskRunnerFramework;

namespace Machine.Specifications.ReSharperRunner.Tasks
{
	internal partial class ContextTask : IUnitTestRemoteTask
	{
		public string TypeName
		{
			get { return ContextTypeName; }
		}

		public string ShortName
		{
			get { return String.Empty; }
		}
	}
=======
using System;

using JetBrains.ReSharper.TaskRunnerFramework;

namespace Machine.Specifications.ReSharperRunner.Tasks
{
	internal partial class ContextTask : IUnitTestRemoteTask
	{
		public string TypeName
		{
			get { return ContextTypeName; }
		}

		public string ShortName
		{
			get { return String.Empty; }
		}
	}
>>>>>>> feature/externs-subtree
}