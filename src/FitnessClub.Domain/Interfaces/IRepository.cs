namespace FitnessClub.Domain.Interfaces;

/// <summary>
/// Универсальный CRUD-репозиторий для сущности с целочисленным идентификатором
/// </summary>
/// <typeparam name="TEntity">Тип сущности</typeparam>
public interface IRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Возвращает все сущности
    /// </summary>
    /// <returns>Список всех сущностей</returns>
    public Task<IReadOnlyList<TEntity>> GetAllAsync();

    /// <summary>
    /// Возвращает сущность по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <returns>Найденная сущность или <c>null</c>, если сущности с таким идентификатором нет</returns>
    public Task<TEntity?> GetByIdAsync(int id);

    /// <summary>
    /// Добавляет новую сущность и присваивает ей идентификатор
    /// </summary>
    /// <param name="entity">Добавляемая сущность</param>
    /// <returns>Добавленная сущность с присвоенным идентификатором</returns>
    public Task<TEntity> AddAsync(TEntity entity);

    /// <summary>
    /// Обновляет существующую сущность
    /// </summary>
    /// <param name="entity">Сущность с обновлёнными данными</param>
    /// <returns><c>true</c>, если сущность найдена и обновлена; иначе <c>false</c></returns>
    public Task<bool> UpdateAsync(TEntity entity);

    /// <summary>
    /// Удаляет сущность по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <returns><c>true</c>, если сущность найдена и удалена; иначе <c>false</c></returns>
    public Task<bool> DeleteAsync(int id);
}
