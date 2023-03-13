using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Projecto_Final.Contexts;
using Projecto_Final.Helpers;
using Projecto_Final.Services;
using System.Text;

namespace Projecto_Final
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ProjetoFinal")));
            builder.Services.AddScoped<ISecurity, Security>();
            builder.Services.AddScoped<IJwtTokenAuth, JwtTokenAuth>();
            builder.Services.AddScoped<IAppLogging, AppLogging>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IDiscountService, DiscountService>();
            builder.Services.AddScoped<IConsoleService, ConsoleService>();
            builder.Services.AddScoped<IGenreService, GenreService>();

            builder.Services.AddCors(options => options.AddPolicy("CorsPolicy", 
                app => {
                    app.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
                })
            );

            var TokenSecurity = builder.Configuration.GetSection("Token");
            builder.Services.Configure<JwtTokenConfig>(TokenSecurity);

            var jwtAuth = TokenSecurity.Get<JwtTokenConfig>();
            var key = Encoding.ASCII.GetBytes(jwtAuth.Secret);

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                            .AddJwtBearer(x =>
                            {
                                x.Events = new JwtBearerEvents();
                                x.RequireHttpsMetadata = false;
                                x.SaveToken = true;
                                x.TokenValidationParameters = new TokenValidationParameters()
                                {
                                    ValidateIssuerSigningKey = true,
                                    IssuerSigningKey = new SymmetricSecurityKey(key),
                                    ValidateIssuer = false,
                                    ValidateAudience = false
                                };
                            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();

            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}