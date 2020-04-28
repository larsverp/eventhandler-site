using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RockStar_IT_Events
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePagesWithReExecute("/ErrorMessage/{0}");
            }

            app.UseRouting();
            app.UseStaticFiles();

            app.UseEndpoints(routes =>
            {
                routes.MapControllerRoute("Default", "{controller=Event}/{action=index}/{id?}");
            });
            app.UseFileServer();
        }
    }
}
