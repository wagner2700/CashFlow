using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Infrastructure.DataAccess;
using CashFlow.Infrastructure.DataAccess.Repositories;
using CashFlow.Infrastructure.DataAccess.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CashFlow.Infrastructure
{
    // Classe intermediária para program ter acesso na injeção de dependência
    public static class DependencyInjectionExtension
    {

        // this para referenciar esta função como função de sistema
        public static void AddInfraestructure(this IServiceCollection services , IConfiguration configuration)
        {
            AddRepositories(services );
            AddDbContext(services , configuration);
        }

        // Adicionar a injeção de dependencia dos repositorios
        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IExpensesReadOnlyRepository, ExpensesRepository>();
            services.AddScoped<IExpensesWriteOnlyRepository, ExpensesRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IExpensesUpdateOnlyRepository, ExpensesRepository>();
        }

        // Adicionar injeção de dependencia do DbContet
        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            // Configurações do banco de dados
            var connectionString = configuration.GetConnectionString("connection");

            var serverVersion = new MySqlServerVersion(new Version(8, 0, 35));
           

            services.AddDbContext<CashFlowDbContext>(config => config.UseMySql(connectionString, serverVersion));
        }
    }
}
