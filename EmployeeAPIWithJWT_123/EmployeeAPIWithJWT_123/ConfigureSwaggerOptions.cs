using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EmployeeAPIWithJWT_123
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        public void Configure(SwaggerGenOptions options)
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "jwt authrization header using the bearer scheme\r\r" +
                "Enter 'Bearer' [space] and then your token in the text input below\r\r\n" +
                "Example:Bearer 12345qwer \r\n" +
                "Name:Authorization \r\n" +
                "In:header",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                      new OpenApiSecurityScheme{
                Reference=new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                },
                Scheme="oauth2",
                Name="Bearer",
                In=ParameterLocation.Header
                },
                      new List<string>()

                }

            });
        }
    }
}
