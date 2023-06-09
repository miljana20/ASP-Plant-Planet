
using API.DTO;
using API.Extensions;
using API.Jwt.TokenStorage;
using API.Jwt;
using API.Middleware;
using Application.Logging;
using Application.UseCaseHandling;
using Application.UseCases.Queries;
using Application;
using Bugsnag.AspNet.Core;
using Implementation.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using DataAccess;
using Implementation.UseCases.Queries;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Uploads;
using Implementation.Uploads;

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
            var appSettings = new AppSettings();
            Configuration.Bind(appSettings);
            services.AddTransient<ITokenStorage, InMemoryTokenStorage>();
            services.AddTransient<JwtManager>(x =>
            {
                var context = x.GetService<PlantPlanetContext>();
                var tokenStorage = x.GetService<ITokenStorage>();
                return new JwtManager(context, appSettings.Jwt.Issuer, appSettings.Jwt.SecretKey, appSettings.Jwt.DurationSeconds, tokenStorage);
            });

            services.AddBugsnag(configuration => {
                configuration.ApiKey = appSettings.BugSnagKey;
            });

            services.AddLogger();
            services.AddValidators();

            services.AddGetQueries();
            services.AddFindQueries();
            services.AddDeleteCommands();
            services.AddUpdateCommands();
            services.AddCreateCommands();

            services.AddTransient<PlantPlanetContext>(x =>
            {
                DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
                builder.UseSqlServer("Data Source=DESKTOP-DQVE25R;Initial Catalog=PalntPlanet;Integrated Security=True");
                return new PlantPlanetContext(builder.Options);
            });

            services.AddTransient<QueryHandler>();

            services.AddHttpContextAccessor();
            services.AddScoped<IApplicationActor>(x =>
            {
                var accessor = x.GetService<IHttpContextAccessor>();
                var header = accessor.HttpContext.Request.Headers["Authorization"];

                var data = header.ToString().Split("Bearer ");

                if (data.Length < 2)
                {
                    throw new UnauthorizedAccessException();
                }

                var handler = new JwtSecurityTokenHandler();

                var tokenObj = handler.ReadJwtToken(data[1].ToString());

                var claims = tokenObj.Claims;

                var email = claims.First(x => x.Type == "Email").Value;
                var id = claims.First(x => x.Type == "Id").Value;
                var username = claims.First(x => x.Type == "Username").Value;
                var useCases = claims.First(x => x.Type == "UseCases").Value;

                List<int> useCaseIds = JsonConvert.DeserializeObject<List<int>>(useCases);

                return new JwtActor
                {
                    Email = email,
                    AllowedUseCases = useCaseIds,
                    Id = int.Parse(id),
                    Username = username,
                };
            });

            services.AddTransient<IBase64FileUploader, Base64FileUploader>();
            services.AddTransient<IUseCaseLogger, EfUseCaseLogger>();
            services.AddTransient<ICommandHandler, CommandHandler>();
            services.AddTransient<ISearchLogQuery, EfSearchLogQuery>();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });

            services.AddJwt(appSettings);

            services.AddTransient<IQueryHandler>(x =>
            {
                var actor = x.GetService<IApplicationActor>();
                var logger = x.GetService<IUseCaseLogger>();
                var queryHandler = new QueryHandler();
                var timeTrackingHandler = new TimeTrackingQueryHandler(queryHandler);
                var loggingHandler = new LoggingQueryHandler(timeTrackingHandler, actor, logger);
                var decoration = new AuthorizationQueryHandler(actor, loggingHandler);

                return decoration;
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));


            app.UseHttpsRedirection();

            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();


            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
