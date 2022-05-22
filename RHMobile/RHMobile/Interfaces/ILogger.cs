using System;
using System.Collections.Generic;

namespace XForms.Interfaces
{
    public interface ILogger
    {
        void LogError(Exception exception, IDictionary<string, string> properties = null, bool showError = true);

        void LogEvent(string eventName, IDictionary<string, string> properties = null);
    }
}