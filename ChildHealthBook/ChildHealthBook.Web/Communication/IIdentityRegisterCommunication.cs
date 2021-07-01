using System.Threading.Tasks;

namespace ChildHealthBook.Web.Communication
{
    public interface IIdentityRegisterCommunication<T>
    {
        Task<bool> Register(T account, string url);
    }
}
