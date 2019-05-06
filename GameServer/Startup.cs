using GameServer.Domain;
using GameServer.Domain.Database;
using GameServer.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using Swashbuckle.AspNetCore.Swagger;

namespace GameServer
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
            services.AddMvc(options => options.Filters.Add<ErrorHandler>())
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(x => x.SerializerSettings.Converters.Add(new StringEnumConverter()));
            services.AddDbContext<GameServerDbContext>(options =>
            {
                options.UseNpgsql(Configuration["DbConnectionString"],
                    c => c.MigrationsAssembly("GameServer"));
            });
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("serverapi", new Info { Title = "GameServerApi", Version = "serverapi" });
                opt.DescribeAllEnumsAsStrings();
            });
            services.AddDomainServices();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            app.UseSwagger(c =>
                {
                    c.RouteTemplate = ("api/{documentName}/swagger/swagger.json");
                }
            );
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/api/serverapi/swagger/swagger.json", "GameServerApi");
                options.RoutePrefix = "api/help";
            });
            app.UseMvc();
        }
    }
}
