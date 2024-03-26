using Microsoft.AspNetCore.Mvc;

namespace UserManagementService.Services.Error
{
    public interface IErrorService
    {
        ErrorContainer ErrorContainer { get; set; }
        void AddError(Error error);
        OkObjectResult Exception(Exception exception);
    }
}
