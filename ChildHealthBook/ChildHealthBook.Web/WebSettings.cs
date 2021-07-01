using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChildHealthBook.Web
{
    public class WebSettings : IWebSettings
    {
        public string GatewayAPI { get; set; }
    }

    public interface IWebSettings
    {
        string GatewayAPI { get; set; }

    }
}

