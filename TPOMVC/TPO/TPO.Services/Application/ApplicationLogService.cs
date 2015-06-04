using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using TPO.Services.Core;

namespace TPO.Services.Application
{
    public class ApplicationLogService : IApplicationLogService
    {
        private static ILog _log = null;
        private static IApplicationLogService _theInstance = null;

        private ApplicationLogService(Type callingType, string userName, int stackDepth)
        {
            //StackTrace stackTrace = new StackTrace(stackDepth + 1);
            //MethodBase methodBase = stackTrace.GetFrame(0).GetMethod();
            //Type callingType = methodBase.DeclaringType;

            _log = log4net.LogManager.GetLogger(callingType);

            if (!string.IsNullOrEmpty(userName))
                log4net.MDC.Set("auth", userName);

            _theInstance = this;
        }

        private ApplicationLogService(ILog log, string userName)
        {
            _log = log;

            if (!string.IsNullOrEmpty(userName))
                log4net.MDC.Set("auth", userName);

            _theInstance = this;
        }

        public static IApplicationLogService InitializeLogger(Type callingType, string userName)
        {
            return new ApplicationLogService(callingType, userName, 1);
        }

        public static IApplicationLogService InitializeLogger(ILog log, string userName)
        {
            return new ApplicationLogService(log, userName);
        }

        public static IApplicationLogService GetInstance()
        {
            if (_theInstance == null)
                throw new InvalidOperationException();

            return _theInstance;
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

        public void Warn(object message, Exception ex, string userName, string hostName)
        {
            SetAppenderProperties(userName, hostName);
            _log.Warn(message, ex);
        }

        private void SetAppenderProperties(string userName, string hostName)
        {
            log4net.GlobalContext.Properties["userName"] = userName;
            log4net.GlobalContext.Properties["hostName"] = hostName;
        }


        public void Debug(object message, Exception exception, string userName, string hostName)
        {
            SetAppenderProperties(userName, hostName);
            _log.Debug(message, exception);
        }

        public void Debug(object message, Exception exception)
        {
            _log.Debug(message, exception);
        }

        public void Debug(object message)
        {
            _log.Debug(message);
        }

        public void Error(object message, Exception exception, string userName, string hostName)
        {
            SetAppenderProperties(userName, hostName);
            _log.Error(message, exception);
        }

        public void Error(object message, Exception exception)
        {
            _log.Error(message, exception);
        }

        public void Error(object message)
        {
            _log.Error(message);
        }

        public void Fatal(object message, Exception exception, string userName, string hostName)
        {
            SetAppenderProperties(userName, hostName);
            _log.Fatal(message, exception);
        }

        public void Fatal(object message, Exception exception)
        {
            _log.Fatal(message, exception);
        }

        public void Fatal(object message)
        {
            _log.Fatal(message);
        }

        public void Info(object message, Exception exception, string userName, string hostName)
        {
            SetAppenderProperties(userName, hostName);
            _log.Info(message, exception);
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
