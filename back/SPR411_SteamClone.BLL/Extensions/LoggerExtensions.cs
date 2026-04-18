using Microsoft.Extensions.Logging;

namespace SPR411_SteamClone.BLL.Extensions
{
    public static class LoggerExtensions
    {
        public static void LogInformationWithTimestamp<T>(this ILogger<T> logger, string message)
        {
            logger.LogInformation($"[{DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")}] - {message}");
        }
    }
}
