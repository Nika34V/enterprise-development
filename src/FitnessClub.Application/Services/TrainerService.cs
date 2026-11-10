using FitnessClub.Application.Mapping;
using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Exceptions;
using FitnessClub.Contracts.Interfaces;
using FitnessClub.Domain;
using FitnessClub.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace FitnessClub.Application.Services;

/// <summary>
/// Сервис управления тренерами: проверка данных, логирование и вызов репозиториев
/// </summary>
public class TrainerService(
    ITrainerRepository trainerRepository,
    ISpecializationRepository specializationRepository,
    ILogger<TrainerService> logger) : ITrainerService
{
    /// <summary>
    /// Возвращает всех тренеров
    /// </summary>
    /// <returns>Список тренеров в виде DTO</returns>
    public async Task<IReadOnlyList<TrainerDto>> GetAllAsync()
    {
        var entities = await trainerRepository.GetAllAsync();
        return [.. entities.Select(TrainerMapper.ToDto)];
    }

    /// <summary>
    /// Возвращает тренера по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор тренера</param>
    /// <returns>DTO тренера</returns>
    /// <exception cref="NotFoundException">Тренер не найден</exception>
    public async Task<TrainerDto> GetByIdAsync(int id)
    {
        var entity = await GetEntityAsync(id);
        return TrainerMapper.ToDto(entity);
    }

    /// <summary>
    /// Создаёт тренера после проверки данных
    /// </summary>
    /// <param name="dto">Данные тренера</param>
    /// <returns>DTO тренера с присвоенным идентификатором</returns>
    /// <exception cref="InvalidRequestException">Данные тренера некорректны</exception>
    public async Task<TrainerDto> CreateAsync(TrainerEditDto dto)
    {
        await ValidateAsync(dto, currentId: null);
        var specialization = await GetSpecializationAsync(dto.SpecializationId);

        var added = await trainerRepository.AddAsync(TrainerMapper.ToEntity(dto, specialization));
        logger.LogInformation("Создан тренер с Id {Id}", added.Id);

        return TrainerMapper.ToDto(added);
    }

    /// <summary>
    /// Изменяет данные тренера после проверки
    /// </summary>
    /// <param name="id">Идентификатор тренера</param>
    /// <param name="dto">Новые данные тренера</param>
    /// <returns>Задача, завершающаяся после сохранения изменений</returns>
    /// <exception cref="NotFoundException">Тренер не найден</exception>
    /// <exception cref="InvalidRequestException">Данные тренера некорректны</exception>
    public async Task UpdateAsync(int id, TrainerEditDto dto)
    {
        var existing = await GetEntityAsync(id);
        await ValidateAsync(dto, currentId: id);
        var specialization = await GetSpecializationAsync(dto.SpecializationId);

        TrainerMapper.ApplyTo(existing, dto, specialization);
        await trainerRepository.UpdateAsync(existing);
        logger.LogInformation("Обновлён тренер с Id {Id}", id);
    }

    /// <summary>
    /// Удаляет тренера. Если тренера уже нет, ошибки не возникает (операция идемпотентна)
    /// </summary>
    /// <param name="id">Идентификатор тренера</param>
    /// <returns>Задача, завершающаяся после удаления; если объекта уже нет, ошибки не возникает</returns>
    public async Task DeleteAsync(int id)
    {
        var deleted = await trainerRepository.DeleteAsync(id);

        if (deleted)
            logger.LogInformation("Удалён тренер с Id {Id}", id);
        else
            logger.LogInformation("Тренер с Id {Id} уже отсутствует, удалять нечего", id);
    }

    private async Task<Trainer> GetEntityAsync(int id)
    {
        var entity = await trainerRepository.GetByIdAsync(id);
        if (entity is null)
        {
            logger.LogWarning("Тренер с Id {Id} не найден", id);
            throw new NotFoundException($"Тренер с Id {id} не найден");
        }

        return entity;
    }

    private async Task<Specialization> GetSpecializationAsync(int specializationId)
    {
        var specialization = await specializationRepository.GetByIdAsync(specializationId);
        if (specialization is null)
            throw Invalid($"Специализация с Id {specializationId} не найдена");

        return specialization;
    }

    private async Task ValidateAsync(TrainerEditDto dto, int? currentId)
    {
        if (!Enum.IsDefined(dto.Gender))
            throw Invalid("Указан некорректный пол");

        if (dto.BirthDate > DateOnly.FromDateTime(DateTime.Today))
            throw Invalid("Дата рождения не может быть в будущем");

        var trainers = await trainerRepository.GetAllAsync();
        if (trainers.Any(t => t.PassportNumber == dto.PassportNumber && t.Id != currentId))
            throw Invalid("Тренер с таким номером паспорта уже существует");
    }

    private InvalidRequestException Invalid(string message)
    {
        logger.LogWarning("Некорректные данные тренера: {Message}", message);
        return new InvalidRequestException(message);
    }
}
