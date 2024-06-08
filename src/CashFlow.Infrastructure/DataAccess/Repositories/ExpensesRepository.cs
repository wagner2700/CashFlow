using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Infrastructure.DataAccess.Repositories
{
    internal class ExpensesRepository : IExpensesRepository
    {
        public void Add(Expense expenses)
        {
            // Instanciar bdContext
            var dbContext = new CashFlowDbContext();

            dbContext.Expenses.Add(expenses);
            dbContext.SaveChanges();
        }
    }
}
