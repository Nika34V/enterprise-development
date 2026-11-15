using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Interfaces;
using FitnessClub.Shared.Enums;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Controllers;

/// <summary>
/// Контроллер для получения аналитических данных фитнес-клуба
/// </summary>
[ApiController]
[Route("api/analytics")]
public class AnalyticsController(IAnalyticsService service, ILogger<AnalyticsController> logger) : ControllerBase
{
    /// <summary>
    /// Возвращает тренеров со стажем работы не менее указанного количества лет
    /// </summary>
    /// <param name="minimumYears">Минимальный стаж работы в годах, не меньше 0 (по умолчанию 5)</param>
    /// <response code="200">Список тренеров, упорядоченный по ФИО</response>
    /// <response code="400">Стаж указан некорректно</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [HttpGet("experienced-trainers")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IReadOnlyList<TrainerDto>>> GetExperiencedTrainers([FromQuery] int minimumYears = 5)
    {
        logger.LogInformation("Запрос тренеров со стажем не менее {Years} лет", minimumYears);
        var result = await service.GetExperiencedTrainersAsync(minimumYears);
        return Ok(result);
    }

    /// <summary>
    /// Проверяет, доступен ли зал для записи в указанный момент времени
    /// </summary>
    /// <remarks>
    /// Зал считается занятым, если в этот момент в нём идёт занятие
    /// </remarks>
    /// <param name="hall">Проверяемый зал</param>
    /// <param name="moment">Момент времени (по умолчанию — текущий)</param>
    /// <response code="200">Информация о доступности зала</response>
    /// <response code="400">Указан несуществующий зал</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [HttpGet("hall-availability")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<HallAvailabilityDto>> GetHallAvailability([FromQuery] Hall hall, [FromQuery] DateTime? moment = null)
    {
        logger.LogInformation("Запрос доступности зала {Hall}", hall);
        var result = await service.IsHallAvailableAsync(hall, moment ?? DateTime.Now);
        return Ok(result);
    }

    /// <summary>
    /// Возвращает клиентов с просроченным абонементом, упорядоченных по ФИО
    /// </summary>
    /// <param name="asOfDate">Дата, на которую проверяется просрочка (по умолчанию — сегодня)</param>
    /// <response code="200">Список клиентов с просроченным абонементом</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [HttpGet("expired-memberships")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IReadOnlyList<ClientDto>>> GetClientsWithExpiredMembership([FromQuery] DateOnly? asOfDate = null)
    {
        logger.LogInformation("Запрос клиентов с просроченным абонементом");
        var result = await service.GetClientsWithExpiredMembershipAsync(asOfDate ?? DateOnly.FromDateTime(DateTime.Now));
        return Ok(result);
    }

    /// <summary>
    /// Возвращает занятия выбранного зала за указанный год и месяц
    /// </summary>
    /// <param name="hall">Зал</param>
    /// <param name="year">Год (от 1 до 9999)</param>
    /// <param name="month">Месяц (от 1 до 12)</param>
    /// <response code="200">Список занятий, упорядоченный по времени начала</response>
    /// <response code="400">Зал, год или месяц указаны некорректно</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [HttpGet("sessions-in-hall")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IReadOnlyList<EnrollmentDto>>> GetSessionsInHall(
        [FromQuery] Hall hall,
        [FromQuery] int year,
        [FromQuery] int month)
    {
        logger.LogInformation("Запрос занятий зала {Hall} за {Month}.{Year}", hall, month, year);
        var result = await service.GetSessionsInHallForMonthAsync(hall, year, month);
        return Ok(result);
    }

    /// <summary>
    /// Возвращает пять самых популярных тренеров по числу записей на занятия
    /// </summary>
    /// <remarks>
    /// Для каждого тренера возвращается и количество его записей
    /// </remarks>
    /// <response code="200">Тренеры с количеством записей, по убыванию популярности</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [HttpGet("top-trainers")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IReadOnlyList<TopTrainerDto>>> GetTopTrainers()
    {
        logger.LogInformation("Запрос топ-5 тренеров");
        var result = await service.GetTopFiveTrainersAsync();
        return Ok(result);
    }
}
