using FitnessClub.Contracts.Exceptions;

namespace FitnessClub.Contracts.Interfaces;

/// <summary>
/// Универсальный CRUD-сервис для работы с сущностью через DTO
/// </summary>
/// <typeparam name="TDto">DTO для чтения</typeparam>
/// <typeparam name="TEditDto">DTO для создания и изменения</typeparam>
public interface IEntityService<TDto, TEditDto>
{
    /// <summary>
    /// Возвращает все сущности
    /// </summary>
    /// <returns>Список DTO</returns>
    public Task<IReadOnlyList<TDto>> GetAllAsync();

    /// <summary>
    /// Возвращает сущность по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <returns>DTO найденной сущности</returns>
    /// <exception cref="NotFoundException">Сущность с таким идентификатором не найдена</exception>
    public Task<TDto> GetByIdAsync(int id);

    /// <summary>
    /// Создаёт новую сущность
    /// </summary>
    /// <param name="dto">Данные новой сущности</param>
    /// <returns>DTO созданной сущности</returns>
    /// <exception cref="InvalidRequestException">Переданные данные некорректны</exception>
    public Task<TDto> CreateAsync(TEditDto dto);

    /// <summary>
    /// Изменяет существующую сущность
    /// </summary>
    /// <param name="id">Идентификатор изменяемой сущности</param>
    /// <param name="dto">Новые данные сущности</param>
    /// <exception cref="NotFoundException">Сущность с таким идентификатором не найдена</exception>
    /// <exception cref="InvalidRequestException">Переданные данные некорректны</exception>
    public Task UpdateAsync(int id, TEditDto dto);

    /// <summary>
    /// Удаляет сущность по идентификатору.
    /// Операция идемпотентна: если сущности уже нет, метод завершается без ошибки
    /// </summary>
    /// <param name="id">Идентификатор удаляемой сущности</param>
    public Task DeleteAsync(int id);
}
