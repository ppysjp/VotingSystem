using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSystem.Application;
using VotingSystem.Database;

namespace VotingSystem.UI
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
            //services.AddSingleton<Service201>(); 
            services.AddSingleton<IVotingPollFactory, VotingPollFactory>();
            services.AddScoped<VotingPollInteractor>();
            services.AddScoped<IVotingSystemPersistance, VotingSystemPersistance>();
            services.AddControllersWithViews();
            services.AddRazorPages();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //app.UseMiddleware<CustomMiddleware>(); 


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapDefaultControllerRoute();

                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World");
                //});

                //endpoints.MapRazorPages();
            });
        }
    }


    public class Service201 
    {
        public int GetCode() => 201;
    }

    public class CustomMiddleware 
    {
        private readonly RequestDelegate _request;

        public CustomMiddleware(RequestDelegate request)
        {
            _request = request;
        }

        public Task Invoke(HttpContext context, Service201 service) 
        {
            // Request is coming in

            context.Response.StatusCode = service.GetCode();
            context.Response.ContentType = "application/json";
            return _request(context);

            // request is going out.

        }

    }

}





