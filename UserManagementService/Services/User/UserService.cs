using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using UserManagementService.Services.Error;

namespace UserManagementService.Services.User
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IErrorService _errorService;
        public UserService(UserManager<IdentityUser> userManager,
            IErrorService errorService,
            SignInManager<IdentityUser> signInManager)
        {
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
                Email = Password
            };

            var result = await userManager.CreateAsync(user, Password);
            if (result.Succeeded)
            {
                // await signInManager.SignInAsync(user, isPersistent: false);
                dynamic dynamic = new { UserId = user.Id, user.UserName };
                resultStr = JsonConvert.SerializeObject(dynamic);
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
    }
}
