namespace SPR411_SteamClone.BLL.Services
{
    public class ServiceResponse
    {
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public object? Payload { get; set; }

        public static ServiceResponse Success(string message, object? payload = null)
        {
            return new ServiceResponse
            {
                IsSuccess = true,
                Message = message,
                Payload = payload
            };
        }

        public static ServiceResponse Error(string message, object? payload = null)
        {
            return new ServiceResponse
            {
                IsSuccess = false,
                Message = message,
                Payload = payload
            };
        }
    }
}
