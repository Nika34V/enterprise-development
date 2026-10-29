using FitnessClub.Application.Mapping;
using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Interfaces;
using FitnessClub.Domain;
using FitnessClub.Domain.Interfaces;

namespace FitnessClub.Application.Services;

/// <summary>
/// Сервис аналитических запросов
/// </summary>
public class AnalyticsService(
    IClientRepository clientRepository,
    ITrainerRepository trainerRepository,
    IEnrollmentRepository enrollmentRepository) : IAnalyticsService
{
    public async Task<IReadOnlyList<TrainerDto>> GetExperiencedTrainersAsync(int minimumYears)
    {
        var trainers = await trainerRepository.GetAllAsync();

        return trainers
            .Where(t => t.ExperienceYears >= minimumYears)
            .OrderBy(t => t.FullName)
            .Select(TrainerMapper.ToDto)
            .ToList();
    }

    public async Task<HallAvailabilityDto> IsHallAvailableAsync(Hall hall, DateTime moment)
    {
        var enrollments = await enrollmentRepository.GetAllAsync();
        var isAvailable = !enrollments.Any(e => e.Hall == hall && e.IsOngoingAt(moment));

        return new HallAvailabilityDto { Hall = hall, IsAvailable = isAvailable };
    }

    public async Task<IReadOnlyList<ClientDto>> GetClientsWithExpiredMembershipAsync(DateOnly asOfDate)
    {
        var clients = await clientRepository.GetAllAsync();

        return clients
            .Where(c => c.MembershipEnd < asOfDate)
            .OrderBy(c => c.FullName)
            .Select(ClientMapper.ToDto)
            .ToList();
    }

    public async Task<IReadOnlyList<EnrollmentDto>> GetSessionsInHallForMonthAsync(Hall hall, int year, int month)
    {
        var enrollments = await enrollmentRepository.GetAllAsync();

        return enrollments
            .Where(e => e.Hall == hall && e.StartsAt.Year == year && e.StartsAt.Month == month)
            .OrderBy(e => e.StartsAt)
            .Select(EnrollmentMapper.ToDto)
            .ToList();
    }

    public async Task<IReadOnlyList<TrainerDto>> GetTopFiveTrainersAsync()
    {
        var enrollments = await enrollmentRepository.GetAllAsync();
        var trainers = await trainerRepository.GetAllAsync();

        var topIds = enrollments
            .GroupBy(e => e.Trainer.Id)
            .OrderByDescending(g => g.Count())
            .ThenBy(g => g.Key)
            .Take(5)
            .Select(g => g.Key)
            .ToList();

        return topIds
            .Select(id => trainers.Single(t => t.Id == id))
            .Select(TrainerMapper.ToDto)
            .ToList();
    }
}