using CashFlow.Infrastructure.Exceptions;

namespace CashFlow.Exception.Exceptions
{
    public class NotFoundException  : CashFlowException
    {
        public NotFoundException(string message) : base(message)
        {
            
        }
    }
}
