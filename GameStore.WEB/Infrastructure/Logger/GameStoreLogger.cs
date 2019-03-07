using NLog;

namespace GameStore.WEB.Infrastructure.Logger
{
    public class GameStoreLogger
    {
        public static NLog.Logger logger = LogManager.GetCurrentClassLogger();
    }
}