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
    /// <response code="200">Список успешно получен</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IEnumerable<EnrollmentDto>>> GetAll()
    {
        logger.LogInformation("Запрос списка записей на занятия");
        var items = await service.GetAllAsync();
        return Ok(items);
    }

    /// <summary>
    /// Возвращает запись на занятие по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи на занятие</param>
    /// <response code="200">Данные успешно получены</response>
    /// <response code="404">Объект с таким идентификатором не найден</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<EnrollmentDto>> GetById(int id)
    {
        logger.LogInformation("Запрос записи на занятие с Id {Id}", id);
        var item = await service.GetByIdAsync(id);
        return Ok(item);
    }

    /// <summary>
    /// Создаёт новую запись на занятие
    /// </summary>
    /// <remarks>
    /// Идентификатор присваивается автоматически
    /// </remarks>
    /// <param name="dto">Данные записи на занятие</param>
    /// <response code="201">Успешно создано, в ответе возвращаются данные созданного объекта</response>
    /// <response code="400">Данные некорректны</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<EnrollmentDto>> Create([FromBody] EnrollmentEditDto dto)
    {
        logger.LogInformation("Запрос на создание записи на занятие");
        var created = await service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Изменяет данные записи на занятие
    /// </summary>
    /// <param name="id">Идентификатор записи на занятие</param>
    /// <param name="dto">Новые данные записи на занятие</param>
    /// <response code="204">Данные изменены</response>
    /// <response code="400">Данные некорректны</response>
    /// <response code="404">Объект с таким идентификатором не найден</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [HttpPut("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> Update(int id, [FromBody] EnrollmentEditDto dto)
    {
        logger.LogInformation("Запрос на изменение записи на занятие с Id {Id}", id);
        await service.UpdateAsync(id, dto);
        return NoContent();
    }

    /// <summary>
    /// Удаляет запись на занятие по идентификатору
    /// </summary>
    /// <remarks>
    /// Операция идемпотентна: если записи на занятие с таким идентификатором нет, метод всё равно возвращает 204
    /// </remarks>
    /// <param name="id">Идентификатор записи на занятие</param>
    /// <response code="204">Успешно: объект удалён или его уже не было</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> Delete(int id)
    {
        logger.LogInformation("Запрос на удаление записи на занятие с Id {Id}", id);
        await service.DeleteAsync(id);
        return NoContent();
    }
}
