using ChatBot.Common.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using UserManagementService.Services.Auth;
using UserManagementService.Services.Error;

namespace UserManagementService.Services.User
{
    public class UserService : IUserService
    {
        private readonly IAuthService _authService;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IErrorService _errorService;
        public UserService(UserManager<IdentityUser> userManager,
            IErrorService errorService,
            IAuthService authService,
            SignInManager<IdentityUser> signInManager)
        {
            this._authService = authService;
            this._errorService = errorService;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public async Task<string> CreateUserAsync(string Email, string Password)
        {
            string resultStr = "";
            var user = new IdentityUser
            {
                UserName = Email,
                Email = Email,

            };

            var result = await userManager.CreateAsync(user, Password);
             if (result.Succeeded)
            {
                AuthenticationModel authenticationModel = new AuthenticationModel
                {
                    Email = Email,
                    UserId = user.Id,
                };
                this._authService.PopulateJwtTokenAsync(authenticationModel);
                await signInManager.SignInAsync(user, isPersistent: false);
                resultStr = JsonConvert.SerializeObject(authenticationModel);
            }
            else
            {
                this._errorService.AddError(new Error.Error
                {
                    ErrorString = string.Join(",", result.Errors.Select(s => s.Description).ToList()),
                    Type = ErrorType.Error
                });

                resultStr = "";
            }

            return resultStr;
            //foreach (var error in result.Errors)
            //{
            //    ModelState.AddModelError(string.Empty, error.Description);
            //}
        }

        public async Task<string> LoginUserAsync(string Email, string Password)
        {
            string resultStr = "";
            var user = await userManager.FindByEmailAsync(Email);
            if (user!=null)
            {
                var valied = await signInManager.UserManager.CheckPasswordAsync(user, Password);
                if (valied)
                {
                    AuthenticationModel authenticationModel = new AuthenticationModel
                    {
                        Email = Email,
                        UserId = user.Id,
                    };
                    this._authService.PopulateJwtTokenAsync(authenticationModel);
                    resultStr = JsonConvert.SerializeObject(authenticationModel);
                }
            }
            return resultStr;
        }

    }
}
