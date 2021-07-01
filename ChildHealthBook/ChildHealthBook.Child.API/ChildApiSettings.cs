namespace ChildHealthBook.Child.API
{
    public class ChildApiSettings : IChildApiSettings
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }

        public string ChildCollectionName { get; set; }
        public string ExaminationCollectionName { get; set; }
        public string PersonalEventCollectionName { get; set; }
        public string MedicalEventCollectionName { get; set; }
        public string SharedEventCollectionName { get; set; }

        public string StorageConnectionString { get; set; }
        public string AddNewChildEventQueue { get; set; }
        public string AddNewMedicalEventQueue { get; set; }
        public string AddNewChildPersonalEventQueue { get; set; }
        public string AddNewExaminationQueue { get; set; }
        public string ShareEventQueue { get; set; }
    }

    public interface IChildApiSettings
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }

        string ChildCollectionName { get; set; }
        string ExaminationCollectionName { get; set; }
        string PersonalEventCollectionName { get; set; }
        string MedicalEventCollectionName { get; set; }
        string SharedEventCollectionName { get; set; }

        string StorageConnectionString { get; set; }
        string AddNewChildEventQueue { get; set; }
        string AddNewMedicalEventQueue { get; set; }
        string AddNewChildPersonalEventQueue { get; set; }
        string AddNewExaminationQueue { get; set; }
        string ShareEventQueue { get; set; }
    }
}
