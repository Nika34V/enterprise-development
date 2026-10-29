using FitnessClub.Domain;

namespace FitnessClub.Infrastructure.InMemory;

/// <summary>
/// Хранилище данных в памяти. Заполняется один раз при старте через датасидер из Domain
/// </summary>
public class FitnessClubInMemoryStore
{
    public List<Specialization> Specializations { get; }
    public List<Trainer> Trainers { get; }
    public List<Client> Clients { get; }
    public List<Enrollment> Enrollments { get; }

    public FitnessClubInMemoryStore()
    {
        Specializations = DataSeeder.GetSpecializations();
        Trainers = DataSeeder.GetTrainers(Specializations);
        Clients = DataSeeder.GetClients();
        Enrollments = DataSeeder.GetEnrollments(Clients, Trainers, DateTime.Now);
    }
}