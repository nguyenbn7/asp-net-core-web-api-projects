using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Users.Models;
using Ecommerce.Users.DTOs;
using Ecommerce.Shared.Error;

namespace Ecommerce.Users;

[ApiController]
[Route("[controller]")]
public class AccountsController(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    IUserService tokenService) : ControllerBase
{
    private readonly UserManager<ApplicationUser> userManager = userManager;
    private readonly SignInManager<ApplicationUser> signInManager = signInManager;
    private readonly IUserService tokenService = tokenService;

    [HttpPost("signin")]
    public async Task<ActionResult<SignInSuccessful>> SignInAsync(CustomerSignIn signin)
    {
        var unauthorizedResponse = new ErrorResponse
        {
            Code = ErrorCodes.SIGN_IN_INFO_INCORRECT,
            Message = "User name or Password is incorrect"
        };

        var user = await userManager.FindByNameAsync(signin.Email);
        if (user == null)
            return Unauthorized(unauthorizedResponse);

        var result = await signInManager.CheckPasswordSignInAsync(user, signin.Password, false);

        if (!result.Succeeded)
            return Unauthorized(unauthorizedResponse);

        return new SignInSuccessful
        {
            DisplayName = user.DisplayName,
            AccessToken = tokenService.GenerateJWTToken(await CreateCustomerClaimsAsync(user)),
        };
    }

    [HttpPost("register")]
    public async Task<ActionResult<SignInSuccessful>> RegisterAsync(CustomerRegister register)
    {
        var user = await userManager.FindByEmailAsync(register.Email);
        if (user is not null)
            return BadRequest(new ErrorResponse
            {
                Code = ErrorCodes.REGISTER_EMAIL_EXIST,
                Message = "Email address already in use"
            });

        user = new ApplicationUser
        {
            FullName = register.FullName,
            DisplayName = register.DisplayName,
            Email = register.Email,
            UserName = register.Email
        };

        var result = await userManager.CreateAsync(user, register.Password);

        if (!result.Succeeded)
            return BadRequest(new ErrorResponse
            {
                Code = ErrorCodes.UNKNOWN_ERROR,
                Message = "Can not register. Please contact to our support."
            });

        return new SignInSuccessful
        {
            DisplayName = user.DisplayName,
            AccessToken = tokenService.GenerateJWTToken(await CreateCustomerClaimsAsync(user)),
        };
    }

    private async Task<List<Claim>> CreateCustomerClaimsAsync(ApplicationUser user)
    {
        var roles = await userManager.GetRolesAsync(user);
        return [
            new (JwtRegisteredClaimNames.Name, user.UserName!),
            new (ClaimTypes.Role, string.Join(',', roles)),
        ];
    }
}
