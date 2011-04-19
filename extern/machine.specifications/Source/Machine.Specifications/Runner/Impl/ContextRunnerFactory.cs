<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications.Model;

namespace Machine.Specifications.Runner.Impl
{
  public static class ContextRunnerFactory
  {
    public static IContextRunner GetContextRunnerFor(Context context)
    {
      if (context.IsSetupForEachSpec)
      {
        return new SetupForEachContextRunner();
      }
      else
      {
        return new SetupOnceContextRunner();
      }
    }
  }
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications.Model;

namespace Machine.Specifications.Runner.Impl
{
  public static class ContextRunnerFactory
  {
    public static IContextRunner GetContextRunnerFor(Context context)
    {
      if (context.IsSetupForEachSpec)
      {
        return new SetupForEachContextRunner();
      }
      else
      {
        return new SetupOnceContextRunner();
      }
    }
  }
>>>>>>> feature/externs-subtree
}