using FitnessClub.Application.Mapping;
using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Interfaces;
using FitnessClub.Domain.Interfaces;

namespace FitnessClub.Application.Services;

/// <summary>
/// Сервис управления тренерами
/// </summary>
public class TrainerService(ITrainerRepository trainerRepository, ISpecializationRepository specializationRepository) : ITrainerService
{
    public async Task<IReadOnlyList<TrainerDto>> GetAllAsync()
    {
        var entities = await trainerRepository.GetAllAsync();
        return entities.Select(TrainerMapper.ToDto).ToList();
    }

    public async Task<TrainerDto?> GetByIdAsync(int id)
    {
        var entity = await trainerRepository.GetByIdAsync(id);
        return entity == null ? null : TrainerMapper.ToDto(entity);
    }

    public async Task<TrainerDto> CreateAsync(TrainerEditDto dto)
    {
        var specialization = await specializationRepository.GetByIdAsync(dto.SpecializationId)
            ?? throw new ArgumentException($"Специализация с Id {dto.SpecializationId} не найдена");

        var entity = TrainerMapper.ToEntity(dto, specialization);
        var added = await trainerRepository.AddAsync(entity);
        return TrainerMapper.ToDto(added);
    }

    public async Task<bool> UpdateAsync(int id, TrainerEditDto dto)
    {
        var existing = await trainerRepository.GetByIdAsync(id);
        if (existing == null)
            return false;

        var specialization = await specializationRepository.GetByIdAsync(dto.SpecializationId)
            ?? throw new ArgumentException($"Специализация с Id {dto.SpecializationId} не найдена");

        TrainerMapper.ApplyTo(existing, dto, specialization);
        return await trainerRepository.UpdateAsync(existing);
    }

    public Task<bool> DeleteAsync(int id) => trainerRepository.DeleteAsync(id);
}