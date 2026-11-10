using FitnessClub.Application.Mapping;
using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Exceptions;
using FitnessClub.Contracts.Interfaces;
using FitnessClub.Domain;
using FitnessClub.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace FitnessClub.Application.Services;

/// <summary>
/// Сервис управления справочником специализаций: проверка данных, логирование и вызов репозитория
/// </summary>
public class SpecializationService(ISpecializationRepository repository, ILogger<SpecializationService> logger) : ISpecializationService
{
    /// <summary>
    /// Возвращает все специализации
    /// </summary>
    public async Task<IReadOnlyList<SpecializationDto>> GetAllAsync()
    {
        var entities = await repository.GetAllAsync();
        return [.. entities.Select(SpecializationMapper.ToDto)];
    }

    /// <summary>
    /// Возвращает специализацию по идентификатору
    /// </summary>
    /// <exception cref="NotFoundException">Специализация не найдена</exception>
    public async Task<SpecializationDto> GetByIdAsync(int id)
    {
        var entity = await GetEntityAsync(id);
        return SpecializationMapper.ToDto(entity);
    }

    /// <summary>
    /// Создаёт специализацию после проверки данных
    /// </summary>
    /// <exception cref="InvalidRequestException">Данные специализации некорректны</exception>
    public async Task<SpecializationDto> CreateAsync(SpecializationEditDto dto)
    {
        await ValidateAsync(dto, currentId: null);

        var added = await repository.AddAsync(SpecializationMapper.ToEntity(dto));
        logger.LogInformation("Создана специализация с Id {Id}", added.Id);

        return SpecializationMapper.ToDto(added);
    }

    /// <summary>
    /// Изменяет специализацию после проверки данных
    /// </summary>
    /// <exception cref="NotFoundException">Специализация не найдена</exception>
    /// <exception cref="InvalidRequestException">Данные специализации некорректны</exception>
    public async Task UpdateAsync(int id, SpecializationEditDto dto)
    {
        var existing = await GetEntityAsync(id);
        await ValidateAsync(dto, currentId: id);

        SpecializationMapper.ApplyTo(existing, dto);
        await repository.UpdateAsync(existing);
        logger.LogInformation("Обновлена специализация с Id {Id}", id);
    }

    /// <summary>
    /// Удаляет специализацию. Если её уже нет, ошибки не возникает (операция идемпотентна)
    /// </summary>
    public async Task DeleteAsync(int id)
    {
        var deleted = await repository.DeleteAsync(id);

        if (deleted)
            logger.LogInformation("Удалена специализация с Id {Id}", id);
        else
            logger.LogInformation("Специализация с Id {Id} уже отсутствует, удалять нечего", id);
    }

    private async Task<Specialization> GetEntityAsync(int id)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity is null)
        {
            logger.LogWarning("Специализация с Id {Id} не найдена", id);
            throw new NotFoundException($"Специализация с Id {id} не найдена");
        }

        return entity;
    }

    private async Task ValidateAsync(SpecializationEditDto dto, int? currentId)
    {
        var items = await repository.GetAllAsync();
        var duplicate = items.Any(s =>
            string.Equals(s.Name, dto.Name.Trim(), StringComparison.OrdinalIgnoreCase) && s.Id != currentId);

        if (duplicate)
        {
            logger.LogWarning("Некорректные данные специализации: название {Name} уже существует", dto.Name);
            throw new InvalidRequestException("Специализация с таким названием уже существует");
        }
    }
}
