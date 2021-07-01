using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChildHealthBook.Web.Services
{
    public class IdentityService
    {
        HttpClient _httpClient;
        public IdentityService()
        {
            _httpClient = new HttpClient();
        }


    }
}
