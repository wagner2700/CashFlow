﻿using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.Exceptions;

namespace CashFlow.Application.UseCases.Expenses.GetById
{
    public class GetExpenseByIdUseCase : IGetExpenseByIdUseCase
    {
        private readonly IExpensesRepository _repository;
        private readonly IMapper _mapper;

        public GetExpenseByIdUseCase(IExpensesRepository repository , IMapper mapper)
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
