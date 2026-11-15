using FitnessClub.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Infrastructure.MsSqlServer.Repositories;

/// <summary>
/// Базовая реализация CRUD-репозитория поверх Entity Framework
/// </summary>
/// <typeparam name="TEntity">Тип сущности</typeparam>
/// <param name="context">Контекст базы данных</param>
public abstract class EfRepository<TEntity>(FitnessClubDbContext context) : IRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Таблица сущности
    /// </summary>
    protected DbSet<TEntity> Set => context.Set<TEntity>();

    /// <summary>
    /// Запрос, по умолчанию возвращающий всю таблицу. Наследники добавляют сюда связанные таблицы (Include)
    /// </summary>
    protected virtual IQueryable<TEntity> Query => Set;

    /// <summary>
    /// Возвращает все сущности
    /// </summary>
    /// <returns>Список сущностей</returns>
    public async Task<IReadOnlyList<TEntity>> GetAllAsync() =>
        await Query.AsNoTracking().ToListAsync();

    /// <summary>
    /// Возвращает сущность по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <returns>Найденная сущность или null</returns>
    public async Task<TEntity?> GetByIdAsync(int id) =>
        await Query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);

    /// <summary>
    /// Добавляет сущность в базу; идентификатор присваивает сама база
    /// </summary>
    /// <param name="entity">Добавляемая сущность</param>
    /// <returns>Добавленная сущность с идентификатором</returns>
    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await Set.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Сохраняет изменения сущности
    /// </summary>
    /// <param name="entity">Изменённая сущность</param>
    /// <returns>true после успешного сохранения</returns>
    public async Task<bool> UpdateAsync(TEntity entity)
    {
        var entry = context.Entry(entity);
        if (entry.State == EntityState.Detached)
            entry.State = EntityState.Modified;

        await context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Удаляет сущность по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <returns>true, если сущность найдена и удалена; иначе false</returns>
    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await Set.FindAsync(id);
        if (entity is null)
            return false;

        Set.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }
}
