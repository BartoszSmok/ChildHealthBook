using ChildHealthBook.Common.Identity.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChildHealthBook.Gateway.API.Communication.Strategy.Identity
{
    public interface IIdentityCommunicationStrategy
    {
        Task<string> GetToken(string url, UserLoginDTO credentials);
        Task<bool> RegisterParent(string url, ParentRegisterDTO parentData);

        Task<bool> RegisterScientist(string url, UserRegisterDTO userData);
        Task<IEnumerable<UserData>> GetParentsFromDb(string connectionKey = "");
    }
}
