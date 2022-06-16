using System;
using XForms.Interfaces;
using Xamarin.Forms;
using System.Collections.Generic;
using XForms.Enums;

namespace XForms.Services
{
    public class AppCenterLogger : ILogger
    {
        public void LogError(Exception exception, IDictionary<string, string> properties = null, bool showError = true)
        {
            if (showError)
            {
                var stackTrace = exception.StackTrace;
                AppHelpers.Alert(exception.Message, exception: exception);
            }

            #region Send extra data with crash log
            var fullNamePair = new KeyValuePair<string, string>(nameof(EventPropertyName.UserName), $"{AppPreferences.FullName}");

            if (properties is IDictionary<string, string> dict)
            {
                dict.Add(fullNamePair);
            }
            else
            {
                properties = new Dictionary<string, string>
                {
                    { fullNamePair.Key, fullNamePair.Value },
                };
            }
            #endregion

            Microsoft.AppCenter.Crashes.Crashes.TrackError(exception, properties);
        }

        public void LogEvent(string eventName, IDictionary<string, string> properties = null)
        {
            #region Send extra data with event log
            var fullNamePair = new KeyValuePair<string, string>(nameof(EventPropertyName.UserName), $"{AppPreferences.FullName}");

            if (properties is IDictionary<string, string> dict)
            {
                dict.Add(fullNamePair);
            }
            else
            {
                properties = new Dictionary<string, string>
                {
                    { fullNamePair.Key, fullNamePair.Value },
                };
            }
            #endregion

            Microsoft.AppCenter.Analytics.Analytics.TrackEvent(eventName, properties);
        }
    }
}