namespace ChildHealthBook.Gateway.API
{
    public class GatewayApiSettings : IGatewayApiSettings
    {
        public string StorageConnectionString { get; set; }
        public string AddNewChildEventQueue { get; set; }
        public string AddNewMedicalEventQueue { get; set; }
        public string AddNewChildPersonalEventQueue { get; set; }
        public string AddNewExaminationQueue { get; set; }
        public string SendExaminationToNotificationQueue { get; set; }
    }

    public interface IGatewayApiSettings
    {
        string StorageConnectionString { get; set; }
        string AddNewChildEventQueue { get; set; }
        string AddNewMedicalEventQueue { get; set; }
        string AddNewChildPersonalEventQueue { get; set; }
        string AddNewExaminationQueue { get; set; }
        string SendExaminationToNotificationQueue { get; set; }
    }
}
