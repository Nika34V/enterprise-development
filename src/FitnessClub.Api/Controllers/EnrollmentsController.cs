using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Controllers;

/// <summary>
/// Контроллер для управления записями на занятия
/// </summary>
[ApiController]
[Route("api/enrollments")]
public class EnrollmentsController(IEnrollmentService service, ILogger<EnrollmentsController> logger) : ControllerBase
{
    /// <summary>
    /// Возвращает список всех записей на занятия
    /// </summary>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IEnumerable<EnrollmentDto>>> GetAll()
    {
        try
        {
            return Ok(await service.GetAllAsync());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении списка записей на занятия");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Возвращает запись на занятие по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<EnrollmentDto>> GetById(int id)
    {
        try
        {
            var item = await service.GetByIdAsync(id);
            if (item == null)
                return NotFound();
            return Ok(item);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении записи с Id {Id}", id);
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Создаёт новую запись на занятие
    /// </summary>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<EnrollmentDto>> Create([FromBody] EnrollmentEditDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var created = await service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Некорректные данные при создании записи на занятие");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при создании записи на занятие");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Обновляет существующую запись на занятие
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> Update(int id, [FromBody] EnrollmentEditDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var updated = await service.UpdateAsync(id, dto);
            return updated ? NoContent() : NotFound();
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Некорректные данные при обновлении записи с Id {Id}", id);
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при обновлении записи с Id {Id}", id);
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Удаляет запись на занятие по идентификатору
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var deleted = await service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при удалении записи с Id {Id}", id);
            return StatusCode(500);
        }
    }
}