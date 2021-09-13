namespace Application.Responses
{
    public class Response<T>
    {
        public bool Succeed { get; set; }
        public ResponseResult Result { get; set; }
        public string ErrorMessage { get; set; }
        public T Value {get; private set;}


        public static Response<T> Success(ResponseResult result, T value) => new() {Succeed = true, Result = result, Value = value};
        public static Response<T> Success(ResponseResult result) => new() {Succeed = true, Result = result};
        public static Response<T> Failure(ResponseResult error, string message) => new() {Succeed = false, Result = error, ErrorMessage = message};
    }
}