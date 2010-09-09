using System;
using System.Collections.Generic;
using log4net;
using log4net.Config;

namespace Hotfixer
{
  /**
   * This class implements the singleton pattern.  There can be only one!
   **/

  public class Logger
  {
    private static Logger _loggerInstance; // Instance of this object
    private ILog _logObject;
    private Stack<string> _stack;

    public enum Level : int
    {
      DEBUG = 1
      ,
      INFO = 2
      ,
      WARN = 3
      ,
      ERROR = 4
      ,
      FATAL = 5
    }

    /**
     * Make the default constructor private so the class cannot be instantiated
     * using the NEW keyword.
     **/

    private Logger(string logName)
    {
      // Initialize the object we'll use for logging
      _logObject = LogManager.GetLogger(logName);
      // Configure the log object
      XmlConfigurator.Configure();
      // Initialize the stack
      _stack = new Stack<string>();
    }

    // Get an instance of this object
    public static Logger GetLoggerInstance(string logName)
    {
      // Initialize this class if it hasn't been already
      if (_loggerInstance == null)
        _loggerInstance = new Logger(logName);

      // Return the current instance of this object
      return _loggerInstance;
    }

    // 
    public void Log(Level logLevel, string message)
    {
      this.Log(logLevel, message, null);
    }

    public void Log(Level logLevel, string message, Exception ex)
    {
      switch (logLevel)
      {
        case Level.DEBUG:
          _logObject.Debug(message, ex);
          break;
        case Level.INFO:
          _logObject.Info(message, ex);
          break;
        case Level.WARN:
          _logObject.Warn(message, ex);
          break;
        case Level.ERROR:
          _logObject.Error(message, ex);
          break;
        case Level.FATAL:
          _logObject.Fatal(message, ex);
          break;
      }
    }

    public void Push(string moduleName, string methodName)
    {
      string element = string.Format("{0}.{1}", moduleName, methodName);
      _stack.Push(element);
      this.Log(Level.DEBUG, string.Format("->{0} {1}", this.Indentation(_stack.Count), element));
    }

    public void Pop()
    {
      if (_stack.Count > 0)
        this.Log(Level.DEBUG, string.Format("<-{0} {1}", this.Indentation(_stack.Count), _stack.Pop()));
      else
        this.Log(Level.DEBUG, string.Format("<-"));
    }

    private string Indentation(int length)
    {
      return new string(' ', (length * 2));
    }
  }
}