using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Exception;
using CashFlow.Exception.Exceptions;
using CashFlow.Infrastructure.DataAccess.Repositories.Expenses;

namespace CashFlow.Application.UseCases.Expenses.GetById
{
    public class GetExpenseByIdUseCase : IGetExpenseByIdUseCase
    {
        private readonly IExpensesReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public GetExpenseByIdUseCase(IExpensesReadOnlyRepository repository , IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ResponseExpenseJson> Execute(long id)
        {
            var result = await _repository.GetById(id);

            if (result is null) {
                throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);
            }

            return _mapper.Map<ResponseExpenseJson>(result);
        }
    }
}
