
namespace Core.Repository.Paging
{
    public abstract class PageableModel<TData>
    {
        public int Index { get; set; }
        public int Size { get; set; }
        public int Count { get; set; }
        public int Pages { get; set; }
        public bool HasPrevious { get; set; }
        public bool HasNext { get; set; }
        public IList<TData> Items { get; set; }
    }
}
