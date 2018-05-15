namespace InstagramApiSharp.Classes
{
    public class PaginationParameters
    {
        private PaginationParameters()
        {
        }

        public string NextId { get; set; } = string.Empty;
        public int MaximumPagesToLoad { get; private set; }
        public int PagesLoaded { get; set; }

        public static PaginationParameters Empty => MaxPagesToLoad(int.MaxValue);

        public static PaginationParameters MaxPagesToLoad(int maxPagesToLoad)
        {
            return new PaginationParameters {MaximumPagesToLoad = maxPagesToLoad};
        }


        public PaginationParameters StartFromId(string nextId)
        {
            NextId = nextId;
            return this;
        }
    }
}