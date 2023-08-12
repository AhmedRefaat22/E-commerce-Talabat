namespace APIDemo.Helpers
{
    public class Pagination<T> where T : class
    {
        public Pagination(int pageSize, int totalCount, int pageIndex, IReadOnlyList<T> data)
        {
            PageSize = pageSize;
            TotalCount = totalCount;
            PageIndex = pageIndex;
            Data = data;
        }

        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int PageIndex { get; set; }
        public IReadOnlyList<T> Data { get; set; }
    }
}
