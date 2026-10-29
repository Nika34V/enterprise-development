using FitnessClub.Application.Mapping;
using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Interfaces;
using FitnessClub.Domain.Interfaces;

namespace FitnessClub.Application.Services;

/// <summary>
/// Сервис управления справочником специализаций
/// </summary>
public class SpecializationService(ISpecializationRepository repository) : ISpecializationService
{
    public async Task<IReadOnlyList<SpecializationDto>> GetAllAsync()
    {
        var entities = await repository.GetAllAsync();
        return entities.Select(SpecializationMapper.ToDto).ToList();
    }

    public async Task<SpecializationDto?> GetByIdAsync(int id)
    {
        var entity = await repository.GetByIdAsync(id);
        return entity == null ? null : SpecializationMapper.ToDto(entity);
    }

    public async Task<SpecializationDto> CreateAsync(SpecializationEditDto dto)
    {
        var entity = SpecializationMapper.ToEntity(dto);
        var added = await repository.AddAsync(entity);
        return SpecializationMapper.ToDto(added);
    }

    public async Task<bool> UpdateAsync(int id, SpecializationEditDto dto)
    {
        var existing = await repository.GetByIdAsync(id);
        if (existing == null)
            return false;

        SpecializationMapper.ApplyTo(existing, dto);
        return await repository.UpdateAsync(existing);
    }

    public Task<bool> DeleteAsync(int id) => repository.DeleteAsync(id);
}