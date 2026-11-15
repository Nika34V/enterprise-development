using FitnessClub.Application.Mapping;
using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Exceptions;
using FitnessClub.Contracts.Interfaces;
using FitnessClub.Domain.Interfaces;
using FitnessClub.Shared.Enums;
using Microsoft.Extensions.Logging;

namespace FitnessClub.Application.Services;

/// <summary>
/// Сервис аналитических запросов: проверка параметров, логирование и выборки данных
/// </summary>
public class AnalyticsService(
    IClientRepository clientRepository,
    ITrainerRepository trainerRepository,
    IEnrollmentRepository enrollmentRepository,
    ILogger<AnalyticsService> logger) : IAnalyticsService
{
    /// <summary>
    /// Возвращает тренеров со стажем не менее указанного, упорядоченных по ФИО
    /// </summary>
    /// <param name="minimumYears">Минимальный стаж работы в годах (включительно), не меньше 0</param>
    /// <returns>Список тренеров со стажем не менее указанного, упорядоченный по ФИО</returns>
    /// <exception cref="InvalidRequestException">Стаж меньше нуля</exception>
    public async Task<IReadOnlyList<TrainerDto>> GetExperiencedTrainersAsync(int minimumYears)
    {
        if (minimumYears < 0)
            throw Invalid("Минимальный стаж не может быть отрицательным");

        logger.LogInformation("Запрос тренеров со стажем не менее {Years} лет", minimumYears);
        var trainers = await trainerRepository.GetAllAsync();

        return [.. trainers
            .Where(t => t.ExperienceYears >= minimumYears)
            .OrderBy(t => t.FullName)
            .Select(TrainerMapper.ToDto)];
    }

    /// <summary>
    /// Проверяет, нет ли в зале занятия в указанный момент времени
    /// </summary>
    /// <param name="hall">Проверяемый зал</param>
    /// <param name="moment">Момент времени, на который проверяется доступность</param>
    /// <returns>Информация о доступности зала: свободен он или занят в этот момент</returns>
    /// <exception cref="InvalidRequestException">Указан несуществующий зал</exception>
    public async Task<HallAvailabilityDto> IsHallAvailableAsync(Hall hall, DateTime moment)
    {
        ValidateHall(hall);

        logger.LogInformation("Проверка доступности зала {Hall} на {Moment}", hall, moment);
        var enrollments = await enrollmentRepository.GetAllAsync();
        var isAvailable = !enrollments.Any(e => e.Hall == hall && e.IsOngoingAt(moment));

        return new HallAvailabilityDto { Hall = hall, IsAvailable = isAvailable };
    }

    /// <summary>
    /// Возвращает клиентов с просроченным абонементом, упорядоченных по ФИО
    /// </summary>
    /// <param name="asOfDate">Дата, на которую проверяется просрочка абонемента</param>
    /// <returns>Список клиентов с просроченным абонементом, упорядоченный по ФИО</returns>
    public async Task<IReadOnlyList<ClientDto>> GetClientsWithExpiredMembershipAsync(DateOnly asOfDate)
    {
        logger.LogInformation("Запрос клиентов с просроченным абонементом на {Date}", asOfDate);
        var clients = await clientRepository.GetAllAsync();

        return [.. clients
            .Where(c => c.MembershipEnd < asOfDate)
            .OrderBy(c => c.FullName)
            .Select(ClientMapper.ToDto)];
    }

    /// <summary>
    /// Возвращает занятия выбранного зала за указанный месяц
    /// </summary>
    /// <param name="hall">Зал</param>
    /// <param name="year">Год (от 1 до 9999)</param>
    /// <param name="month">Месяц (от 1 до 12)</param>
    /// <returns>Список занятий зала за месяц, упорядоченный по времени начала</returns>
    /// <exception cref="InvalidRequestException">Зал, год или месяц указаны некорректно</exception>
    public async Task<IReadOnlyList<EnrollmentDto>> GetSessionsInHallForMonthAsync(Hall hall, int year, int month)
    {
        ValidateHall(hall);

        if (month is < 1 or > 12)
            throw Invalid("Месяц должен быть числом от 1 до 12");

        if (year is < 1 or > 9999)
            throw Invalid("Год должен быть числом от 1 до 9999");

        logger.LogInformation("Запрос занятий зала {Hall} за {Month}.{Year}", hall, month, year);
        var enrollments = await enrollmentRepository.GetAllAsync();

        return [.. enrollments
            .Where(e => e.Hall == hall && e.StartsAt.Year == year && e.StartsAt.Month == month)
            .OrderBy(e => e.StartsAt)
            .Select(EnrollmentMapper.ToDto)];
    }

    /// <summary>
    /// Возвращает пять самых популярных тренеров вместе с количеством их записей
    /// </summary>
    /// <returns>Пять самых популярных тренеров с количеством их записей, по убыванию популярности</returns>
    public async Task<IReadOnlyList<TopTrainerDto>> GetTopFiveTrainersAsync()
    {
        logger.LogInformation("Запрос топ-5 тренеров по числу записей");
        var enrollments = await enrollmentRepository.GetAllAsync();

        return [.. enrollments
            .GroupBy(e => e.Trainer.Id)
            .Select(g => new { Trainer = g.First().Trainer, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .ThenBy(x => x.Trainer.Id)
            .Take(5)
            .Select(x => TrainerMapper.ToTopDto(x.Trainer, x.Count))];
    }

    private void ValidateHall(Hall hall)
    {
        if (!Enum.IsDefined(hall))
            throw Invalid("Указан несуществующий зал");
    }

    private InvalidRequestException Invalid(string message)
    {
        logger.LogWarning("Некорректные параметры аналитического запроса: {Message}", message);
        return new InvalidRequestException(message);
    }
}
