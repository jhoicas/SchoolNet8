namespace School.API.Tools
{
   public interface IApiKeyValidation
    {
        bool IsValidApiKey(string userApiKey);
    }
}
