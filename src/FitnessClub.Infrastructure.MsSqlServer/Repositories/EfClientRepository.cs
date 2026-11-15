using FitnessClub.Domain;
using FitnessClub.Domain.Interfaces;

namespace FitnessClub.Infrastructure.MsSqlServer.Repositories;

/// <summary>
/// Репозиторий клиентов на базе Entity Framework
/// </summary>
/// <param name="context">Контекст базы данных</param>
public class EfClientRepository(FitnessClubDbContext context) : EfRepository<Client>(context), IClientRepository;
