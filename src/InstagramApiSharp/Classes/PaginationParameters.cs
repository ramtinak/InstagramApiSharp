namespace InstagramApiSharp
{
    public class PaginationParameters
    {
        private PaginationParameters()
        {
        }

        public string RankToken { get; set; } = string.Empty;
        public string NextId { get; set; } = string.Empty;
        public int MaximumPagesToLoad { get; private set; }
        public int PagesLoaded { get; set; } = 1;

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

        public PaginationParameters StartFromRankToken(string nextId, string rankToken)
        {
            NextId = nextId;
            RankToken = rankToken;
            return this;
        }
    }
}