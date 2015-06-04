using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPO.Services.Core
{
    public interface IApplicationLogService
    {
        IDisposable PushContext(string context);
        void PopContext();

        void Warn(object message);
        void Warn(object message, Exception ex);
        void Warn(object message, Exception exception, string userName, string machineName);
        
        void Debug(object message);
        void Debug(object message, Exception exception);
        void Debug(object message, Exception exception, string userName, string machineName);
        
        void Error(object message);
        void Error(object message, Exception exception);
        void Error(object message, Exception exception, string userName, string machineName);
        
        void Fatal(object message);
        void Fatal(object message, Exception exception);
        void Fatal(object message, Exception exception, string userName, string machineName);
        
        void Info(object message);
        void Info(object message, Exception exception);
        void Info(object message, Exception exception, string userName, string machineName);
        
        bool IsDebugEnabled { get; }
        bool IsErrorEnabled { get; }
        bool IsFatalEnabled { get; }
        bool IsInfoEnabled { get; }
        bool IsWarnEnabled { get; }
    }

}
