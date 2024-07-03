namespace WebApi.ResponseModels
{
    public class BaseResponseModel<T>
    {
        public string ResponseCode { get; }
        public string Message { get; }
        public T Result { get; }
        public IList<T> Results { get; }

        public BaseResponseModel(string responseCode, string message)
        {
            ResponseCode = responseCode;
            Message = message;
        }

        public BaseResponseModel(string responseCode, string message, T result)
        {
            ResponseCode = responseCode;
            Message = message;
            Result = result;
        }

        public BaseResponseModel(string responseCode, string message, IList<T> results)
        {
            ResponseCode = responseCode;
            Message = message;
            Results = results;
        }
    }
}
