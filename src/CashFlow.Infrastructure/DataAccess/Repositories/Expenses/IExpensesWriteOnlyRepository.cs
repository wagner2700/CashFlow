using CashFlow.Domain.Entities;

namespace CashFlow.Infrastructure.DataAccess.Repositories.Expenses
{
    public interface IExpensesWriteOnlyRepository
    {
        // Função para adicionar despesa
        Task Add(Expense expenses);
        /// <summary>
        /// Metodo deve retornar true se remoção for sucesso
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Delete(long id);
    }
}
