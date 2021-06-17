namespace ChildHealthBook.Common
{
    public class ApiSettings : IApiSettings
    {
        public string StorageConnectionString { get; set; }
        public string AddNewChildPersonalEventQueue { get; set; }
    }

    public interface IApiSettings
    {
        string StorageConnectionString { get; set; }
        string AddNewChildPersonalEventQueue { get; set; }
    }
}
