using Microsoft.AspNetCore.Mvc;

namespace School.API.Tools
{
    public class ApiKeyAttribute : ServiceFilterAttribute
    {
        public ApiKeyAttribute()
            : base(typeof(ApiKeyAuthFilter))
        {
        }
    }
}
