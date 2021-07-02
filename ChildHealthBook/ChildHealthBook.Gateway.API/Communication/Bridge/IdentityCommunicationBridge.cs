using ChildHealthBook.Common.Identity.DTOs;
using ChildHealthBook.Gateway.API.Communication.Strategy.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChildHealthBook.Gateway.API.Communication.Bridge
{
    public class IdentityCommunicationBridge
    {
        private IIdentityCommunicationStrategy _identityCommunication { get; set; }

        public IdentityCommunicationBridge(IIdentityCommunicationStrategy identityCommunication)
        {
            _identityCommunication = identityCommunication;
        }

        public async Task<string> GetTokenAsString(string url, UserLoginDTO credentials)
        {
            return await _identityCommunication.GetToken(url, credentials);
        }

        public async Task<bool> RegisterParent(string url, ParentRegisterDTO parentData)
        {
            return await _identityCommunication.RegisterParent(url, parentData);
        }

        public async Task<bool> RegisterScientist(string url, UserRegisterDTO userData)
        {
            return await _identityCommunication.RegisterScientist(url, userData);
        }

        public async Task<IEnumerable<UserData>> GetParentsFromDb(string url)
        {
            return await _identityCommunication.GetParentsFromDb(url);
        }
    }
}
