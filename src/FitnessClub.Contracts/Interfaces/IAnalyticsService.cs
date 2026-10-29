using FitnessClub.Contracts.Dto;
using FitnessClub.Domain;

namespace FitnessClub.Contracts.Interfaces;

/// <summary>
/// Сервис аналитических запросов
/// </summary>
public interface IAnalyticsService
{
    Task<IReadOnlyList<TrainerDto>> GetExperiencedTrainersAsync(int minimumYears);
    Task<HallAvailabilityDto> IsHallAvailableAsync(Hall hall, DateTime moment);
    Task<IReadOnlyList<ClientDto>> GetClientsWithExpiredMembershipAsync(DateOnly asOfDate);
    Task<IReadOnlyList<EnrollmentDto>> GetSessionsInHallForMonthAsync(Hall hall, int year, int month);
    Task<IReadOnlyList<TrainerDto>> GetTopFiveTrainersAsync();
}