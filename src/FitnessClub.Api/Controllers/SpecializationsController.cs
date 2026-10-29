using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Controllers;

/// <summary>
/// Контроллер для управления справочником специализаций
/// </summary>
[ApiController]
[Route("api/specializations")]
public class SpecializationsController(ISpecializationService service, ILogger<SpecializationsController> logger) : ControllerBase
{
    /// <summary>
    /// Возвращает список всех специализаций
    /// </summary>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IEnumerable<SpecializationDto>>> GetAll()
    {
        try
        {
            return Ok(await service.GetAllAsync());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении списка специализаций");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Возвращает специализацию по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<SpecializationDto>> GetById(int id)
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
            logger.LogError(ex, "Ошибка при получении специализации с Id {Id}", id);
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Создаёт новую специализацию
    /// </summary>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<SpecializationDto>> Create([FromBody] SpecializationEditDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var created = await service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при создании специализации");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Обновляет существующую специализацию
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> Update(int id, [FromBody] SpecializationEditDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var updated = await service.UpdateAsync(id, dto);
            return updated ? NoContent() : NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при обновлении специализации с Id {Id}", id);
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Удаляет специализацию по идентификатору
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
            logger.LogError(ex, "Ошибка при удалении специализации с Id {Id}", id);
            return StatusCode(500);
        }
    }
}