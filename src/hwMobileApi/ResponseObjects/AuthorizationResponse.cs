using HydrantWiki.Mobile.Api.Objects;

namespace HydrantWiki.Mobile.Api.ResponseObjects
{
    public class AuthorizationResponse : BaseResponse
    {
        public User User { get; set; }
        public string Message { get; set; }
    }
}