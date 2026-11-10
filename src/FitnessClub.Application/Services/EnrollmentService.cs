using FitnessClub.Application.Mapping;
using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Exceptions;
using FitnessClub.Contracts.Interfaces;
using FitnessClub.Domain;
using FitnessClub.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace FitnessClub.Application.Services;

/// <summary>
/// Сервис управления записями на занятия: проверка данных, логирование и вызов репозиториев
/// </summary>
public class EnrollmentService(
    IEnrollmentRepository enrollmentRepository,
    IClientRepository clientRepository,
    ITrainerRepository trainerRepository,
    ILogger<EnrollmentService> logger) : IEnrollmentService
{
    /// <summary>
    /// Возвращает все записи на занятия
    /// </summary>
    public async Task<IReadOnlyList<EnrollmentDto>> GetAllAsync()
    {
        var entities = await enrollmentRepository.GetAllAsync();
        return [.. entities.Select(EnrollmentMapper.ToDto)];
    }

    /// <summary>
    /// Возвращает запись на занятие по идентификатору
    /// </summary>
    /// <exception cref="NotFoundException">Запись не найдена</exception>
    public async Task<EnrollmentDto> GetByIdAsync(int id)
    {
        var entity = await GetEntityAsync(id);
        return EnrollmentMapper.ToDto(entity);
    }

    /// <summary>
    /// Создаёт запись на занятие после проверки данных
    /// </summary>
    /// <exception cref="InvalidRequestException">Данные записи некорректны или клиент/тренер не найден</exception>
    public async Task<EnrollmentDto> CreateAsync(EnrollmentEditDto dto)
    {
        Validate(dto);
        var (client, trainer) = await ResolveReferencesAsync(dto.ClientId, dto.TrainerId);

        var added = await enrollmentRepository.AddAsync(EnrollmentMapper.ToEntity(dto, client, trainer));
        logger.LogInformation("Создана запись на занятие с Id {Id}", added.Id);

        return EnrollmentMapper.ToDto(added);
    }

    /// <summary>
    /// Изменяет запись на занятие после проверки данных
    /// </summary>
    /// <exception cref="NotFoundException">Запись не найдена</exception>
    /// <exception cref="InvalidRequestException">Данные записи некорректны или клиент/тренер не найден</exception>
    public async Task UpdateAsync(int id, EnrollmentEditDto dto)
    {
        var existing = await GetEntityAsync(id);
        Validate(dto);
        var (client, trainer) = await ResolveReferencesAsync(dto.ClientId, dto.TrainerId);

        EnrollmentMapper.ApplyTo(existing, dto, client, trainer);
        await enrollmentRepository.UpdateAsync(existing);
        logger.LogInformation("Обновлена запись на занятие с Id {Id}", id);
    }

    /// <summary>
    /// Удаляет запись на занятие. Если записи уже нет, ошибки не возникает (операция идемпотентна)
    /// </summary>
    public async Task DeleteAsync(int id)
    {
        var deleted = await enrollmentRepository.DeleteAsync(id);

        if (deleted)
            logger.LogInformation("Удалена запись на занятие с Id {Id}", id);
        else
            logger.LogInformation("Запись на занятие с Id {Id} уже отсутствует, удалять нечего", id);
    }

    private async Task<Enrollment> GetEntityAsync(int id)
    {
        var entity = await enrollmentRepository.GetByIdAsync(id);
        if (entity is null)
        {
            logger.LogWarning("Запись на занятие с Id {Id} не найдена", id);
            throw new NotFoundException($"Запись на занятие с Id {id} не найдена");
        }

        return entity;
    }

    private void Validate(EnrollmentEditDto dto)
    {
        if (!Enum.IsDefined(dto.Hall))
            throw Invalid("Указан несуществующий зал");

        if (dto.EndsAt <= dto.StartsAt)
            throw Invalid("Время окончания занятия должно быть позже времени начала");
    }

    private async Task<(Client Client, Trainer Trainer)> ResolveReferencesAsync(int clientId, int trainerId)
    {
        var client = await clientRepository.GetByIdAsync(clientId)
            ?? throw Invalid($"Клиент с Id {clientId} не найден");

        var trainer = await trainerRepository.GetByIdAsync(trainerId)
            ?? throw Invalid($"Тренер с Id {trainerId} не найден");

        return (client, trainer);
    }

    private InvalidRequestException Invalid(string message)
    {
        logger.LogWarning("Некорректные данные записи на занятие: {Message}", message);
        return new InvalidRequestException(message);
    }
}
