using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses
{
    public interface IExpensesRepository
    {
        // Função para adicionar despesa
        Task Add(Expense expenses);
        Task<List<Expense>> GetAll();

        Task<Expense?> GetById(long id);
    }
}
