using FitnessClub.Domain;
using FitnessClub.Domain.Interfaces;

namespace FitnessClub.Infrastructure.InMemory.Repositories;

public class InMemorySpecializationRepository(FitnessClubInMemoryStore store)
    : InMemoryRepository<Specialization>(store.Specializations, s => s.Id, (s, id) => s.Id = id), ISpecializationRepository;