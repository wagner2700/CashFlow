using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses
{
    public interface IExpensesRepository
    {
        // Função para adicionar despesa
        void Add(Expense expenses);
    }
}
