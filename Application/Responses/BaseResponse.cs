namespace Application.Responses
{
    public abstract class BaseResponse
    {
        public string Message {get; protected set;}
        public bool Success {get; protected set;}

        public BaseResponse(string message, bool success)
        {
            Message = message;
            Success = success;
        }
        
    }
}