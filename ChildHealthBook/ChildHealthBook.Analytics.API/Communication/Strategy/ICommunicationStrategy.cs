using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChildHealthBook.Analytics.API.Communication.Strategy
{
    public interface ICommunicationStrategy<T>
    {
        Task<IEnumerable<T>> GetAll(string connectionKey = "");
    }
}
