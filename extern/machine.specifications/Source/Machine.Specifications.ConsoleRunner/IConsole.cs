using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Machine.Specifications.ConsoleRunner
{
  public interface IConsole
  {
    void Write(string line);
    void Write(string line, params object[] parameters);
    void WriteLine(string line);
    void WriteLine(string line, params object[] parameters);
  }
}