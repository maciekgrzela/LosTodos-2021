using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Persistence.Contexts;
using Persistence.Repositories;
using Persistence.Repositories.Interfaces;
using Security;

namespace API
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
            services.AddDbContext<DatabaseContext>(opt => {
                opt.UseSqlServer(Configuration.GetConnectionString("SqlServerDevelopment"), o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
            });
            
            services.AddControllers();

            services.AddSignalR();

            services.AddCors(opt => 
            {
                opt.AddPolicy("DefaultPolicy", policy => 
                {
                    policy
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithExposedHeaders("X-Pagination")
                        .WithExposedHeaders("WWW-Authenticate")
                        .WithOrigins("http://localhost:3000")
                        .AllowCredentials();
                });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1"  });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "bearer",
                    Description = "Please insert JWT token into field"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            var builder = services.AddIdentityCore<AppUser>();
            builder.AddRoles<IdentityRole>();
            builder.AddRoleManager<RoleManager<IdentityRole>>();
            var identityBuilder = new IdentityBuilder(builder.UserType, builder.RoleType, builder.Services);
            identityBuilder.AddEntityFrameworkStores<DatabaseContext>();
            identityBuilder.AddRoles<IdentityRole>();
            identityBuilder.AddRoleManager<RoleManager<IdentityRole>>();
            identityBuilder.AddSignInManager<SignInManager<AppUser>>();

            var key = SecurityKeyGenerator.Instance.GetKey();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options => {
                        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            ValidateActor = true,
                            IssuerSigningKey = key,
                            ValidateAudience = false,
                            ValidateIssuer = false,
                            ValidateLifetime = true,
                            ClockSkew = TimeSpan.Zero,
                            RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
                        };
                        options.Events = new JwtBearerEvents
                        {
                            OnMessageReceived = context => {
                                var accessToken = context.Request.Query["access_token"];
                                var path = context.HttpContext.Request.Path;
                                if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hub"))
                                {
                                    context.Token = accessToken;
                                }

                                return System.Threading.Tasks.Task.CompletedTask;
                            }
                        };
                    });

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserAccessor, UserAccessor>();
            services.AddScoped<IFacebookAccessor, FacebookAccessor>();
            services.AddScoped<IWebTokenGenerator, WebTokenGenerator>();
            services.AddScoped<ITodoService, TodoService>();
            services.AddScoped<ITodoSetService, TodoSetService>();
            services.AddScoped<ITodoRepository, TodoRepository>();
            services.AddScoped<ITodoSetRepository, TodoSetRepository>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<ITodoSetTagRepository, TodoSetTagRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.Configure<FacebookAppSettings>(Configuration.GetSection("Authentication:Facebook"));

            services.AddAutoMapper(typeof(Startup));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("DefaultPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireAuthorization();
            });
        }
    }
}
