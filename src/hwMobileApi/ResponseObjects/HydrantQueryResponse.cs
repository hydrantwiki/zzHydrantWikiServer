using System.Collections.Generic;
using HydrantWiki.Mobile.Api.Objects;

namespace HydrantWiki.Mobile.Api.ResponseObjects
{
    public class HydrantQueryResponse: BaseResponse
    {
        public HydrantQueryResponse() { }

        public HydrantQueryResponse(bool _success,
            List<HydrantHeader> _hydrants) : base(_success)
        {
            Hydrants = _hydrants;
        }

        public List<HydrantHeader> Hydrants { get; set; }

    }
}