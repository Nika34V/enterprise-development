using FitnessClub.Contracts.Dto;

namespace FitnessClub.Contracts.Interfaces;

/// <summary>
/// Сервис управления клиентами
/// </summary>
public interface IClientService : IEntityService<ClientDto, ClientEditDto>;