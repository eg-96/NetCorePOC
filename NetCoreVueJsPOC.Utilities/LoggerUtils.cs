using NLog;
using NLog.Web;

namespace NetCoreVueJsPOC.Utilities
{
    public static class LoggerUtils
    {
        private static Logger Logger;


        public static Logger GetLogger()
        {
            if (Logger == null)
            {
                Logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            }

            return Logger;
        }
    }
}
