namespace ChatService.Services.Error
{

    public enum ErrorType
    {
        Error = 1, Warning = 2, Info = 3, Suggestion = 4
    }
    public class ErrorContainer
    {
        public List<Error> Errors { get; set; } = new List<Error>();
    }
    public class Error
    {
        public string? ErrorString { get; set; }
        public string? InnerMessage { get; set; }
        public string? Suggestion { get; set; }
        public string? StackTrace { get; set; }
        public ErrorType Type { get; set; }
    }
}
