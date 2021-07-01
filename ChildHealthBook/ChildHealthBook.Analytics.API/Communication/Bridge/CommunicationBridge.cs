using ChildHealthBook.Analytics.API.Communication.Strategy;
using ChildHealthBook.Common.WebDtos.ChildDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChildHealthBook.Analytics.API.Communication.Bridge
{
    public class CommunicationBridge<T> : ICommunicationBridge<T>
    {  
        public ICommunicationStrategy<T> _communicationStrategy { get; set; }

        public CommunicationBridge(ICommunicationStrategy<T> strategy)
        {
            _communicationStrategy = strategy;
        }

        public async Task<IEnumerable<T>> GetAll(string url)
        {
            return await _communicationStrategy.GetAll(url);
        }
    }
}
