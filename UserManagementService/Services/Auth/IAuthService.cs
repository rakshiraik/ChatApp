using ChatBot.Common.ViewModels;

namespace UserManagementService.Services.Auth
{
    public interface IAuthService
    {
        void PopulateJwtTokenAsync(AuthenticationModel user);
    }
}
