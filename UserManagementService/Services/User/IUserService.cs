namespace UserManagementService.Services.User
{
    public interface IUserService
    {
        Task<string> CreateUserAsync(string Email, string Password);

        Task<string> LoginUserAsync(string Email, string Password);
    }
}
