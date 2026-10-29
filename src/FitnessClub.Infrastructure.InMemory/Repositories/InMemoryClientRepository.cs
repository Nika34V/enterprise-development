using FitnessClub.Domain;
using FitnessClub.Domain.Interfaces;

namespace FitnessClub.Infrastructure.InMemory.Repositories;

public class InMemoryClientRepository(FitnessClubInMemoryStore store)
    : InMemoryRepository<Client>(store.Clients, c => c.Id, (c, id) => c.Id = id), IClientRepository;