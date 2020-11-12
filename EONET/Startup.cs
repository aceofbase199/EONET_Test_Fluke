using AutoMapper;
using EONET.Api.Client;
using EONET.BL.Abstraction;
using EONET.BL.MappingProfiles;
using EONET.BL.Services;
using EONET.Web.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Net.Http;
using System.Text.Json.Serialization;

namespace EONET
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
            services.AddAutoMapper(typeof(EventMappingProfile));

            services.AddTransient<IEventService, EventService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddSingleton<IHttpEonetClient, HttpEonetClient>();

            var apiClientSection = Configuration.GetSection("ApiClient");
            services.AddHttpClient("eonetClient", c =>
            {
                c.BaseAddress = new Uri(apiClientSection.GetValue<string>("Url"));
            }).AddPolicyHandler(GetRetryPolicy());

            services.AddControllersWithViews();

            services.AddMvc().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.IgnoreNullValues = true;
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "reactApp",
                    pattern: "api/{controller}/{action}",
                    defaults: "{controller}/{action}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{*home}",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }

        private IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            var section = Configuration.GetSection("ApiClient");
            var retryCount = section.GetValue<int>("RetryCount");

            return HttpPolicyExtensions.HandleTransientHttpError()
                                       .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                                       .WaitAndRetryAsync(retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }
    }
}