using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AnanasCore;
using System.Reflection;
using AnanasSQLite;
using GoerksAPI.Middleware;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace GoerksAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(); 
            services.AddTokenAuthentication(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            ConfigureAnanas();
        }


        private void ConfigureAnanas()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            var types = CollectTypes();
            string connectionString = configuration.GetConnectionString("SQLiteDB");
            AnanasApplication app = AnanasApplication.GetApplication();
            ApplicationSubManager sqlitemanager = new ApplicationSubManager(new DependencyConfigurationSQLite(), connectionString);

            app.RegisterApplicationSubManager("localManager", sqlitemanager, true);

            foreach (var type in types)
            {
                sqlitemanager.RegisterType(type);
            }

            app.Start();
        }

        /// <summary>
        /// Collects all DB types from the executing assembly
        /// </summary>
        /// <returns></returns>
        private IList<Type> CollectTypes()
        {
            List<Type> typeList = new List<Type>();

            Assembly mscorlib = GetType().Assembly;
            foreach (Type type in mscorlib.GetTypes())
            {
                if (typeof(PersistentObject).IsAssignableFrom(type))
                    typeList.Add(type);
            }

            return typeList;
        }
    }
}
