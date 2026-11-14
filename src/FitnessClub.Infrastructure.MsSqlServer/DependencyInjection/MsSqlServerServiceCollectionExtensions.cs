using FitnessClub.Domain.Interfaces;
using FitnessClub.Infrastructure.MsSqlServer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FitnessClub.Infrastructure.MsSqlServer.DependencyInjection;

/// <summary>
/// Регистрация работы с MS SQL Server
/// </summary>
public static class MsSqlServerServiceCollectionExtensions
{
    /// <summary>
    /// Регистрирует контекст базы данных и репозитории на базе Entity Framework
    /// </summary>
    /// <param name="services">Коллекция сервисов</param>
    /// <param name="connectionString">Строка подключения к базе</param>
    /// <returns>Та же коллекция сервисов</returns>
    public static IServiceCollection AddMsSqlServerPersistence(this IServiceCollection services, string? connectionString)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);

        services.AddDbContext<FitnessClubDbContext>(options => options.UseSqlServer(connectionString));

        services.AddScoped<IClientRepository, EfClientRepository>();
        services.AddScoped<ITrainerRepository, EfTrainerRepository>();
        services.AddScoped<ISpecializationRepository, EfSpecializationRepository>();
        services.AddScoped<IEnrollmentRepository, EfEnrollmentRepository>();

        return services;
    }

    /// <summary>
    /// Применяет миграции: создаёт таблицы и заполняет их начальными данными
    /// </summary>
    /// <param name="services">Контейнер сервисов приложения</param>
    /// <returns>Задача, завершающаяся после применения миграций</returns>
    public static async Task ApplyMigrationsAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<FitnessClubDbContext>();
        await context.Database.MigrateAsync();
    }
}
