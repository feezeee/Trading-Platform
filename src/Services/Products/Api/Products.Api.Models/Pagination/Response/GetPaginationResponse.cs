using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Products.Api.Models.Pagination.Response
{
    public class GetPaginationResponse <T> where T : class
    {
        public GetPaginationResponse(List<T> data, int pageNumber, int pageSize, int totalCount)
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

        [JsonPropertyName("data")]
        [BindRequired]
        [Required]
        public List<T> Data { get; set; }

        [JsonPropertyName("page_number")]
        [BindRequired]
        [Required]
        public int PageNumber { get; set; }

        [JsonPropertyName("page_size")]
        [BindRequired]
        [Required]

        public int PageSize { get; set; }

        [JsonPropertyName("total_count")]
        [BindRequired]
        [Required]

        public int TotalCount { get; set; }

        [JsonPropertyName("total_pages")]
        [BindRequired]
        [Required]

        public int TotalPages => (int)Math.Ceiling((double)TotalCount / (double)PageSize);

        [JsonPropertyName("has_nex_page")]
        [BindRequired]
        [Required]
        public bool HasNextPage => PageNumber < TotalPages;

        [JsonPropertyName("has_previous_page")]
        [BindRequired]
        [Required]
        public bool HasPreviousPage => PageNumber > 1;
    }
}
