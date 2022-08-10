namespace WebApi.Models.Common
{
    public class ApiResponseDto
    {
        public object Results { get; set; } 
        public int StatusCode { get; set; } = 200;
    }

    public class PaginationResponseDto
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public object Items{get;set;}
        public int TotalCount { get; set; }
    }

    public class SingleItemResponseDto
    {
        public object Item { get; set; }
    }

    public class ModelStateResponseDto
    {
        public object errors { get; set; }
        public int status { get; set; }
    }
}
