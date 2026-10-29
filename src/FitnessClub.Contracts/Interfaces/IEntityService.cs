namespace FitnessClub.Contracts.Interfaces;

/// <summary>
/// Универсальный CRUD-сервис для работы с сущностью через DTO.
/// TDto — DTO для чтения, TEditDto — DTO для создания/изменения
/// </summary>
public interface IEntityService<TDto, TEditDto>
{
    Task<IReadOnlyList<TDto>> GetAllAsync();
    Task<TDto?> GetByIdAsync(int id);
    Task<TDto> CreateAsync(TEditDto dto);
    Task<bool> UpdateAsync(int id, TEditDto dto);
    Task<bool> DeleteAsync(int id);
}