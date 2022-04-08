using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebHelpers.Mvc5.Enum;

namespace WebHelpers.Mvc5.Demo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddControllersWithViews();
            services.AddWebOptimizer(pipeline =>
            {
                pipeline.AddCssBundle("/css/bundles.css", new string[]
                {
                    "/css/bootstrap.min.css",
                    "/css/bootstrap-select.css",
                    "/css/bootstrap-datepicker3.min.css",
                    "/css/font-awesome.min.css",
                    "/css/icheck/blue.css",
                    "/css/AdminLTE.css",
                    "/css/skins/skin-blue.css",
                    "/css/site.css"
                });
                pipeline.AddJavaScriptBundle("/js/bundles.js", new string[]
                {
                    "/lib/jquery/dist/jquery-3.3.1.js",
                    "/lib/bootstrap/dist/bootstrap.min.js",
                    "/lib/bootstrap-select/dist/bootstrap-select.js",
                    "/lib/fastclick/dist/fastclick.js",
                    "/lib/slimscroll/dist/jquery.slimscroll.js",
                    "/lib/moment/dist/moment.js",
                    "/lib/datepicker/dist/bootstrap-datepicker.js",
                    "/lib/icheck/dist/icheck.js",
                    "/lib/validator/dist/validator.js",
                    "/lib/inputmask/dist/jquery.inputmask.bundle.js",
                    "/js/adminlte.js",
                    "/js/init.js"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseEnum();

            app.UseWebOptimizer();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
