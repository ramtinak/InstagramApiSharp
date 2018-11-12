using System;

namespace InstagramApiSharp
{
    /// <summary>
    ///     Pagination of everything! use NextMaxId instead of using old NextId
    /// </summary>
    public class PaginationParameters
    {
        private PaginationParameters()
        {
        }

        public string RankToken { get; set; } = string.Empty;
        public string NextMaxId { get; set; } = string.Empty;
        /// <summary>
        ///     Only works for Comments!
        /// </summary>
        public string NextMinId { get; set; }
        public int MaximumPagesToLoad { get; private set; }
        public int PagesLoaded { get; set; } = 1;

        public static PaginationParameters Empty => MaxPagesToLoad(int.MaxValue);

        public static PaginationParameters MaxPagesToLoad(int maxPagesToLoad)
        {
            return new PaginationParameters {MaximumPagesToLoad = maxPagesToLoad};
        }

        [Obsolete("StartFromId is deprecated. Please use PaginationParameters.StartFromMaxId instead.")]
        public PaginationParameters StartFromId(string maxId)
        {
            NextMaxId = maxId;
            NextMinId = null;
            return this;
        }


        public PaginationParameters StartFromMaxId(string maxId)
        {
            NextMaxId = maxId;
            NextMinId = null;
            return this;
        }

        public PaginationParameters StartFromMinId(string minId)
        {
            NextMinId = minId;
            NextMaxId = null;
            return this;
        }

        public PaginationParameters StartFromRankToken(string nextId, string rankToken)
        {
            NextMaxId = nextId;
            RankToken = rankToken;
            return this;
        }
    }
}