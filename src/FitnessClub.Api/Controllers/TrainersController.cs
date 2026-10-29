using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Controllers;

/// <summary>
/// Контроллер для управления тренерами
/// </summary>
[ApiController]
[Route("api/trainers")]
public class TrainersController(ITrainerService service, ILogger<TrainersController> logger) : ControllerBase
{
    /// <summary>
    /// Возвращает список всех тренеров
    /// </summary>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IEnumerable<TrainerDto>>> GetAll()
    {
        try
        {
            return Ok(await service.GetAllAsync());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении списка тренеров");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Возвращает тренера по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TrainerDto>> GetById(int id)
    {
        try
        {
            var trainer = await service.GetByIdAsync(id);
            if (trainer == null)
                return NotFound();
            return Ok(trainer);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении тренера с Id {Id}", id);
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Создаёт нового тренера
    /// </summary>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TrainerDto>> Create([FromBody] TrainerEditDto dto)
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
            logger.LogWarning(ex, "Некорректные данные при создании тренера");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при создании тренера");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Обновляет существующего тренера
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> Update(int id, [FromBody] TrainerEditDto dto)
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
            logger.LogWarning(ex, "Некорректные данные при обновлении тренера с Id {Id}", id);
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при обновлении тренера с Id {Id}", id);
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Удаляет тренера по идентификатору
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
            logger.LogError(ex, "Ошибка при удалении тренера с Id {Id}", id);
            return StatusCode(500);
        }
    }
}