namespace ProductAPI.Domain.Response
{
    public class ApiResponse
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public string Tag { get; private set; }

        public ApiResponse(bool success, string message, string tag = default)
        {
            Success = success;
            Message = message;
            Tag = tag;
        }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T Data { get; private set; }

        public ApiResponse(T resource, string tag = default) : base(true, string.Empty, tag)
        {
            Data = resource;
        }

        public ApiResponse(string message, string tag = default) : base(false, message, tag)
        {
            Data = default;
        }
    }

    public class ApiResponseFactory
    {
        public static ApiResponse CreateSuccess()
        {
            return new ApiResponse(true, string.Empty);
        }

        public static ApiResponse<T> CreateSuccess<T>(T data)
        {
            return new ApiResponse<T>(data);
        }

        public static ApiResponse CreateError(string message = default, string tag = default)
        {
            return new ApiResponse(false, message, tag);
        }

        public static ApiResponse<T> CreateError<T>(string message = default, string tag = default)
        {
            return new ApiResponse<T>(message, tag);
        }
    }
}