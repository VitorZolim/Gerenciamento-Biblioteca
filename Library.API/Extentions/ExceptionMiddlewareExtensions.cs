using Library.API.Extentions.ViewModels;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace Library.API.Extentions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment env, ILogger logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        // Registra o erro no arquivo .txt antes de responder ao usuário
                        logger.LogError($"Erro capturado pelo Middleware: {contextFeature.Error}");

                        var isDev = env.IsDevelopment();

                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = isDev ? contextFeature.Error.Message : "Um erro interno inesperado ocorreu.",
                            Trace = isDev ? contextFeature.Error.StackTrace : null
                        }.ToString());
                    }
                });
            });
        }
    }
}