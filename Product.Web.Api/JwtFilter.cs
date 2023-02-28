using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Product.Web.Api;

public class JwtFilter: IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {            var jwt = context.HttpContext.Request.Headers["Authorization"].ToString();
            
        var validateParameters = new TokenValidationParameters() 
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Frontend")),
            ValidateIssuer = true,
            ValidIssuer = "http://localhost:5004",
  //          ValidateAudience = true,
    //        ValidAudience = "http://localhost:5001",
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(5)
        };

        var validResult = ValidateToken(jwt, validateParameters);
            
            
        if (validResult == false)
        {
            context.HttpContext.Response.StatusCode = 401;
        }
    }
    
    private static bool ValidateToken(string token, TokenValidationParameters validationParameters)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    
    public class errmessage
    {
        public string statuscode { get; set; }
        public string err { get; set; }
    }
}