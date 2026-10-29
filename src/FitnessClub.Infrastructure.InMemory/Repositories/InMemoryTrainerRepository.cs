using FitnessClub.Domain;
using FitnessClub.Domain.Interfaces;

namespace FitnessClub.Infrastructure.InMemory.Repositories;

public class InMemoryTrainerRepository(FitnessClubInMemoryStore store)
    : InMemoryRepository<Trainer>(store.Trainers, t => t.Id, (t, id) => t.Id = id), ITrainerRepository;