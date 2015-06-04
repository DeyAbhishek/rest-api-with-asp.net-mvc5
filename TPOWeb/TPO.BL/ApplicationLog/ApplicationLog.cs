using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TPO.BL.ApplicationLog
{
    public class ApplicationLog : IApplicationLog
    {
        private readonly ILog _log = null;

        private ApplicationLog(Type callingType, string userName, int stackDepth)
        {
            //StackTrace stackTrace = new StackTrace(stackDepth + 1);
            //MethodBase methodBase = stackTrace.GetFrame(0).GetMethod();
            //Type callingType = methodBase.DeclaringType;

            _log = log4net.LogManager.GetLogger(callingType);

            if (!string.IsNullOrEmpty(userName))
                log4net.MDC.Set("auth", userName);

        }

        private ApplicationLog(ILog log, string userName)
        {
            _log = log;

            if (!string.IsNullOrEmpty(userName))
                log4net.MDC.Set("auth", userName);
        }

        public static IApplicationLog GetLogger(Type callingType, string userName)
        {
            return new ApplicationLog(callingType, userName, 1);
        }

        public static IApplicationLog GetLogger(ILog log, string userName)
        {
            return new ApplicationLog(log, userName);
        }

        public IDisposable PushContext(string context)
        {
            return log4net.NDC.Push(context);
        }

        public void PopContext()
        {
            log4net.NDC.Pop();
        }

        public void Warn(object message)
        {
            _log.Warn(message);
        }

        public void Warn(object message, Exception ex)
        {
            _log.Warn(message, ex);
        }


        public void Debug(object message, Exception exception)
        {
            _log.Debug(message, exception);
        }

        public void Debug(object message)
        {
            _log.Debug(message);
        }

        public void Error(object message, Exception exception)
        {
            _log.Error(message, exception);
        }

        public void Error(object message)
        {
            _log.Error(message);
        }

        public void Fatal(object message, Exception exception)
        {
            _log.Fatal(message, exception);
        }

        public void Fatal(object message)
        {
            _log.Fatal(message);
        }

        public void Info(object message, Exception exception)
        {
            _log.Info(message, exception);
        }

        public void Info(object message)
        {
            _log.Info(message);
        }

        public bool IsDebugEnabled
        {
            get { return _log.IsDebugEnabled; }
        }

        public bool IsErrorEnabled
        {
            get { return _log.IsErrorEnabled; }
        }

        public bool IsFatalEnabled
        {
            get { return _log.IsFatalEnabled; }
        }

        public bool IsInfoEnabled
        {
            get { return _log.IsInfoEnabled; }
        }

        public bool IsWarnEnabled
        {
            get { return _log.IsWarnEnabled; }
        }
    }
}
