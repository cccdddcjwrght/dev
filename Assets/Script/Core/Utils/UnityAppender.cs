using UnityEngine;
using log4net.Core;
namespace log4net.Appender
{
    public class UnityAppender : log4net.Appender.AppenderSkeleton
    {
        protected override void Append(LoggingEvent loggingEvent)
        {
            var message = RenderLoggingEvent(loggingEvent);
            if (loggingEvent.Level >= Level.Error)
            {
                Debug.LogError(message);
            }
            else if (loggingEvent.Level == Level.Warn)
            {
                Debug.LogWarning(message);
            }
            else
            {
                Debug.Log(message);
            }
        }
    }
}
