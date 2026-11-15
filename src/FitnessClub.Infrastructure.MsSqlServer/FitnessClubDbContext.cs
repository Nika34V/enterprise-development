using FitnessClub.Domain;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Infrastructure.MsSqlServer;

/// <summary>
/// Контекст базы данных фитнес-клуба
/// </summary>
/// <param name="options">Настройки подключения</param>
public class FitnessClubDbContext(DbContextOptions<FitnessClubDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Таблица специализаций
    /// </summary>
    public DbSet<Specialization> Specializations => Set<Specialization>();

    /// <summary>
    /// Таблица тренеров
    /// </summary>
    public DbSet<Trainer> Trainers => Set<Trainer>();

    /// <summary>
    /// Таблица клиентов
    /// </summary>
    public DbSet<Client> Clients => Set<Client>();

    /// <summary>
    /// Таблица записей на занятия
    /// </summary>
    public DbSet<Enrollment> Enrollments => Set<Enrollment>();

    /// <summary>
    /// Подключает настройки таблиц из папки Configurations
    /// </summary>
    /// <param name="modelBuilder">Построитель модели</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FitnessClubDbContext).Assembly);
    }
}
