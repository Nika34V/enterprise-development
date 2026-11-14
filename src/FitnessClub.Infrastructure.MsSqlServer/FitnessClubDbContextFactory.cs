using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FitnessClub.Infrastructure.MsSqlServer;

/// <summary>
/// Создаёт контекст для инструментов миграций (dotnet ef)
/// </summary>
public class FitnessClubDbContextFactory : IDesignTimeDbContextFactory<FitnessClubDbContext>
{
    /// <summary>
    /// Создаёт контекст с заглушечной строкой подключения: при создании миграции к базе никто не подключается
    /// </summary>
    /// <param name="args">Аргументы командной строки</param>
    /// <returns>Контекст базы данных</returns>
    public FitnessClubDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<FitnessClubDbContext>()
            .UseSqlServer("Server=localhost;Database=FitnessClub;Trusted_Connection=True;TrustServerCertificate=True")
            .Options;

        return new FitnessClubDbContext(options);
    }
}
