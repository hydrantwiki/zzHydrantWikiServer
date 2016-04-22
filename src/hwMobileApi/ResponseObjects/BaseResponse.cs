namespace HydrantWiki.Mobile.Api.ResponseObjects
{ 
    public class BaseResponse
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public string Message { get; set; }

        public BaseResponse() { }

        public BaseResponse(bool _success)
        {
            Success = _success;
        }
    }
}