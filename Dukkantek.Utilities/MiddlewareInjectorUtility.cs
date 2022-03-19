using System;
using System.Collections.Generic;
using System.Text;
using Gezira.Infrastructure;
using Microsoft.AspNetCore.Builder;

namespace Gezira.Utilities
{
    public static class MiddlewareInjectorUtility
    {
        public static void InjectMiddlewares(IApplicationBuilder app)
        {
            //app.UseMiddleware<ExceptionCatchMiddleware>();
            //app.UseMiddleware<RequestLoggingMiddleware<GeziraContext>>();
            //app.UseMiddleware<FileDownloaderMiddleware>();
            //app.UseImageDownloader();
            //app.UseStaticFiles();
        }
    }
}
