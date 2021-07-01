namespace ChildHealthBook.Analytics.API.Repository.Setup
{
    public class HistoryDatabaseSettings : IHistoryDatabaseSettings
    {
            public string ConnectionString { get; set; }
            public string DatabaseName { get; set; }
    }
    public interface IHistoryDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
