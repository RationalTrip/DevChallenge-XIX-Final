namespace Miners.Domain.Exceptions.Results
{
    public class CellsCannotBeDetectedException : UnprocessableEntityException
    {
        public CellsCannotBeDetectedException() : base(
            "Proper cells can not be detected",
            "Not all borders are white, or cell may not squire")
        {
        }
    }
}
