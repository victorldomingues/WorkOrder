using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkOrder.Infra.Contexts;
using WorkOrder.Shared;

namespace WorkOrder.Api
{
    /// <summary>
    ///     Startup para criar banco de dados
    /// </summary>
    public class DbStartup
    {
        /// <inheritdoc />
        public DbStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <inheritdoc />
        public IConfiguration Configuration { get; }

        /// <inheritdoc />
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddDbContext<SaasContext>(options => options.UseSqlServer(Settings.ConnectionString));
        }

        /// <inheritdoc />
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
