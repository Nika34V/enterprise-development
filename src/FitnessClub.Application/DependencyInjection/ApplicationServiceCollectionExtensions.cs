using FitnessClub.Application.Services;
using FitnessClub.Contracts.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FitnessClub.Application.DependencyInjection;

/// <summary>
/// Регистрация сервисов 
/// </summary>
public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<ITrainerService, TrainerService>();
        services.AddScoped<ISpecializationService, SpecializationService>();
        services.AddScoped<IEnrollmentService, EnrollmentService>();
        services.AddScoped<IAnalyticsService, AnalyticsService>();

        return services;
    }
}