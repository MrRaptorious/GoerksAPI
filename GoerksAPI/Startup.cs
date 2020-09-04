using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using GoerksAPI.Models;
using Microsoft.EntityFrameworkCore.Storage;
using GoerksAPI.Models.Contexts;
using System.Globalization;

namespace GoerksAPI
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
            //services.AddDbContext<TodoContext>(opt =>
            //    opt.UseInMemoryDatabase("TodoList"));


            ConfigureDBContext(services);


            services.AddControllers();


        }

        private void ConfigureDBContext(IServiceCollection services)
        {
            string connectionString = "Filename=MyDatabase.db";
            services.AddDbContext<TodoContext>(opt =>
                 opt.UseSqlite(connectionString));
            services.AddDbContext<UserContext>(opt =>
                opt.UseSqlite(connectionString));
            services.AddDbContext<ActivityContext>(opt =>
                opt.UseSqlite(connectionString));
            services.AddDbContext<ActivityCatalogContext>(opt =>
                opt.UseSqlite(connectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
