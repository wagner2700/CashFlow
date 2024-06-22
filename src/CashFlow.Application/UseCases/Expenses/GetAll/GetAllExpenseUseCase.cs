﻿using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Application.UseCases.Expenses.GetAll
{
    public class GetAllExpenseUseCase : IGetAllExpenseUseCase
    {
        private readonly IExpensesRepository _expensesRepository;
        private readonly IMapper _mapper;
        public GetAllExpenseUseCase(IExpensesRepository repository ,IMapper mapper)
        {
            repository = _expensesRepository;
            _mapper = mapper;
        }

        public async Task<ResponseExpensesJson> Execute()
        {
            var result = await _expensesRepository.GetAll();

            return new ResponseExpensesJson
            {
                Expenses = _mapper.Map<List<ResponseShortExpenseJson>>(result),    
            };
        }

        
    }
}
