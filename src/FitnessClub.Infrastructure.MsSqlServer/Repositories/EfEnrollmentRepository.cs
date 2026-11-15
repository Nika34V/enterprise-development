using FitnessClub.Domain;
using FitnessClub.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Infrastructure.MsSqlServer.Repositories;

/// <summary>
/// Репозиторий записей на занятия на базе Entity Framework
/// </summary>
/// <param name="context">Контекст базы данных</param>
public class EfEnrollmentRepository(FitnessClubDbContext context) : EfRepository<Enrollment>(context), IEnrollmentRepository
{
    /// <summary>
    /// Записи вместе с клиентом, тренером и специализацией тренера
    /// </summary>
    protected override IQueryable<Enrollment> Query => Set
        .Include(e => e.Client)
        .Include(e => e.Trainer)
        .ThenInclude(t => t.Specialization);
}
