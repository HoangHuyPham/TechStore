namespace api.DTOs
{
    public class PaginationResponseDTO<T>
    {
        public List<T> Items { get; set; } = null!;
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPage { get; set; }
        public int TotalItems { get; set; }
    }
}
