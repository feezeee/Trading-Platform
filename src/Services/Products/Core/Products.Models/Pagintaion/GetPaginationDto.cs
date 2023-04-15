namespace Products.Models.Pagintaion
{
    public class GetPaginationDto <T> where T : class
    {
        public GetPaginationDto(List<T> data, int pageNumber, int pageSize, int totalCount)
        {
            if (pageNumber < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "pageNumber less than 0");
            }

            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "pageNumber less than 0");
            }

            Data = data;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
        }

        public List<T> Data { get; set; } 

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public int TotalPages => (int)Math.Ceiling( (double)TotalCount / (double)PageSize);

        public bool HasNextPage => PageNumber < TotalPages;

        public bool HasPreviousPage => PageNumber > 1;
    }
}
