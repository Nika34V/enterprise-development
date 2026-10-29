using FitnessClub.Application.Mapping;
using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Interfaces;
using FitnessClub.Domain.Interfaces;

namespace FitnessClub.Application.Services;

/// <summary>
/// Сервис управления клиентами
/// </summary>
public class ClientService(IClientRepository repository) : IClientService
{
    public async Task<IReadOnlyList<ClientDto>> GetAllAsync()
    {
        var entities = await repository.GetAllAsync();
        return entities.Select(ClientMapper.ToDto).ToList();
    }

    public async Task<ClientDto?> GetByIdAsync(int id)
    {
        var entity = await repository.GetByIdAsync(id);
        return entity == null ? null : ClientMapper.ToDto(entity);
    }

    public async Task<ClientDto> CreateAsync(ClientEditDto dto)
    {
        var entity = ClientMapper.ToEntity(dto);
        var added = await repository.AddAsync(entity);
        return ClientMapper.ToDto(added);
    }

    public async Task<bool> UpdateAsync(int id, ClientEditDto dto)
    {
        var existing = await repository.GetByIdAsync(id);
        if (existing == null)
            return false;

        ClientMapper.ApplyTo(existing, dto);
        return await repository.UpdateAsync(existing);
    }

    public Task<bool> DeleteAsync(int id) => repository.DeleteAsync(id);
}