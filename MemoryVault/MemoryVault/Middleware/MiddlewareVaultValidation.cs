using MemoryVault.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace MemoryVault.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class MiddlewareVaultValidation
    {
        private readonly RequestDelegate _next;

        public MiddlewareVaultValidation(RequestDelegate next)
        {
            
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            
            if (httpContext.Request.Method == "POST")
            {
                string bodyStr = "";
                using (var reader = new StreamReader(httpContext.Request.Body, Encoding.UTF8, true, 1024, true))
                {
                    bodyStr = await reader.ReadToEndAsync();
                }
                Guid guid;
                var vaultItem = JsonSerializer.Deserialize<Vault>(bodyStr);
                if (!Guid.TryParse(vaultItem.value,out guid)) {

                    httpContext.Response.StatusCode = 502;
                    
                    return;
    
                }//end of if
            }
            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class VaultValidationExtensions
    {
        public static IApplicationBuilder UseVaultValidation(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MiddlewareVaultValidation>();
        }
    }
}
