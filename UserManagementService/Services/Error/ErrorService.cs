using Microsoft.AspNetCore.Mvc;

namespace UserManagementService.Services.Error
{
    public class ErrorService: IErrorService
    {
        public ErrorContainer ErrorContainer { get; set; } = new ErrorContainer();


        public void AddError(Error error)
        {
            this.ErrorContainer.Errors.Add(error);
        }

        public OkObjectResult Exception(Exception exception)
        {
            ErrorContainer.Errors.Add(new Error
            {
                ErrorString = exception.Message,
                Type = ErrorType.Error
            });
            return new OkObjectResult(ErrorContainer);
        }
    }
}
