using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.Repositories.Expenses
{
    internal class ExpensesRepository : IExpensesReadOnlyRepository , IExpensesWriteOnlyRepository
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

        public async Task<Expense?> GetById(long id)
        {
            return await _dbContext.Expenses.AsNoTracking().FirstOrDefaultAsync(expense => expense.Id == id);
        }


    }
}
