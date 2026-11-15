using FitnessClub.Domain;
using FitnessClub.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Infrastructure.MsSqlServer.Repositories;

/// <summary>
/// Репозиторий тренеров на базе Entity Framework
/// </summary>
/// <param name="context">Контекст базы данных</param>
public class EfTrainerRepository(FitnessClubDbContext context) : EfRepository<Trainer>(context), ITrainerRepository
{
    /// <summary>
    /// Тренеры вместе со специализацией
    /// </summary>
    protected override IQueryable<Trainer> Query => Set.Include(t => t.Specialization);
}
