using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NGC.DAL.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Newtonsoft.Json.Serialization;

namespace NGC.UI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.

           // services.Configure<MvcOptions>(opts=>opts.Filters.Add(new RequireHttpsAttribute()));
            services.AddAuthorization(config =>
            {
                config.AddPolicy("Loged", policy => policy.RequireAssertion(context =>
               !string.IsNullOrWhiteSpace(context.User.Identity.Name)));
            });
            services.AddMvc(setup => {
                
            }).AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
            services.AddDbContext<MerakiContext>((options) =>
            {
            });
            BLL.BLLConfiguration.Configure(services);
            services.Configure<MerakiConfiguration>(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseCookieAuthentication(new CookieAuthenticationOptions() {
                CookieName="MerakiAuthCookie",
                LoginPath = new PathString("/Login"),
                AccessDeniedPath = new PathString("/Forbidden/"),
                AuthenticationScheme = "Loged",
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });
           
            app.UseStaticFiles();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            /*var rwOptions = new RewriteOptions().AddRedirectToHttps();
            app.UseRewriter(rwOptions);*/
        }
    }
}
