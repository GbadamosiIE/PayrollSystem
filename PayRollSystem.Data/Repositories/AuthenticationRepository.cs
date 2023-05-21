

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Net;
using Microsoft.EntityFrameworkCore;
using PayRollSystem.Domain.Entities;
using PayRollSystem.Domain.Utilities;
using PayRollSystem.Domain.DTO;
using PayRollSystem.Domain.IRepositories;
using PayRollSystem.Domain.IPayRollSystemServices;

using PayRollSystem.Domain.EmailRepositories;
using PayRollSystem.Application.Profiles;

namespace PayRollSystem.Data.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly UserManager<Employee> _userManager;
        private readonly ITokenService _token;
        private readonly ITokenDetails _tokenDetails;
        private readonly IHttpContextAccessor _httpContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        private readonly SignInManager<Employee> _signInManager;

        public AuthenticationRepository(UserManager<Employee> userManager, ITokenService token,
            ITokenDetails tokenDetails, IHttpContextAccessor httpContext,
            RoleManager<IdentityRole> roleManager, IEmailService emailService, SignInManager<Employee> signInManager)
        {
            _userManager = userManager;
            _token = token;
            _tokenDetails = tokenDetails;
            _httpContext = httpContext;
            _roleManager = roleManager;
            _emailService = emailService;
            _signInManager = signInManager;
        }

        public string GetId() => _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        public async Task<Response<string>> Login(LoginDTO model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            var response = new Response<string>();
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                
                var UserModel = new UserModel {
                    Id = user.Id,
                    UserName = model.Username,
                    Role = userRoles.FirstOrDefault() ?? ""
                };

                await _signInManager.PasswordSignInAsync(model.Username, model.Password, isPersistent: false, lockoutOnFailure: false);
                var refreshToken = _token.SetRefreshToken();
                //var refreshToken = SetRefreshToken();
                await SaveRefreshToken(user, refreshToken);
                response.Succeeded = true;
                response.Data = _token.CreateToken(UserModel);
                response.StatusCode = (int)HttpStatusCode.Accepted;
                response.Message = "Logged in successfully";
                
            }
            else
            {
                response.Succeeded = false;
                response.Message = "Wrong Credential";
                
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            return response;
        }
       

        private async Task SaveRefreshToken(Employee user, RefreshToken refreshToken)
        {
            user.RefreshToken = refreshToken.Refreshtoken;
            user.RefreshTokenExpiryTime = refreshToken.RefreshTokenExpiryTime;
            await _userManager.UpdateAsync(user);
        }

        public async Task<Response<string>> RefreshToken()
        {
            var currentToken = _httpContext.HttpContext.Request.Cookies["refresh-token"];
            var user = await _userManager.FindByIdAsync(_tokenDetails.GetId());
             
            var response = new Response<string>();
            response.Succeeded = false;
            response.StatusCode = (int)HttpStatusCode.BadRequest;
            if (user == null || user.RefreshToken != currentToken)
            {
                response.Data = "Invalid refresh token";
            }else if(user.RefreshTokenExpiryTime < DateTime.Now)
            {
                response.Message = "Token Expired";
            }else
            {
                var UserModel = new UserModel
                {
                    Id = _tokenDetails.GetId(),
                    UserName = _tokenDetails.GetUserName(),
                    Role = _tokenDetails.GetRoles()
                };

                response.Succeeded = true;
                response.Data = _token.CreateToken(UserModel);
                response.Message = "Successful refreshed token";
                response.StatusCode = (int)HttpStatusCode.Accepted;

                var refreshToken = _token.SetRefreshToken();
                await SaveRefreshToken(user, refreshToken);
            }
            return response;
        }
        public async Task<bool> Register(RegisterDTO user, string Role)
        {
            var mapInitializer = new MapInitializer();
            var newUser = mapInitializer.regMapper.Map<RegisterDTO, Employee>(user);
            
            //var result = await _userManager.CreateAsync(newUser, user.Password);
            var result = await _userManager.CreateAsync(newUser, user.Password);
            var roles = await _roleManager.Roles.ToListAsync();
            if (!await _roleManager.RoleExistsAsync(Role)) await _roleManager.CreateAsync(new IdentityRole { Name = Role });
            if (result.Succeeded) await _userManager.AddToRoleAsync(newUser, Role);

            
            return result.Succeeded;
        }

        public async Task<object> ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            var user = await _userManager.FindByIdAsync(GetId());
            if (user == null) return "Please login to change password";
            var result = await _userManager.ChangePasswordAsync(user, changePasswordDTO.CurrentPassword, changePasswordDTO.NewPassword);
            if (!result.Succeeded) return "Unable to change password: password should contain a Capital, number, character and minimum length of 8";
            return "Password changed succesffully";
        }

        public async Task<object> ForgottenPassword(ResetPasswordDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return ("The Email Provided is not associated with a user account");
            }

            var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var emailMsg = new EmailMessage(new string[] { user.Email }, "Reset your password", $"Please Follow the Link to reset your Password: https://localhost:7255/api/Auth/Reset-Update-Password?token={resetPasswordToken}");
            await _emailService.SendEmailAsync(emailMsg);
            return "A password reset Link has been sent to your email address";
        }

        public async Task<object> ResetPassword(UpdatePasswordDTO model)
        {

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return "The Email Provided is not associated with a user account.";
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);

            if (!result.Succeeded)
            {
                return "The Provided Reset Token is Invalid or Has expired";
            }
            return "Password Reset Successfully";
        }

        public async Task Signout()
        {
            var headers = _httpContext.HttpContext.Request.Headers;
            headers.Remove("Authorisation");
        }
    }
}
