using System.Collections.Generic;
using System.Threading.Tasks;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;

namespace InstagramApiSharp.API.Processors
{
    public interface IHashtagProcessor
    {
        Task<IResult<InstaHashtagSearch>> Search(string query, IEnumerable<long> excludeList = null, string rankToken = null);

        Task<IResult<InstaHashtag>> GetHashtagInfo(string tagname);
    }
}