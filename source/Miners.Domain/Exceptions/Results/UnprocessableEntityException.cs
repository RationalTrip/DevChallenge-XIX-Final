namespace Miners.Domain.Exceptions.Results
{
    public class UnprocessableEntityException : Exception
    {
        public UnprocessableEntityException(string error, string details) : base(error)
        {
            Error = error;
            Details = details;
        }

        public string Error { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
    }
}
