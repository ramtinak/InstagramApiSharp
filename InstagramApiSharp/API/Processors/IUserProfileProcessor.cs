using System.Threading.Tasks;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;

namespace InstagramApiSharp.API.Processors
{
    public interface IUserProfileProcessor
    {
        Task<IResult<InstaUserShort>> SetAccountPrivateAsync();

        Task<IResult<InstaUserShort>> SetAccountPublicAsync();

        Task<IResult<bool>> ChangePasswordAsync(string oldPassword, string newPassword);
    }
}