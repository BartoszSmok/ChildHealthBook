namespace ChildHealthBook.Common
{
    public class ApiSettings : IApiSettings
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }

        public string ChildCollectionName { get; set; }
        public string ExaminationCollectionName { get; set; }
        public string PersonalEventCollectionName { get; set; }
        public string MedicalEventCollectionName { get; set; }

        public string StorageConnectionString { get; set; }
        public string AddNewChildPersonalEventQueue { get; set; }
    }

    public interface IApiSettings
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }

        string ChildCollectionName { get; set; }
        string ExaminationCollectionName { get; set; }
        string PersonalEventCollectionName { get; set; }
        string MedicalEventCollectionName { get; set; }

        string StorageConnectionString { get; set; }
        string AddNewChildPersonalEventQueue { get; set; }
    }
}
