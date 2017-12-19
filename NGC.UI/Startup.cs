
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NGC.DAL.Base;
using NGC.UI.BllContainers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net;

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
            services.Configure<FormOptions>(x=> {
                x.MultipartBodyLengthLimit = long.MaxValue;
                x.ValueLengthLimit = int.MaxValue;
            });
            BLL.BLLConfiguration.Configure(services);
            services.Configure<MerakiConfiguration>(Configuration);
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.ConfigureBLLContainers();
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
                AutomaticChallenge = true,
                Events = new CookieAuthenticationEvents()
                {
                    OnRedirectToLogin = ctx =>{
                        if(ctx.Request.Path.StartsWithSegments("/api") && 
                        ctx.Response.StatusCode == (int)HttpStatusCode.OK)
                        {
                            ctx.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        }
                        else
                        {
                            ctx.Response.Redirect(ctx.RedirectUri);
                        }
                        return Task.FromResult(0);
                    }
                }
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
