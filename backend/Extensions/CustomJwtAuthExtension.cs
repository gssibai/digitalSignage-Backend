using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace backend.Extensions;

public static class CustomJwtAuthExtension
{
    public static void AddCustomJwtAuth(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddAuthentication(
            o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
            });
    }

    public static void AddSwaggerGenAuth(this IServiceCollection services)
    {
        services.AddSwaggerGen(o =>
        {
            o.SwaggerDoc("V1", new OpenApiInfo()
            {
                Version = "v1",
                Title = "DigitalSignage",
                Description = "DigitalSignage API",
                Contact = new OpenApiContact()
                {
                    Name = "Ghassan Al-Sibai",
                    Email = "ghassan@sibai.com",
                    Url = new Uri("https://www.linkedin.com/in/gssibai/")
                }
            });
            o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter JWT Bearer Token",
            });
            o.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme()
                    {
                        Reference = new OpenApiReference()
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }


            });
        });
        
    }
}