namespace CashFlow.Infrastructure.Exceptions
{
    public class ErrorOnValidationException : CashFlowException
    {
        public List<string> Errors { get; set; }
        public ErrorOnValidationException(List<string> errorMessages) 
        { 
            Errors = errorMessages;
        }
    }
}
