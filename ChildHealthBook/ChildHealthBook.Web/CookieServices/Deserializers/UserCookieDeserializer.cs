using ChildHealthBook.Web.Models.Session;
using Newtonsoft.Json;

namespace ChildHealthBook.Web.CookieServices
{
    public class UserCookieDeserializer : ICookieDeserializer<AuthUserSession>
    {
        public AuthUserSession Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<AuthUserSession>(json);
        }
    }
}
