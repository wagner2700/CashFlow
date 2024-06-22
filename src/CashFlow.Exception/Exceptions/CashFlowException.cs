namespace CashFlow.Infrastructure.Exceptions
{
    public abstract class CashFlowException : SystemException
    {
        protected CashFlowException(string message) : base(message)
        {
            
        }
    }
}
