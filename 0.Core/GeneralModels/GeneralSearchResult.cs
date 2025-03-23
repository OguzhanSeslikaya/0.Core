namespace _0.Core.GeneralModels
{
    public class SearchListResult<T> where T : class, new()
    {
        public List<T> Data { get; set; }
        public int TotalCount { get; set; }
    }
}
