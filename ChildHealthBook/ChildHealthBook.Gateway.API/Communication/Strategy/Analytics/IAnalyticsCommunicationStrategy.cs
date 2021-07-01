using System.Threading.Tasks;

namespace ChildHealthBook.Gateway.API.Communication.Strategy
{
    public interface IAnalyticsCommunicationStrategy<T>
    {
        Task<T> Get(string connectionKey = "");
    }
}
