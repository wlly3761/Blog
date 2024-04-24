using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Blog.BaseConfigSerivce.Jwt;

public static class JwtInit
{
    public static void JwtRegesiterService(this IServiceCollection services, IConfiguration systemConfig)
    {
        if (systemConfig == null) throw new NullReferenceException("ServiceConfig is Null");
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(p =>
            {
                p.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromSeconds(30),
                    ValidateIssuerSigningKey = true,
                    ValidAudience =  systemConfig.GetValue<string>("JwtSetting:Audience"),
                    ValidIssuer = systemConfig.GetValue<string>("JwtSetting:Issuer"),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(systemConfig.GetValue<string>("JwtSetting:SecretKey")!))
                };
            });
    }
}