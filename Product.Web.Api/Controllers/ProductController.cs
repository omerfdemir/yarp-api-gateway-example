using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Product.Web.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductController: ControllerBase
{
    private const string BearerPrefix = "Bearer ";
    private static readonly string[] Products = new[]
    {
        "iPhone", "iPad", "MacBook", "MacBookPro",
    };
    public ProductController()
    {
        
    }

    [HttpGet]
    [Route("products")]
    public async Task<IActionResult> GetProducts()
    {
   
        string jwt = HttpContext.Request.Headers["Authorization"];
        
        var handler = new JwtSecurityTokenHandler();
        jwt = jwt.Substring(BearerPrefix.Length);
        var token = handler.ReadJwtToken(jwt);
        
        //TODO Add Token validate
        //TODO Move Token logic to BaseApiController
        
        return Ok(Products);
    }
}