using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ServerRentCar.Models;
using ServerRentCar.Services;
using ServerRentCar.Utils;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ServerRentCar
{
    public class Startup
    {
        
        private readonly IWebHostEnvironment _env;
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            //test = env.EnvironmentName;

            //configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json")
            //.AddJsonFile("appsettings." + env.EnvironmentName + ".json").Build();

            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSwaggerGen(options =>
         {
             options.SwaggerDoc("v1", new OpenApiInfo
             {
                 Title = "RentCar API",
                 Version = "v1"

             });
             var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
             var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
             options.IncludeXmlComments(xmlPath);
         });
            services.AddDbContext<rentdbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("RentCarDb")));
            services.AddScoped<DataAautoMapper>();
            services.AddScoped<UserService>();
            services.AddScoped<RecordService>();
            services.AddScoped<AuthService>();
            services.AddScoped<CarsService>();
            services.AddSingleton<IConfiguration>(Configuration);

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                     .AllowAnyHeader();
                                  });
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseSwagger();
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "RentCar API V1");
            //});

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Rent Car API");
                options.RoutePrefix = "swagger";
            });
        }
    }
}
