using Serilog;

namespace Logging
{
    internal static class LoggingHttpResponse
    {
        internal static void LogHttpResponse(this ILogger logger, HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                logger.Debug("Received a success response from {Url}", response.RequestMessage.RequestUri);
            }
            else
            {
                logger.Warning("Received a non-success status code {StatusCode} from {Url}",
                    (int)response.StatusCode, response.RequestMessage.RequestUri);
            }
        }
    }
}