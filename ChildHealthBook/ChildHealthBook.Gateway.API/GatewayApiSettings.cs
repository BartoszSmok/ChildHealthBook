﻿namespace ChildHealthBook.Gateway.API
{
    public class GatewayApiSettings : IGatewayApiSettings
    {
        public string StorageConnectionString { get; set; }
        public string AddNewChildEventQueue { get; set; }
        public string AddNewMedicalEventQueue { get; set; }
        public string AddNewPersonalEventQueue { get; set; }
        public string AddNewExaminationQueue { get; set; }
    }

    public interface IGatewayApiSettings
    {
        string StorageConnectionString { get; set; }
        string AddNewChildEventQueue { get; set; }
        string AddNewMedicalEventQueue { get; set; }
        string AddNewPersonalEventQueue { get; set; }
        string AddNewExaminationQueue { get; set; }
    }
}
