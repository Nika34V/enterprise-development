using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Controllers;

/// <summary>
/// Контроллер для управления клиентами
/// </summary>
[ApiController]
[Route("api/clients")]
public class ClientsController(IClientService service, ILogger<ClientsController> logger) : ControllerBase
{
    /// <summary>
    /// Возвращает список всех клиентов
    /// </summary>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IEnumerable<ClientDto>>> GetAll()
    {
        try
        {
            return Ok(await service.GetAllAsync());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении списка клиентов");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Возвращает клиента по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<ClientDto>> GetById(int id)
    {
        try
        {
            var client = await service.GetByIdAsync(id);
            if (client == null)
            {
                logger.LogWarning("Клиент с Id {Id} не найден", id);
                return NotFound();
            }
            return Ok(client);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении клиента с Id {Id}", id);
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Создаёт нового клиента
    /// </summary>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<ClientDto>> Create([FromBody] ClientEditDto dto)
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
            logger.LogError(ex, "Ошибка при создании клиента");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Обновляет существующего клиента
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> Update(int id, [FromBody] ClientEditDto dto)
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
            logger.LogError(ex, "Ошибка при обновлении клиента с Id {Id}", id);
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Удаляет клиента по идентификатору
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
            logger.LogError(ex, "Ошибка при удалении клиента с Id {Id}", id);
            return StatusCode(500);
        }
    }
}