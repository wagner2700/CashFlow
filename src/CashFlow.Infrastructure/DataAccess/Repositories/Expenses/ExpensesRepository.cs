﻿using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.Repositories.Expenses
{
    internal class ExpensesRepository : IExpensesReadOnlyRepository , IExpensesWriteOnlyRepository , IExpensesUpdateOnlyRepository
    {
        // Prop da injeção de dependencia
        private readonly CashFlowDbContext _dbContext;

        public ExpensesRepository(CashFlowDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Expense expenses)
        {
            await _dbContext.Expenses.AddAsync(expenses);

        }

        public async Task<bool> Delete(long id)
        {
            var result = await _dbContext.Expenses.FirstOrDefaultAsync(x => x.Id == id);

            if (result is null) { 
                return false;
            }

            _dbContext.Expenses.Remove(result);
            return true;
        }

        public async Task<List<Expense>> GetAll()
        {
            return await _dbContext.Expenses.AsNoTracking().ToListAsync();
        }

        async Task<Expense?> IExpensesReadOnlyRepository.GetById(long id)
        {
            return await _dbContext.Expenses.AsNoTracking().FirstOrDefaultAsync(expense => expense.Id == id);
        }

        async Task<Expense?> IExpensesUpdateOnlyRepository.GetById(long id)
        {
            return await _dbContext.Expenses.FirstOrDefaultAsync(expense => expense.Id == id);
        }

        public void Update(Expense expense)
        {
            _dbContext.Expenses.Update(expense);
        }
    }
}
