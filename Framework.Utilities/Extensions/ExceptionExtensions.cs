using System;
using System.Runtime.CompilerServices;
using NLog;

namespace Framework.Utilities.Extensions
{
    public static class ExceptionExtensions
    {
        public static void Manage(this Exception e, [CallerFilePath] string filePath = "", [CallerMemberName] string callerName = "")
        {
            var logger = LogManager.GetLogger($"{filePath}-{callerName}");
            logger.Error(!string.IsNullOrEmpty(e.InnerException?.Message) ? e.InnerException.Message : e.Message);
        }
    }
}
