using System.Collections.Generic;
using HydrantWiki.Mobile.Api.Objects;

namespace HydrantWiki.Mobile.Api.ResponseObjects
{
    public class TagsResponse : PagedResponse
    {
        public TagsResponse() { }

        public TagsResponse(List<Tag> _tags, int _currentPage, int _pageSize, int _totalPages, int _totalItems)
            : base(_currentPage, _pageSize, _totalPages, _totalItems)
        {
            Tags = _tags;
        }

        public List<Tag> Tags { get; set; }
    }
}