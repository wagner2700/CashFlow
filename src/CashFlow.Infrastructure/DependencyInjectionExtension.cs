using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Infrastructure.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infrastructure
{
    // Classe intermediária para program ter acesso na injeção de dependência
    public static class DependencyInjectionExtension
    {
        
        // this para referenciar esta função como função de sistema
        public static void AddInfraestructure(this IServiceCollection services)
        {
            services.AddScoped<IExpensesRepository, ExpensesRepository>();
        }
    }
}
