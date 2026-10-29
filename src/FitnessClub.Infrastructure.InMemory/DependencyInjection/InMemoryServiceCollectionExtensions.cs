using FitnessClub.Domain.Interfaces;
using FitnessClub.Infrastructure.InMemory.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FitnessClub.Infrastructure.InMemory.DependencyInjection;

/// <summary>
/// Регистрация репозиториев, работающих с хранилищем в памяти
/// </summary>
public static class InMemoryServiceCollectionExtensions
{
    public static IServiceCollection AddInMemoryPersistence(this IServiceCollection services)
    {
        services.AddSingleton<FitnessClubInMemoryStore>();

        services.AddScoped<IClientRepository, InMemoryClientRepository>();
        services.AddScoped<ITrainerRepository, InMemoryTrainerRepository>();
        services.AddScoped<ISpecializationRepository, InMemorySpecializationRepository>();
        services.AddScoped<IEnrollmentRepository, InMemoryEnrollmentRepository>();

        return services;
    }
}