using DotNetCoreBlog.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DotNetCoreBlog
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.Configure<AppSettings>(Configuration.GetSection(nameof(AppSettings)));

            ConfigureDatabase(services);

            services.AddTransient<BlogService>();
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // plug in custom middleware - it's important that we do this first to get accurate results
            app.UseMiddleware<Middleware.ProcessingTimeMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseStatusCodePagesWithReExecute("/Home/Error/{0}");
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        protected virtual void ConfigureDatabase(IServiceCollection services)
        {
            var sqlConnection = Configuration.GetConnectionString("BlogContext");
            services.AddDbContext<BlogContext>(options =>
            {
                // TODO: add caching for EF
                options.UseMemoryCache(null);
                options.UseSqlServer(sqlConnection, sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure();
                });
            });
        }
    }
}
