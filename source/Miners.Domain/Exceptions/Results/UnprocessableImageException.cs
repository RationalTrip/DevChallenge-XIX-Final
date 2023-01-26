namespace Miners.Domain.Exceptions.Results
{
    public class UnprocessableImageException : UnprocessableEntityException
    {
        public UnprocessableImageException(string message) :
            base("Image can not be decoded.",
                message)
        {
        }
    }
}
