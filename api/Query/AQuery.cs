namespace api.Query
{
    public abstract class AQuery
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? KeyWord { get; set; }
    }
}