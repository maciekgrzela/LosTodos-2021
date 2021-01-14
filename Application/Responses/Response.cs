namespace Application.Responses
{
    public class Response<T> : BaseResponse
    {
        public T Value {get; private set;}

        public Response(string message, bool success, T value) : base(message, success) 
        {
            Value = value;
        }
        public Response(string message) : this(message, false, default(T)) {}
        public Response(T value) : this(string.Empty, true, value) {}
        
    }
}