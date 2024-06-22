using CashFlow.Domain.Entities;

namespace CashFlow.Infrastructure.DataAccess.Repositories.Expenses
{
    public interface IExpensesReadOnlyRepository
    {

        Task<List<Expense>> GetAll();

        Task<Expense?> GetById(long id);
    }
}
