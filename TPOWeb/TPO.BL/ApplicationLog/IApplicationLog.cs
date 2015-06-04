using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPO.BL.ApplicationLog
{
    public interface IApplicationLog
    {
        IDisposable PushContext(string context);
        void PopContext();
        void Warn(object message);
        void Warn(object message, Exception ex);
        void Debug(object message, Exception exception);
        void Debug(object message);
        void Error(object message, Exception exception);
        void Error(object message);
        void Fatal(object message, Exception exception);
        void Fatal(object message);
        void Info(object message, Exception exception);
        void Info(object message);
        bool IsDebugEnabled { get; }
        bool IsErrorEnabled { get; }
        bool IsFatalEnabled { get; }
        bool IsInfoEnabled { get; }
        bool IsWarnEnabled { get; }
    }
}
