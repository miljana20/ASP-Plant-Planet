using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using API.DTO;
using API.ErrorLogging;
using API.Jwt;
using Application.Logging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.UseCases.Queries;
using Implementation.UseCases.Queries;
using Application.UseCases.Commands;
using Implementation.UseCases.Commands;
using Implementation.Validators;

namespace API.Extensions
{
    public static class ContainerExtensions
    {
        public static void AddLogger(this IServiceCollection services)
        {
            services.AddTransient<IErrorLogger>(x =>
            {
                var accesor = x.GetService<IHttpContextAccessor>();

                if (accesor == null || accesor.HttpContext == null)
                {
                    return new ConsoleErrorLogger();
                }

                var logger = accesor.HttpContext.Request.Headers["Logger"].FirstOrDefault();

                if (logger == "Console")
                {
                    return new ConsoleErrorLogger();
                }
                else
                {
                    return new BugSnagErrorLogger(x.GetService<Bugsnag.IClient>());
                }
            });
        }

        public static void AddValidators(this IServiceCollection services)
        {
            services.AddTransient<RegisterValidator>();
            services.AddTransient<CreateCartValidator>();
            services.AddTransient<CreatePlantValidator>();
            services.AddTransient<CreateHabitatValidator>();
            services.AddTransient<CreateSpeciesValidator>();
            services.AddTransient<CreateDurabilityValidator>();
            services.AddTransient<AddToCartValidator>();
            services.AddTransient<OrderValidator>();
            services.AddTransient<UpdatePlantValidator>();
            services.AddTransient<UpdateHabitatValidator>();
            services.AddTransient<UpdateDurabilityValidator>();
            services.AddTransient<UpdateSpeciesValidator>();
            services.AddTransient<UpdateUserValidator>();
        }

        public static void AddGetQueries(this IServiceCollection services)
        {
            services.AddTransient<IGetUserQuery, EfGetUsersQuery>();
            services.AddTransient<IGetPlantQuery, EfGetPlantsQuery>();
            services.AddTransient<IGetDurabilityQuery, EfGetDurabilitiesQuery>();
            services.AddTransient<IGetHabitatQuery, EfGetHabitatsQuery>();
            services.AddTransient<IGetSpeciesQuery, EfGetSpeciesQuery>();
            services.AddTransient<IGetRoleQuery, EfGetRoleQuery>();
            services.AddTransient<IGetCartQuery, EfGetCartQuery>();
        }

        public static void AddFindQueries(this IServiceCollection services)
        {
            services.AddTransient<IFindUserQuery, EfFindUserQuery>();
            services.AddTransient<IFindPlantQuery, EfFindPlantQuery>();
            services.AddTransient<IFindDurabilityQuery, EfFindDurabilityQuery>();
            services.AddTransient<IFindHabitatQuery, EfFindHabitatQuery>();
            services.AddTransient<IFindSpeciesQuery, EfFindSpeciesQuery>();
            services.AddTransient<IFindRoleQuery, EfFindRoleQuery>();
            services.AddTransient<IFindCartQuery, EfFindCartQuery>();
        }
        
        public static void AddDeleteCommands(this IServiceCollection services)
        {
            services.AddTransient<IDeleteUserCommand, EfDeleteUserCommand>();
            services.AddTransient<IDeletePlantCommand, EfDeletePlantCommand>();
            services.AddTransient<IDeleteCartCommand, EfDeleteCartCommand>();
            services.AddTransient<IDeleteDurabilityCommand, EfDeleteDurabilityCommand>();
            services.AddTransient<IDeleteHabitatCommand, EfDeleteHabitatCommand>();
            services.AddTransient<IDeleteSpeciesCommand, EfDeleteSpeciesCommand>();
            services.AddTransient<IRemoveFromCartCommand, EfRemoveFromCartCommand>();
        }

        public static void AddUpdateCommands(this IServiceCollection services)
        {
            services.AddTransient<IOrderCommand, EfOrderCommand>();
            services.AddTransient<IUpdatePlantCommand, EfUpdatePlantCommand>();
            services.AddTransient<IUpdateDurabilityCommand, EfUpdateDurabilityCommand>();
            services.AddTransient<IUpdateSpeciesCommand, EfUpdateSpeciesCommand>();
            services.AddTransient<IUpdateHabitatCommand, EfUpdateHabitatCommand>();
        }
        public static void AddCreateCommands(this IServiceCollection services)
        {
            services.AddTransient<IAddToCartCommand, EfAddToCartCommand>();
            services.AddTransient<IRegisterUserCommand, EfRegisterUserCommand>();
            services.AddTransient<ICreateCartCommand, EfCreateCartCommand>();
            services.AddTransient<ICreatePlantCommand, EfCreatePlantCommand>();
            services.AddTransient<ICreateDurabilityCommand, EfCreateDurabilityCommand>();
            services.AddTransient<ICreateSpeciesCommand, EfCreateSpeciesCommand>();
            services.AddTransient<ICreateHabitatCommand, EfCreateHabitatCommand>();
        }



        public static void AddJwt(this IServiceCollection services, AppSettings settings)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = settings.Jwt.Issuer,
                    ValidateIssuer = true,
                    ValidAudience = "Any",
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Jwt.SecretKey)),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                cfg.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {

                        var header = context.Request.Headers["Authorization"];

                        var token = header.ToString().Split("Bearer ")[1];

                        var handler = new JwtSecurityTokenHandler();

                        var tokenObj = handler.ReadJwtToken(token);

                        string jti = tokenObj.Claims.FirstOrDefault(x => x.Type == "jti").Value;

                        ITokenStorage storage = context.HttpContext.RequestServices.GetService<ITokenStorage>();

                        bool isValid = storage.TokenExists(jti);

                        if(!isValid)
                        {
                            context.Fail("Token is not valid.");
                        }

                        return Task.CompletedTask;
                    }
                };
            });
        }
    }
}
