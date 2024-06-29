using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Repositories.User;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Infrastructure.DataAccess;
using CashFlow.Infrastructure.DataAccess.Repositories;
using CashFlow.Infrastructure.DataAccess.Repositories.Expenses;
using CashFlow.Infrastructure.Security.Tokens;
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
            services.AddScoped<IPasswordEncripter, Security.Criptography.BCrypt>();
        }

        // Adicionar a injeção de dependencia dos repositorios
        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IExpensesReadOnlyRepository, ExpensesRepository>();
            services.AddScoped<IExpensesWriteOnlyRepository, ExpensesRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IExpensesUpdateOnlyRepository, ExpensesRepository>();
            services.AddScoped<IUserReadOnlyRepository, UserRepository>();
            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
            services.AddScoped<IAcessTokenGenerator, JwtTokenGenerator>();
        }

        private static void AddToken(IServiceCollection services, IConfiguration configuration)
        {
            // Entre <> o tipo de dado que quer buscar
            var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpiresMinutes");
            var signinKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

            services.AddScoped<IAcessTokenGenerator>(config => new JwtTokenGenerator(expirationTimeMinutes , signinKey!));
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
