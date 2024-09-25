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
using Microsoft.OpenApi.Models;
using InsuranceHub.Domain.Entities;

namespace InsuranceHub.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

          
            builder.Services.AddControllers();
            builder.Services.AddScoped<GetPolicyByIdUseCase>();
            builder.Services.AddScoped<RegisterPolicyUseCase>();
            builder.Services.AddScoped<CustomerLoginUseCase>();
            builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<ITokenService, TokenService>();




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
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<DeletePolicyUseCase>();
            builder.Services.AddScoped<UpdatePolicyUseCase>();
            builder.Services.AddScoped<GetPoliciesByCustomerUseCase>();
          




            // Add Swagger for API documentation
            builder.Services.AddEndpointsApiExplorer();
            // Add Swagger generation with Bearer Token Authorization
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "InsuranceHub.API", Version = "v1" });

                // Define the BearerAuth scheme
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\n\nExample: 'Bearer 12345abcdef'",
                });

                // Make Swagger use the BearerAuth scheme
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
              new string[] {}
        }
    });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "InsuranceHub.API v1");
                });
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();  
            app.UseAuthorization();   

            app.MapControllers();

            app.Run();
        }
    }
}
