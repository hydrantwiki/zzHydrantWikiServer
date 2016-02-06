namespace HydrantWiki.Mobile.Api.ResponseObjects
{
    public class PagedResponse : BaseResponse
    {
        public PagedResponse() { }

        public PagedResponse(int _currentPage, int _pageSize, int _totalPages, int _totalItems)
            : base(true)
        {
            CurrentPage = _currentPage;
            PageSize = _pageSize;
            TotalPages = _totalPages;
            TotalItems = _totalItems;
        }

        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
    }
}