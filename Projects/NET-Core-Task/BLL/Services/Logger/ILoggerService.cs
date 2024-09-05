namespace NET_Core_Task.BLL.Services.Logger
{
    public interface ILoggerService
    {
        void LogInformation(string msg);
        void LogWarning(string msg);
        void LogTrace(string msg);
        void LogDebug(string msg);
        void LogError(object request, string errorMsg);
    }
}
