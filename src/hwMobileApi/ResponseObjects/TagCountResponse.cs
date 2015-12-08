namespace HydrantWiki.Mobile.Api.ResponseObjects
{
    public class TagCountResponse : BaseResponse
    { 
        public int TagCount { get; set; }

        public TagCountResponse() { }

        public TagCountResponse(bool _success, int _tagCount)
            : base(_success)
        {
            TagCount = _tagCount;
        }
    }
}