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
    /// <response code="200">Список успешно получен</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IEnumerable<ClientDto>>> GetAll()
    {
        logger.LogInformation("Запрос списка клиентов");
        var items = await service.GetAllAsync();
        return Ok(items);
    }

    /// <summary>
    /// Возвращает клиента по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    /// <response code="200">Данные успешно получены</response>
    /// <response code="404">Объект с таким идентификатором не найден</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<ClientDto>> GetById(int id)
    {
        logger.LogInformation("Запрос клиента с Id {Id}", id);
        var item = await service.GetByIdAsync(id);
        return Ok(item);
    }

    /// <summary>
    /// Создаёт нового клиента
    /// </summary>
    /// <remarks>
    /// Идентификатор присваивается автоматически
    /// </remarks>
    /// <param name="dto">Данные клиента</param>
    /// <response code="201">Успешно создано, в ответе возвращаются данные созданного объекта</response>
    /// <response code="400">Данные некорректны</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<ClientDto>> Create([FromBody] ClientEditDto dto)
    {
        logger.LogInformation("Запрос на создание клиента");
        var created = await service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Изменяет данные клиента
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    /// <param name="dto">Новые данные клиента</param>
    /// <response code="204">Данные изменены</response>
    /// <response code="400">Данные некорректны</response>
    /// <response code="404">Объект с таким идентификатором не найден</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [HttpPut("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> Update(int id, [FromBody] ClientEditDto dto)
    {
        logger.LogInformation("Запрос на изменение клиента с Id {Id}", id);
        await service.UpdateAsync(id, dto);
        return NoContent();
    }

    /// <summary>
    /// Удаляет клиента по идентификатору
    /// </summary>
    /// <remarks>
    /// Операция идемпотентна: если клиента с таким идентификатором нет, метод всё равно возвращает 204
    /// </remarks>
    /// <param name="id">Идентификатор клиента</param>
    /// <response code="204">Успешно: объект удалён или его уже не было</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> Delete(int id)
    {
        logger.LogInformation("Запрос на удаление клиента с Id {Id}", id);
        await service.DeleteAsync(id);
        return NoContent();
    }
}
