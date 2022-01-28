using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Swashbuckle.AspNetCore.Swagger;
using System.Threading.Tasks;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;
using Gisa.Domain.Interfaces.Service;
using Gisa.Service;
using Gisa.Domain.Interfaces.Repository;
using Gisa.SqlRepository;
using FluentValidation;
using Gisa.Domain;
using Gisa.Domain.Validation;
using System.Globalization;
using Microsoft.Extensions.Caching.Memory;
using Gisa.Domain.Interfaces.Integration;
using Gisa.SAF;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Gisa.WebApi
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
            services.AddControllers();

            var key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("GisaSecret"));
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            #region [ Swagger ]

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "GISA - Gestão Integral da Saúde do Associado",
                        Version = PlatformServices.Default.Application.ApplicationVersion,
                        Description = "GISA - API REST",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact
                        {
                            Name = "PUC Minas"
                        }
                    });

                string pathApplication = PlatformServices.Default.Application.ApplicationBasePath;
                string nameAplication = PlatformServices.Default.Application.ApplicationName;
                string pathXmlDoc = Path.Combine(pathApplication, $"{nameAplication}.xml");

                c.IncludeXmlComments(pathXmlDoc);
            });

            #endregion

            #region [ Integration ]

            services.AddTransient<IEspecialidadeIntegration, EspecialidadeIntegration>();
            services.AddTransient<IConsultarIntegration, ConsultarIntegration>();

            #endregion

            #region [ Services ]

            services.AddTransient<IAtenticacaoService, AtenticacaoService>();
            services.AddTransient<IConsultaService, ConsultaService>();
            services.AddTransient<IConveniadoService, ConveniadoService>();
            services.AddTransient<IEspecialidadeService, EspecialidadeService>();
            services.AddTransient<IPrestadorService, PrestadorService>();
            services.AddTransient<IAssociadoService, AssociadoService>();
            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient<IFluxoService, FluxoService>();
            services.AddTransient<IConsultaFluxoService, ConsultaFluxoService>();
            services.AddTransient<IPrestadorService, PrestadorService>();

            #endregion

            #region [ Repositories ]

            services.AddTransient<IConsultaRepository, ConsultaRepository>();
            services.AddTransient<IConveniadoRepository, ConveniadoRepository>();
            services.AddTransient<IEspecialidadeRepository, EspecialidadeRepository>();
            services.AddTransient<IPrestadorRepository, PrestadorRepository>();
            services.AddTransient<IAssociadoRepository, AssociadoRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<ILocalizacaoRepository, LocalizacaoRepository>();
            services.AddTransient<IFluxoRepository, FluxoRepository>();
            services.AddTransient<IConsultaFluxoRepository, ConsultaFluxoRepository>();
            services.AddTransient<IPrestadorRepository, PrestadorRepository>();

            #endregion

            #region [ Validator ]

            services.AddSingleton<IValidator<Consulta>, ConsultaValidator>();
            services.AddSingleton<IValidator<Conveniado>, ConveniadoValidator>();
            services.AddSingleton<IValidator<Especialidade>, EspecialidadeValidator>();
            services.AddSingleton<IValidator<Prestador>, PrestadorValidator>();
            services.AddSingleton<IValidator<Associado>, AssociadoValidator>();
            services.AddSingleton<IValidator<ConsultaFluxo>, ConsultaFluxoValidator>();
            services.AddSingleton<IValidator<Fluxo>, FluxoValidator>();
            services.AddSingleton<IValidator<Usuario>, UsuarioValidator>();

            #endregion

            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = Configuration.GetConnectionString("RedisConnectionString");
            });

            services.AddSingleton<IMemoryCache, MemoryCache>();
            Gisa.SqlRepository.Map.DapperMap.RegisterDapperMapper();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("pt-BR");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            // Ativando middlewares para uso do Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "GISA");
            });
        }
    }
}
