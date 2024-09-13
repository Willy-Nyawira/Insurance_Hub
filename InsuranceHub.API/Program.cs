using InsuranceHub.Application.Handlers;
using InsuranceHub.Application.RepositoryInterfaces;
using InsuranceHub.Application.ServiceInterfaces;
using InsuranceHub.Application.Services;
using InsuranceHub.Infrastructure.Repositories;
using InsuranceHub.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using InsuranceHub.Application.UseCases;

namespace InsuranceHub.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

          
            builder.Services.AddControllers();
            builder.Services.AddScoped<RegisterCustomerUseCase>();
            builder.Services.AddScoped<GetPolicyByIdUseCase>();
            builder.Services.AddScoped<RegisterPolicyUseCase>();

            
            builder.Services.AddScoped<IPolicyRepository, PolicyRepository>();
        


            var configuration = builder.Configuration;

         
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Configure JWT authentication
            var Key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
            builder.Services.AddAuthentication(x =>
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
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Key)
                };
            });

            // Register application services
            builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<RegisterUserCommandHandler>();
            builder.Services.AddScoped<LoginUserCommandHandler>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<GetCustomerByIdUseCase>();
            builder.Services.AddScoped<GetCustomerByUsernameUseCase>();

            // Add Swagger for API documentation
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();  
            app.UseAuthorization();   

            app.MapControllers();

            app.Run();
        }
    }
}
