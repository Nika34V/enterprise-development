namespace FitnessClub.Domain.Interfaces;

/// <summary>
/// Универсальный CRUD-репозиторий для сущности с целочисленным идентификатором
/// </summary>
public interface IRepository<TEntity> where TEntity : class
{
    Task<IReadOnlyList<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(int id);
    Task<TEntity> AddAsync(TEntity entity);
    Task<bool> UpdateAsync(TEntity entity);
    Task<bool> DeleteAsync(int id);
}