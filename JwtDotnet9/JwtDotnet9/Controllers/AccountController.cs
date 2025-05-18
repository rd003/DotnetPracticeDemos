using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using JwtDotnet9.Models;
using JwtDotnet9.Services;
using Microsoft.AspNetCore.Mvc;

namespace JwtDotnet9.Controllers;

[ApiController]
[Route("/accounts")]
public class AccountController : ControllerBase
{

    private readonly ITokenService _tokenService;
    private readonly IUserService _userService;

    public AccountController(ITokenService tokenService, IUserService userService)
    {
        _tokenService = tokenService;
        _userService = userService;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginModel model)
    {
        try
        {
            var user = _userService.GetUser(model.Username, model.Password);

            if (user == null)
            {
                return Unauthorized("Invalid username or password");
            }

            // creating the necessary claims

            List<Claim> claims = [
                new (ClaimTypes.Name, user.Username),  // claim to store name
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            // unique identifier for jwt
            ];

            // adding roles to claims

            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // generating access token
            var token = _tokenService.GenerateAccessToken(claims);

            return Ok(token);
        }
        catch (Exception ex)
        {
            return Unauthorized();
        }
    }
}