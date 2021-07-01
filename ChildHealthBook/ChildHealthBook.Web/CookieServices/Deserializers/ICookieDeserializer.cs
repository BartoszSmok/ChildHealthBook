namespace ChildHealthBook.Web.CookieServices
{
    public interface ICookieDeserializer<T>
    {
        T Deserialize(string json);
    }
}
