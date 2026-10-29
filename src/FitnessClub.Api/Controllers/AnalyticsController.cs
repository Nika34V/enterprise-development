using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Interfaces;
using FitnessClub.Domain;
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
    [HttpGet("experienced-trainers")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IReadOnlyList<TrainerDto>>> GetExperiencedTrainers([FromQuery] int minimumYears = 5)
    {
        try
        {
            return Ok(await service.GetExperiencedTrainersAsync(minimumYears));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка аналитики: опытные тренеры");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Проверяет, доступен ли зал для записи в указанный момент времени
    /// </summary>
    [HttpGet("hall-availability")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<HallAvailabilityDto>> GetHallAvailability([FromQuery] Hall hall, [FromQuery] DateTime? moment = null)
    {
        try
        {
            var checkMoment = moment ?? DateTime.Now;
            return Ok(await service.IsHallAvailableAsync(hall, checkMoment));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка аналитики: доступность зала");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Возвращает клиентов с просроченным абонементом, упорядоченных по ФИО
    /// </summary>
    [HttpGet("expired-memberships")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IReadOnlyList<ClientDto>>> GetClientsWithExpiredMembership([FromQuery] DateOnly? asOfDate = null)
    {
        try
        {
            var checkDate = asOfDate ?? DateOnly.FromDateTime(DateTime.Now);
            return Ok(await service.GetClientsWithExpiredMembershipAsync(checkDate));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка аналитики: просроченные абонементы");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Возвращает занятия выбранного зала за указанный год и месяц
    /// </summary>
    [HttpGet("sessions-in-hall")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IReadOnlyList<EnrollmentDto>>> GetSessionsInHall([FromQuery] Hall hall, [FromQuery] int year, [FromQuery] int month)
    {
        try
        {
            return Ok(await service.GetSessionsInHallForMonthAsync(hall, year, month));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка аналитики: занятия зала за месяц");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Возвращает пять самых популярных тренеров по числу записей на занятия
    /// </summary>
    [HttpGet("top-trainers")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IReadOnlyList<TrainerDto>>> GetTopTrainers()
    {
        try
        {
            return Ok(await service.GetTopFiveTrainersAsync());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка аналитики: топ тренеров");
            return StatusCode(500);
        }
    }
}