using ChildHealthBook.Common.Identity.DTOs;
using ChildHealthBook.Gateway.API.Communication.Strategy.Identity;
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

        public async Task RegisterParent(string url, ParentRegisterDTO parentData)
        {
            await _identityCommunication.RegisterParent(url, parentData);
        }

        public async Task RegisterScientist(string url, UserRegisterDTO userData)
        {
            await _identityCommunication.RegisterScientist(url, userData);
        }
    }
}
