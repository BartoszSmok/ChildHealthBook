namespace ChildHealthBook.Common
{
    public class GatewayApiSettings : IGatewayApiSettings
    {
        public string StorageConnectionString { get; set; }
        public string AddNewChildPersonalEventQueue { get; set; }
    }

    public interface IGatewayApiSettings
    {
        string StorageConnectionString { get; set; }
        string AddNewChildPersonalEventQueue { get; set; }
    }
}
