using FitnessClub.Application.Mapping;
using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Exceptions;
using FitnessClub.Contracts.Interfaces;
using FitnessClub.Domain;
using FitnessClub.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace FitnessClub.Application.Services;

/// <summary>
/// Сервис управления клиентами: проверка данных, логирование и вызов репозитория
/// </summary>
public class ClientService(IClientRepository repository, ILogger<ClientService> logger) : IClientService
{
    /// <summary>
    /// Возвращает всех клиентов
    /// </summary>
    public async Task<IReadOnlyList<ClientDto>> GetAllAsync()
    {
        var entities = await repository.GetAllAsync();
        return [.. entities.Select(ClientMapper.ToDto)];
    }

    /// <summary>
    /// Возвращает клиента по идентификатору
    /// </summary>
    /// <exception cref="NotFoundException">Клиент не найден</exception>
    public async Task<ClientDto> GetByIdAsync(int id)
    {
        var entity = await GetEntityAsync(id);
        return ClientMapper.ToDto(entity);
    }

    /// <summary>
    /// Создаёт клиента после проверки данных
    /// </summary>
    /// <exception cref="InvalidRequestException">Данные клиента некорректны</exception>
    public async Task<ClientDto> CreateAsync(ClientEditDto dto)
    {
        await ValidateAsync(dto, currentId: null);

        var added = await repository.AddAsync(ClientMapper.ToEntity(dto));
        logger.LogInformation("Создан клиент с Id {Id}", added.Id);

        return ClientMapper.ToDto(added);
    }

    /// <summary>
    /// Изменяет данные клиента после проверки
    /// </summary>
    /// <exception cref="NotFoundException">Клиент не найден</exception>
    /// <exception cref="InvalidRequestException">Данные клиента некорректны</exception>
    public async Task UpdateAsync(int id, ClientEditDto dto)
    {
        var existing = await GetEntityAsync(id);
        await ValidateAsync(dto, currentId: id);

        ClientMapper.ApplyTo(existing, dto);
        await repository.UpdateAsync(existing);
        logger.LogInformation("Обновлён клиент с Id {Id}", id);
    }

    /// <summary>
    /// Удаляет клиента. Если клиента уже нет, ошибки не возникает (операция идемпотентна)
    /// </summary>
    public async Task DeleteAsync(int id)
    {
        var deleted = await repository.DeleteAsync(id);

        if (deleted)
            logger.LogInformation("Удалён клиент с Id {Id}", id);
        else
            logger.LogInformation("Клиент с Id {Id} уже отсутствует, удалять нечего", id);
    }

    private async Task<Client> GetEntityAsync(int id)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity is null)
        {
            logger.LogWarning("Клиент с Id {Id} не найден", id);
            throw new NotFoundException($"Клиент с Id {id} не найден");
        }

        return entity;
    }

    private async Task ValidateAsync(ClientEditDto dto, int? currentId)
    {
        if (!Enum.IsDefined(dto.Gender))
            throw Invalid("Указан некорректный пол");

        if (dto.BirthDate > DateOnly.FromDateTime(DateTime.Today))
            throw Invalid("Дата рождения не может быть в будущем");

        if (dto.MembershipEnd < dto.MembershipStart)
            throw Invalid("Дата окончания абонемента не может быть раньше даты начала");

        var clients = await repository.GetAllAsync();
        if (clients.Any(c => c.PassportNumber == dto.PassportNumber && c.Id != currentId))
            throw Invalid("Клиент с таким номером паспорта уже существует");
    }

    private InvalidRequestException Invalid(string message)
    {
        logger.LogWarning("Некорректные данные клиента: {Message}", message);
        return new InvalidRequestException(message);
    }
}
