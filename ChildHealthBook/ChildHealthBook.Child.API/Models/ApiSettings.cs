namespace ChildHealthBook.Child.API.Models
{
    public class ApiSettings : IApiSettings
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }

        public string ChildCollectionName { get; set; }
    }

    public interface IApiSettings
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }

        string ChildCollectionName { get; set; }
    }
}
