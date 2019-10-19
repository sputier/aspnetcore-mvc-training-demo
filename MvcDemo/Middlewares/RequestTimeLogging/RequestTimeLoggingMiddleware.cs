using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MvcDemo.Middlewares.RequestTimeLogging
{
    /// <summary>
    /// Middleware qui permet d'enregistrer dans un fichier text le temps d'exécution de chaque requête.
    /// Les seules informations écrites concernent la méthode (GET, POST, etc...) et l'URL complète
    /// </summary>
    public class RequestTimeLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private const string logFilename = "requests.txt";

        // Le constructeur du middleware DOIT avoir un paramètre de type RequestDelegate
        public RequestTimeLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // Le middleware DOIT avoir un méthode Invoke (ou InvokeAsync) 
        // qui retourne un objet Task et dont le premier paramètre est de type HttpContext
        public async Task InvokeAsync(HttpContext context)
        {
            var start = DateTime.Now;

            // Exécution du middleware suivants
            await _next(context);

            var end = DateTime.Now;

            // GetDisplayUrl est une méthode d'extension qui nécessite d'ajouter 
            // using Microsoft.AspNetCore.Http.Extensions;
            var text = $"{context.Request.Method.ToUpper()} {context.Request.GetDisplayUrl()} {Environment.NewLine}" +
                       $"exécutée en {(end - start).TotalMilliseconds} ms {Environment.NewLine}";

            await File.AppendAllTextAsync(logFilename, text);
        }
    }
}
