namespace RickAndMortyWebApp.Models
{
    public class PaginationModel<T>(int pageIndex, int pageSize, int count, IReadOnlyList<T> data)
    {
        public int PageIndex { get; set; } = pageIndex;
        public int PageSize { get; set; } = pageSize;
        public int TotalPages { get; set; } = (int)Math.Ceiling(count / (double)pageSize);
        public IReadOnlyList<T> Data { get; set; } = data;
    }
}
