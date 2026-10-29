using FitnessClub.Domain;
using FitnessClub.Domain.Interfaces;

namespace FitnessClub.Infrastructure.InMemory.Repositories;

public class InMemoryEnrollmentRepository(FitnessClubInMemoryStore store)
    : InMemoryRepository<Enrollment>(store.Enrollments, e => e.Id, (e, id) => e.Id = id), IEnrollmentRepository;