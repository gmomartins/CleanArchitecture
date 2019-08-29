using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using CleanArchitecture.Application.Comandos.Autenticar;
using CleanArchitecture.Application.Repositorios;
using CleanArchitecture.Application.Seguranca;
using CleanArchitecture.Framework;
using CleanArchitecture.Infrastructure;
using CleanArchitecture.Infrastructure.EntityFrameworkDataAccess;
using CleanArchitecture.Infrastructure.EntityFrameworkDataAccess.Repositorios;

namespace CleanArchitecture.Autenticacao.Api
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
            ConfigureJwt(services);

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(DomainExceptionFilter));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            ConfigureDependencyInjection(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                  new Info
                  {
                      Title = "Teste DBServer - Autenticação Microservice",
                      Version = "v1"
                  });

                string caminhoAplicacao =
                    PlatformServices.Default.Application.ApplicationBasePath;
                string nomeAplicacao =
                    PlatformServices.Default.Application.ApplicationName;
                string caminhoXmlDoc =
                    Path.Combine(caminhoAplicacao, $"{nomeAplicacao}.xml");

                c.IncludeXmlComments(caminhoXmlDoc);
            });
        }

        private void ConfigureJwt(IServiceCollection services)
        {
            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(
                Configuration.GetSection("TokenConfigurations"))
                    .Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);


            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                JwtBearerOptionsHelper.Configure(bearerOptions.TokenValidationParameters, signingConfigurations, tokenConfigurations);
            });
        }

        private void ConfigureDependencyInjection(IServiceCollection services)
        {
            services.AddDbContext<Context>(opt => opt.UseInMemoryDatabase("TesteDbServer"));
            services.AddTransient<ContextInitializer>();
            services.AddTransient<IClienteRepository, ClienteRepository>();
            services.AddTransient<IAutenticarUseCase, AutenticarUseCase>();
            services.AddTransient<ITokenProvider, JwtTokenProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Teste DBServer - Autenticação Microservice");
            });
        }
    }
}
