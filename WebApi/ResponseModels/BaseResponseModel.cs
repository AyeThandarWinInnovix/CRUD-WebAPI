namespace WebApi.ResponseModels
{
    public class BaseResponseModel<T>
    {
        public int ResponseCode { get; }
        public string Message { get; }
        public T Result { get; }
        public IList<T> Results { get; }

        public BaseResponseModel(int responseCode, string message)
        {
            ResponseCode = responseCode;
            Message = message;
        }

        public BaseResponseModel(int responseCode, string message, T result)
        {
            ResponseCode = responseCode;
            Message = message;
            Result = result;
        }

        public BaseResponseModel(int responseCode, string message, IList<T> results)
        {
            ResponseCode = responseCode;
            Message = message;
            Results = results;
        }
    }
}
