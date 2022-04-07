using System;
using System.IO;
using System.Reflection;
using AspNetCore.Yandex.ObjectStorage.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace studash_service
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
            services.AddControllers();

            string? bucketName = Environment.GetEnvironmentVariable("BUCKET");
            string? accessKey = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID");
            string? secretKey = Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY");
            string? region = Environment.GetEnvironmentVariable("REGION");
            services.AddYandexObjectStorage(options =>
                                            {
                                                options.BucketName = bucketName;
                                                options.AccessKey = accessKey;
                                                options.SecretKey = secretKey;
                                                options.Location = region;
                                            });
            
            services.AddSwaggerGen(swaggerOptions =>
                                   {
                                       swaggerOptions.EnableAnnotations();
                                       swaggerOptions.SwaggerDoc("v1",
                                                    new OpenApiInfo { Title = "studash_service", Version = "v1" });
                                   });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "studash_service v1"));
            }
            app.UseCors(x => x
                             .AllowAnyMethod()
                             .AllowAnyHeader()
                             .SetIsOriginAllowed(origin => true) // allow any origin
                             .AllowCredentials());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}