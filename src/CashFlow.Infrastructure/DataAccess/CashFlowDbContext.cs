using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess
{
    internal class CashFlowDbContext : DbContext
    {
        public DbSet<Expense> Expenses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configurações do banco de dados
            var connectionString = "server=localhost;Database=cashflowdb;Uid=root;Pwd=58326111;";

            var serverVersion = new MySqlServerVersion(new Version(8,0,35));
            optionsBuilder.UseMySql(connectionString , serverVersion);
        }
    }
}
