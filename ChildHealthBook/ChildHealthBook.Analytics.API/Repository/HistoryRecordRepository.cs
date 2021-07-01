using ChildHealthBook.Analytics.API.Repository.Setup;
using MongoDB.Driver;
using System.Collections.Generic;

namespace ChildHealthBook.Analytics.API.Repository
{
    public class HistoryRecordRepository<T> : IHistoryRecordRepository<T>
    {
        private readonly IMongoCollection<T> _entries;

        public HistoryRecordRepository(IHistoryDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _entries = database.GetCollection<T>(typeof(T).Name);
        }

        public IEnumerable<T> GetAll() =>
            _entries.Find(history => true).ToList();

        public T Insert(T entry)
        {
            _entries.InsertOne(entry);
            return entry;
        }

    }
}
