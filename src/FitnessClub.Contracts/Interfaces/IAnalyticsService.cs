using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Exceptions;
using FitnessClub.Shared.Enums;

namespace FitnessClub.Contracts.Interfaces;

/// <summary>
/// Сервис аналитических запросов
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Возвращает тренеров со стажем работы не менее указанного, упорядоченных по ФИО
    /// </summary>
    /// <param name="minimumYears">Минимальный стаж работы в годах (включительно), не меньше 0</param>
    /// <returns>Список тренеров</returns>
    /// <exception cref="InvalidRequestException">Стаж указан некорректно</exception>
    public Task<IReadOnlyList<TrainerDto>> GetExperiencedTrainersAsync(int minimumYears);

    /// <summary>
    /// Проверяет, доступен ли зал для записи в указанный момент времени
    /// </summary>
    /// <param name="hall">Проверяемый зал</param>
    /// <param name="moment">Момент времени, на который проверяется доступность</param>
    /// <returns>Информация о доступности зала</returns>
    /// <exception cref="InvalidRequestException">Указан несуществующий зал</exception>
    public Task<HallAvailabilityDto> IsHallAvailableAsync(Hall hall, DateTime moment);

    /// <summary>
    /// Возвращает клиентов с просроченным абонементом, упорядоченных по ФИО
    /// </summary>
    /// <param name="asOfDate">Дата, на которую проверяется просрочка абонемента</param>
    /// <returns>Список клиентов</returns>
    public Task<IReadOnlyList<ClientDto>> GetClientsWithExpiredMembershipAsync(DateOnly asOfDate);

    /// <summary>
    /// Возвращает занятия выбранного зала за указанный месяц
    /// </summary>
    /// <param name="hall">Зал</param>
    /// <param name="year">Год (от 1 до 9999)</param>
    /// <param name="month">Месяц (от 1 до 12)</param>
    /// <returns>Список занятий, упорядоченный по времени начала</returns>
    /// <exception cref="InvalidRequestException">Зал, год или месяц указаны некорректно</exception>
    public Task<IReadOnlyList<EnrollmentDto>> GetSessionsInHallForMonthAsync(Hall hall, int year, int month);

    /// <summary>
    /// Возвращает пять самых популярных тренеров по числу записей на занятия
    /// </summary>
    /// <returns>Тренеры вместе с количеством записей, по убыванию популярности</returns>
    public Task<IReadOnlyList<TopTrainerDto>> GetTopFiveTrainersAsync();
}
