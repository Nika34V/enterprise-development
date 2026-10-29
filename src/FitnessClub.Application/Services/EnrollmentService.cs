using FitnessClub.Application.Mapping;
using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Interfaces;
using FitnessClub.Domain;
using FitnessClub.Domain.Interfaces;

namespace FitnessClub.Application.Services;

/// <summary>
/// Сервис управления записями на занятия
/// </summary>
public class EnrollmentService(
    IEnrollmentRepository enrollmentRepository,
    IClientRepository clientRepository,
    ITrainerRepository trainerRepository) : IEnrollmentService
{
    public async Task<IReadOnlyList<EnrollmentDto>> GetAllAsync()
    {
        var entities = await enrollmentRepository.GetAllAsync();
        return entities.Select(EnrollmentMapper.ToDto).ToList();
    }

    public async Task<EnrollmentDto?> GetByIdAsync(int id)
    {
        var entity = await enrollmentRepository.GetByIdAsync(id);
        return entity == null ? null : EnrollmentMapper.ToDto(entity);
    }

    public async Task<EnrollmentDto> CreateAsync(EnrollmentEditDto dto)
    {
        var (client, trainer) = await ResolveReferencesAsync(dto.ClientId, dto.TrainerId);

        var entity = EnrollmentMapper.ToEntity(dto, client, trainer);
        var added = await enrollmentRepository.AddAsync(entity);
        return EnrollmentMapper.ToDto(added);
    }

    public async Task<bool> UpdateAsync(int id, EnrollmentEditDto dto)
    {
        var existing = await enrollmentRepository.GetByIdAsync(id);
        if (existing == null)
            return false;

        var (client, trainer) = await ResolveReferencesAsync(dto.ClientId, dto.TrainerId);

        EnrollmentMapper.ApplyTo(existing, dto, client, trainer);
        return await enrollmentRepository.UpdateAsync(existing);
    }

    public Task<bool> DeleteAsync(int id) => enrollmentRepository.DeleteAsync(id);

    private async Task<(Client Client, Trainer Trainer)> ResolveReferencesAsync(int clientId, int trainerId)
    {
        var client = await clientRepository.GetByIdAsync(clientId)
            ?? throw new ArgumentException($"Клиент с Id {clientId} не найден");

        var trainer = await trainerRepository.GetByIdAsync(trainerId)
            ?? throw new ArgumentException($"Тренер с Id {trainerId} не найден");

        return (client, trainer);
    }
}