using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using CleanArchitecture.Application.Comandos.AbrirConta;
using CleanArchitecture.Application.Comandos.Creditar;
using CleanArchitecture.Application.Comandos.Debitar;
using CleanArchitecture.Application.Comandos.Transferir;
using CleanArchitecture.Application.Repositorios;
using CleanArchitecture.Infrastructure.EntityFrameworkDataAccess;
using CleanArchitecture.Infrastructure.EntityFrameworkDataAccess.Repositorios;
using CleanArchitecture.Framework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System;
using Microsoft.Extensions.Options;
using CleanArchitecture.Infrastructure;
using CleanArchitecture.Application.Comandos.DetalharConta;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.AspNetCore.Http;
using CleanArchitecture.Domain.Seguranca;

namespace CleanArchitecture.Contas.Api
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

            // Ativa o uso do token como forma de autorizar o acesso
            // a recursos deste projeto
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            });

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(DomainExceptionFilter));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            ConfigureDependencyInjection(services);

            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("oauth2", new ApiKeyScheme
                {
                    Description = "Exemplo: \"bearer {token}\"",
                    In = "header",
                    Name = "Authorization",
                    Type = "apiKey"
                });

                c.OperationFilter<SecurityRequirementsOperationFilter>();

                c.SwaggerDoc("v1",
                  new Info
                  {
                      Title = "Teste DBServer - Contas Microservice",
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
            #region Application

            services.AddTransient<IAbrirContaUseCase, AbrirContaUseCase>();
            services.AddTransient<IDetalharContaUseCase, DetalharContaUseCase>();
            services.AddTransient<ICreditarUseCase, CreditarUseCase>();
            services.AddTransient<IDebitarUseCase, DebitarUseCase>();
            services.AddTransient<ITransferirUseCase, TransferirUseCAse>();

            #endregion

            #region Infrastructure

            services.AddDbContext<Context>(opt => opt.UseInMemoryDatabase("TesteDbServer"));
            services.AddTransient<ContextInitializer>();
            services.AddTransient<IClienteRepository, ClienteRepository>();
            services.AddTransient<IContaCorrenteRepository, ContaCorrenteRepository>();

            #endregion
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IUsuarioAutenticado, UsuarioAutenticado>();
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Teste DBServer - Contas Microservice");
            });
        }
    }
}
