namespace api.Responses
{
    public class APIResponse<T>
    {
        public int Status { get; set; }
        public T? Data { get; set; }
    }
}