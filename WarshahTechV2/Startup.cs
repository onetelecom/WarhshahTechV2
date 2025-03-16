using BL.Infrastructure;
using BL.Security;
using DL.DBContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Model.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using DL.Mapping;
using DL.MailModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;


using System.IO;

using BL.Repositories;
using HELPER;
using MailReader;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Http.Features;
using Helper;
using Helper.Triggers;
using System.Diagnostics;
using Microsoft.AspNetCore.SignalR;
using WarshahTechV2.Models;
using Hangfire;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using KSAEinvoice;

namespace WarshahTechV2
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

            Debug.WriteLine("Simulating heavy I/O bound work");



            services.AddSignalR().AddMessagePackProtocol();



            //var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            //var builder = WebApplication.CreateBuilder(args);

            //builder.Services.AddCors(options =>
            //{
            //    options.AddPolicy(MyAllowSpecificOrigins,
            //                          policy =>
            //                          {
            //                              policy.WithOrigins("http://example.com",
            //                                                  "http://www.contoso.com")
            //                                                  .AllowAnyHeader()
            //                                                  .AllowAnyMethod();
            //                          });
            //});


            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {

                ////builder.WithOrigins("http://example.com",
                ////                              "http://www.contoso.com")
                //builder.WithOrigins("https://warshahtech.sa",
                //                                              "http://localhost:4200")
               builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();//.WithExposedHeaders("*");
            }));




            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            services.AddTransient<IMailService, MailService>();
            services.AddTransient<IBoxNow, BoxNow>();
            services.AddTransient<ISMS, SMS>();
            services.AddTransient<IMailRepository, MailRepository>();
            services.AddTransient<ILog, LogService>();
            services.AddTransient<ISerialService, SerialService>();
            services.AddTransient<ILoyalityService, LoyalityService>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<WarshahTechContext, WarshahTechContext>();
            services.AddSingleton<HtmlEncoder>(HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin,
                                            UnicodeRanges.Arabic }));


            //services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("ConnectionString")) );
            //services.AddHangfireServer();





            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingConfigration());
                // mc.AddGlobalIgnore("CreatedOn");
                // mc.AddGlobalIgnore("UpdatedOn");
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);





            services.AddDbContext<AppDBContext>(options =>
            {
                GlobalVariables.connectionString = Configuration.GetConnectionString("connectionString");
                GlobalVariables.Url_Api = Configuration.GetConnectionString("Url_APITax");
                options.UseSqlServer(Configuration.GetConnectionString("connectionString"),
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure();
                });
            });
            
           



            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            #region Configure Session
            services.AddDistributedMemoryCache();

           



            services.AddMvc().AddSessionStateTempDataProvider();
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSession(
                options =>
                {
                    options.Cookie.IsEssential = true;
                    options.Cookie.HttpOnly = true;
                    options.IdleTimeout = TimeSpan.FromHours(10);
                }
            );
            #endregion
            #region API Token Config

            services.Configure<TokenManagement>(Configuration.GetSection("tokenManagement"));
            services.Configure<MyConfig>(Configuration.GetSection("MyConfig"));
            var token = Configuration.GetSection("tokenManagement").Get<TokenManagement>();

            services.AddAuthentication(jwtBearerDefaults =>
            {
                jwtBearerDefaults.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                jwtBearerDefaults.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(token.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                    // ValidIssuer = token.Issuer,
                    // ValidAudience = token.Audience,

                };
            });



            

            services.AddTransient<IUserManagementService, UserManagementService>();
            services.AddTransient<ICheckUniqes, ChekUniqeSer>();
            services.AddTransient<ISubscribtionCheckService, SubscribtionCheckService>();
           // services.AddTransient<Hub<IChatHub>, ChatHub>();

            services.AddTransient<ISubscribtionsWarshahTech, SubscribtionsWarshahTech>();
            services.AddHostedService<MyScheduler>();
            services.AddTransient<IAuthenticateService,TokenAuthenticationService>();

            #endregion
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));

            services.AddControllers();

            services.AddSwaggerGen(config => {
                config.SwaggerDoc("v1", new OpenApiInfo() { Title = "WebAPI", Version = "v1" });
                config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });
                config.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
                app.UseExceptionHandler("/api/Log/HandleErorr");
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
            }
            InitializeDatabase(app);
            app.UseDeveloperExceptionPage();
            // app.UseElmah();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));


            app.UseSwagger(options =>
            {
                options.SerializeAsV2 = true;
            });

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/info/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = "info/swagger";
            });

            app.UseHttpsRedirection();

            //app.UseHangfireDashboard();
            //app.UseMyMiddleware();

            app.UseAuthentication();



            app.UseRouting();
            app.UseCors("MyPolicy");
            app.UseStaticFiles();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/signalr");
              
                endpoints.MapControllers();
            });
        }
        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<AppDBContext>().Database.Migrate();
               
    }
        }
    }
}
