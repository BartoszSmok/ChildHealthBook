namespace ChildHealthBook.Child.API.Models
{
    public class ApiSettings : IApiSettings
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }

        public string ChildCollectionName { get; set; }
        public string ExaminationCollectionName { get; set; }
        public string PersonalEventCollectionName { get; set; }
        public string MedicalEventCollectionName { get; set; }
    }

    public interface IApiSettings
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }

        string ChildCollectionName { get; set; }
        string ExaminationCollectionName { get; set; }
        string PersonalEventCollectionName { get; set; }
        string MedicalEventCollectionName { get; set; }
    }
}
