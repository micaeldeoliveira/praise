
namespace Praise.Commands
{
    public class CommandResult
    {
        public CommandResult(bool success, string message, object data, object errors)
        {
            Success = success;
            Message = message;
            Data = data;
            Errors = errors;
        }
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public object Data { get; private set; }
        public object Errors { get; private set; }
    }
}
