using System;
using System.IO;
using DemoStore.Domain.StoreContext.Handlers;
using DemoStore.Domain.StoreContext.Repositories;
using DemoStore.Domain.StoreContext.Services;
using DemoStore.Infra.DataContexts;
using DemoStore.Infra.StoreContext.Repositories;
using DemoStore.Infra.StoreContext.Services;
using DemoStore.Shared;
using Elmah.Io.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace DemoStore.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public static IConfiguration Configuration { get; set; }
        public void ConfigureServices(IServiceCollection services)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            //services.AddApplicationInsightsTelemetry(Configuration);
            services.AddMvc();
            services.AddResponseCompression();

            services.AddScoped<MssqlDataContext, MssqlDataContext>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<CustomerHandler, CustomerHandler>();
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info { Title = "Demo Store", Version = "v1" });
            });
            services.AddElmahIo(o =>
            {
                o.ApiKey = "7429e57fadb141d7a77f3f0312ed8ad8";
                o.LogId = new Guid("55c9e328-dbff-475f-bb27-a1d54503e1c7");
            });

            Settings.ConnectionString = $"{Configuration["connectionString"]}";
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();                

            app.UseMvc();   
            app.UseResponseCompression();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo Store - V1");
            });
            app.UseElmahIo();
        }
    }
}
