using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildHealthBook.Notification.Service.Settings
{
    public class SmtpSettings
    {
        public string host { get; set; }
        public string PortNumber { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string EnableSsl { get; set; }
    }
}
