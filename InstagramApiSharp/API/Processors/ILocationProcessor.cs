using System.Threading.Tasks;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;

namespace InstagramApiSharp.API.Processors
{
    public interface ILocationProcessor
    {
        Task<IResult<InstaLocationShortList>> Search(double latitude, double longitude, string query);

        Task<IResult<InstaLocationFeed>> GetFeed(long locationId, PaginationParameters paginationParameters);
    }
}