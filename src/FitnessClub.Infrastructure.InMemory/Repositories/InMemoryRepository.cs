using FitnessClub.Domain.Interfaces;

namespace FitnessClub.Infrastructure.InMemory.Repositories;

/// <summary>
/// Базовая реализация CRUD-репозитория поверх списка в памяти
/// </summary>
public abstract class InMemoryRepository<TEntity>(
    List<TEntity> items,
    Func<TEntity, int> getId,
    Action<TEntity, int> setId) : IRepository<TEntity> where TEntity : class
{
    protected List<TEntity> Items { get; } = items;

    public Task<IReadOnlyList<TEntity>> GetAllAsync() =>
        Task.FromResult<IReadOnlyList<TEntity>>(Items.ToList());

    public Task<TEntity?> GetByIdAsync(int id) =>
        Task.FromResult(Items.FirstOrDefault(e => getId(e) == id));

    public Task<TEntity> AddAsync(TEntity entity)
    {
        var newId = Items.Count != 0 ? Items.Max(getId) + 1 : 1;
        setId(entity, newId);
        Items.Add(entity);
        return Task.FromResult(entity);
    }

    public Task<bool> UpdateAsync(TEntity entity)
    {
        var id = getId(entity);
        var index = Items.FindIndex(e => getId(e) == id);
        if (index == -1)
            return Task.FromResult(false);

        Items[index] = entity;
        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var item = Items.FirstOrDefault(e => getId(e) == id);
        if (item == null)
            return Task.FromResult(false);

        Items.Remove(item);
        return Task.FromResult(true);
    }
}