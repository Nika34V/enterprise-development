using FitnessClub.Domain;
using FitnessClub.Domain.Interfaces;

namespace FitnessClub.Infrastructure.MsSqlServer.Repositories;

/// <summary>
/// Репозиторий специализаций на базе Entity Framework
/// </summary>
/// <param name="context">Контекст базы данных</param>
public class EfSpecializationRepository(FitnessClubDbContext context) : EfRepository<Specialization>(context), ISpecializationRepository;
