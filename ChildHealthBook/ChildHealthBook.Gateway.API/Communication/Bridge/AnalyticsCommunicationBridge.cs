using ChildHealthBook.Gateway.API.Communication.Strategy;
using System;
using System.Threading.Tasks;

namespace ChildHealthBook.Gateway.API.Communication.Bridge
{
    public class AnalyticsCommunicationBridge<T>
    {
        private IAnalyticsCommunicationStrategy<T> _communicationStrategy { get; set; }
        public AnalyticsCommunicationBridge(IAnalyticsCommunicationStrategy<T> strategy)
        {
            _communicationStrategy = strategy;
        }
        public async Task<T> Get(string url)
        {
            return await _communicationStrategy.Get(url);
        }
    }
}
