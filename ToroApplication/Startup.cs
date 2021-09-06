using DataAccess;
using DataAccess.Repository;
using Domain.Model.Interfaces;
using Domain.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Security;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;

namespace ToroApplication
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
            services.AddCors();
            services.AddControllers()
                .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddLogging(c =>
            {
                c.ClearProviders();
                c.AddConsole();
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Toro Application API",
                    Description = "This API is used to manage investments of Toro`s Clients",

                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "JWT with Bearer Token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(
                    new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });


            //Add DbContext
            services
                .AddDbContext<InvestmentsDbContext>(c =>
                c.UseSqlServer(Configuration.GetConnectionString("InvestmentsContext")));
            //

            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            services
                .AddAuthentication(c =>
                {
                    c.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    c.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(c =>
                {
                    c.RequireHttpsMetadata = false;
                    c.SaveToken = true;
                    c.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,

                    };
                });

            //Add Services DI
            services.AddScoped<IUnitOfWork, UnityOfWork>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<IUserPositionService, UserPositionService>();
            services.AddScoped<IShareService, ShareService>();
            services.AddScoped<IOrderService, OrderService>();
            //

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger, InvestmentsDbContext investmentsDbContext)
        {
            EnsureDbMigration(investmentsDbContext, logger);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors(c => c.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Toro API");
                c.RoutePrefix = string.Empty;
            });
        }

        private void EnsureDbMigration(InvestmentsDbContext dbContext, ILogger<Startup> logger, int tentatives = 1)
        {
            logger.LogInformation($"Attempting to Migrate Database. Tentative: {tentatives}");
            int count = tentatives;
            try
            {
                dbContext.Database.Migrate();
                logger.LogInformation($"Migrate Database - Success.");
            }
            catch (Exception ex)
            {
                Thread.Sleep(30000);
                count += 1;
                if (count > 6)
                    throw ex;
                EnsureDbMigration(dbContext, logger, count);
            }
        }
    }
}
