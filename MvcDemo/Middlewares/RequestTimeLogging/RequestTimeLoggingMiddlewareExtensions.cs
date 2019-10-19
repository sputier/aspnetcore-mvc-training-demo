using Microsoft.AspNetCore.Builder;

namespace MvcDemo.Middlewares.RequestTimeLogging
{
    public static class RequestTimeLoggingMiddlewareExtensions
    {
        // Méthode d'extension sur IApplicationBuilder pour utilisation dans Startup
        // Pour ne pas avoir à importer de namespace supplémentaire dans Startup, 
        // on peut mettre la méthode d'extensiond ans le namespace Microsoft.AspNetCore.Builder
        public static IApplicationBuilder UseRequestTimeLogging(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestTimeLoggingMiddleware>();
        }
    }
}
