using AutoMapper;
using Cadastro.Data;
using Cadastro.Models;
using Cadastro.Repository;
using Cadastro.Repository.Interface;
using Cadastro.ViewModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Cadastro
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region conexao com o banco
            var connectionString = builder.Configuration.GetConnectionString("databaseUrl");

            builder.Services.AddDbContext<DataContext>(
                connection => connection.UseSqlServer(connectionString).EnableSensitiveDataLogging(true));
            #endregion

            #region injecao de dependencia 
            builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>(); 
            builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
            
            #endregion

            #region modelState
            builder.Services.AddControllers().ConfigureApiBehaviorOptions(opt =>
            {
                opt.SuppressModelStateInvalidFilter = true;
                opt.SuppressMapClientErrors = true;
            });
            #endregion

            #region mapper
            var mapperConfig = new AutoMapper.MapperConfiguration(config =>
            {
                config.AllowNullCollections = true;
                config.AllowNullDestinationValues = true;

                config.CreateMap<UsuarioModel, LoginRequestVM>();
                config.CreateMap<LoginRequestVM, UsuarioModel>();

                config.CreateMap<LoginResponseVM, UsuarioModel>();
                config.CreateMap<UsuarioModel, LoginResponseVM>();

                config.CreateMap<UsuarioModel, UsuarioResponseVM>();

                config.CreateMap<UsuarioModel, UsuarioRequestVM>();
                config.CreateMap<UsuarioRequestVM, UsuarioModel>();
                config.CreateMap<UsuarioRequestVM, UsuarioResponseVM>();
            });

            IMapper mapper = mapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);
            #endregion

            #region autenticacao

            bool CustomLifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken tokenToValidate, TokenValidationParameters @param)
            {
                if (expires != null)
                {
                    return expires > DateTime.UtcNow;
                }
                return false;
            }

            var key = Encoding.ASCII.GetBytes(Settings.SECRET_TOKEN);

            builder.Services.AddAuthentication(a => {
                a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
                ).AddJwtBearer(options => {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidIssuer = "cadastro",
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        //LifetimeValidator = CustomLifetimeValidator, // forma de validar se o token está expirado
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        RequireExpirationTime = true
                    };
                });
            #endregion

            #region versionamento
            builder.Services.AddApiVersioning(options =>
            {
                options.UseApiBehavior = false;
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(3, 0);
                options.ApiVersionReader =
                    ApiVersionReader.Combine(
                        new HeaderApiVersionReader("x-api-version"),
                        new QueryStringApiVersionReader(),
                        new UrlSegmentApiVersionReader());
            });

            builder.Services.AddVersionedApiExplorer(setup => {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });

            builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            #endregion

            var app = builder.Build();

            var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();

                // Ajustando versionamento no Swagger
                app.UseSwaggerUI(c =>
                {
                    foreach (var d in provider.ApiVersionDescriptions)
                    {
                        c.SwaggerEndpoint(
                            $"/swagger/{d.GroupName}/swagger.json",
                            d.GroupName.ToUpperInvariant());
                    }

                    c.DocExpansion(DocExpansion.List);
                });
            }

            app.UseApiVersioning();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
