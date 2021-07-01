using ChildHealthBook.Analytics.API.Communication.Strategy;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChildHealthBook.Analytics.API.Communication.Bridge
{
    public interface ICommunicationBridge<T>
    {
        ICommunicationStrategy<T> _communicationStrategy { get; set; }

        Task<IEnumerable<T>> GetAll(string url);
    }
}
