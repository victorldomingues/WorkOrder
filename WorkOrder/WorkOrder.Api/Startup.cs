using System.IO;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using WorkOrder.Api.Security;
using WorkOrder.Api.Tenant;
using WorkOrder.Application.AccountContext.Handlers;
using WorkOrder.Application.AccountContext.Repositories;
using WorkOrder.Application.AccountContext.Services;
using WorkOrder.Application.CustomFormsContext.Handlers;
using WorkOrder.Application.CustomFormsContext.Repositories;
using WorkOrder.Application.SharedContext.Repositories;
using WorkOrder.Infra.Contexts;
using WorkOrder.Infra.Contexts.AccountContext.Repositories;
using WorkOrder.Infra.Contexts.AccountContext.Services;
using WorkOrder.Infra.Contexts.CustomFormsContext.Repositories;
using WorkOrder.Infra.Contexts.SharedContext;
using WorkOrder.Infra.Transactions;
using WorkOrder.Shared;
using WorkOrder.Shared.Entities;
using Swashbuckle.AspNetCore.Swagger;

namespace WorkOrder.Api
{
    public class Startup
    {
        private readonly SymmetricSecurityKey _ssk;
        private readonly string AUDIENCE;

        private readonly string ISSUER;
        private readonly string SECRET_KEY;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();


            ISSUER = Configuration["Issuer"];
            AUDIENCE = Configuration["Audience"];
            SECRET_KEY = Configuration["SecretKey"];

            //Settings.ConnectionString = Configuration["DbConnection"];
            //Settings.SendgridApiKey = Configuration["SendGridApiKey"];

            Settings.SecurityKey = Configuration["SecurityKey"];


            _ssk = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SECRET_KEY));
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("User", policy => policy.RequireClaim("Saas", "User"));
                options.AddPolicy("Owner", policy => policy.RequireClaim("Saas", "Owner"));
                options.AddPolicy("Manager", policy => policy.RequireClaim("Saas", "Manager"));
            });


            services.Configure<TokenOptions>(options =>
            {
                options.Issuer = ISSUER;
                options.Audience = AUDIENCE;
                options.SigningCredentials = new SigningCredentials(_ssk, SecurityAlgorithms.HmacSha256Signature);
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = ISSUER,
                        ValidAudience = AUDIENCE,
                        RequireExpirationTime = true,
                        IssuerSigningKey = _ssk
                    };
                });

            services.AddMultitenancy<AppTenant, AppTenantResolver>();

            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(Settings.ConnectionString));
            services.AddDbContext<SaasContext>(options => options.UseSqlServer(Settings.ConnectionString));

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<ITenantRepository, TenantRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddTransient<ICreateAccountEmailService, CreateAccountEmailService>();
            services.AddTransient<IAddressRepository, AddressRepository>();


            services.AddTransient<ICustomFieldRepository,CustomFieldRepository>();
            services.AddTransient<ICustomFieldOptionRepository, CustomFieldOptionRepository>();
            services.AddTransient<ICustomFormRepository, CustomFormRepository>();
            services.AddTransient<IFormPageComponentRepository, FormPageComponentRepository>();
            services.AddTransient<IPageComponentRepository, PageComponentRepository>();
            services.AddTransient<ICustomFormAnswerRepository, CustomFormAnswerRepository>();
            services.AddTransient<ICustomFieldAnswerRepository, CustomFieldAnswerRepository>();


            services.AddTransient<AccountHandler, AccountHandler>();
           
            services.AddTransient<CustomFormsHandler, CustomFormsHandler>();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Saas ",
                        Version = "v1",
                        Description = "Saas ",
                        Contact = new Swashbuckle.AspNetCore.Swagger.Contact
                        {
                            Name = "Saas ",
                            Url = "http://victorldomingues.net/"
                        }
                    });

                var applicationPath =
                    PlatformServices.Default.Application.ApplicationBasePath;
                var applicationName =
                    PlatformServices.Default.Application.ApplicationName;
                var pathXmlDock =
                    Path.Combine(applicationPath, $"{applicationName}.xml");

                c.IncludeXmlComments(pathXmlDock);
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseStaticFiles();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseAuthentication();

            app.UseCors(x =>
            {
                x.AllowAnyHeader();
                x.AllowAnyMethod();
                x.AllowAnyOrigin();
            });

            app.UseMultitenancy<AppTenant>();
            app.UseHttpsRedirection();

            app.UseMvc();

            if (env.IsDevelopment())
            {

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Saas  V1");
                });
            }


        }
    }
}
