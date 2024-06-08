using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Infrastructure.DataAccess;
using CashFlow.Infrastructure.Exceptions;

namespace CashFlow.Application.UseCases.Expenses.Register
{
    public class RegisterExpenseUseCase : IRegisterExpenseUseCase
    {
        private readonly IExpensesRepository _repository;

        public RegisterExpenseUseCase(IExpensesRepository repository)
        {
            _repository = repository;
        }
        public ResponseRegisterExpenseJson Execute(RequestRegisterExpenseJson request)
        {
            Validate(request);

            var entity = new Expense
            {
                Amount = request.Amount,
                Date = request.Date,
                Description = request.Description,
                Tile = request.Title,
                PaymentType = (Domain.Entities.Enums.PaymentsType)request.paymentType,
            };

            _repository.Add(entity);
            return new ResponseRegisterExpenseJson();
        }

        private void Validate(RequestRegisterExpenseJson request)
        {

            var validator = new RegisterExpenseValidator();

            var result = validator.Validate(request);

            if( result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
