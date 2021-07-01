using Common;
using System.Collections.Generic;

namespace ChildHealthBook.Analytics.API.Repository
{
    public interface IHistoryRecordRepository<T>
    {
        IEnumerable<T> GetAll();
        T Insert(T entry);

    }
}
