namespace FitnessClub.Domain.Tests;

/// <summary>
/// Общие тестовые данные, создаются один раз и расшариваются между всеми тестами класса
/// </summary>
public class FitnessClubFixture
{
    /// <summary>
    /// Зафиксированный момент «сейчас», чтобы результаты не зависели от дня запуска
    /// </summary>
    public DateTime Now { get; } = new(2026, 5, 20, 11, 30, 0);

    /// <summary>
    /// Дата, соответствующая моменту <see cref="Now"/>
    /// </summary>
    public DateOnly Today => DateOnly.FromDateTime(Now);

    /// <summary>
    /// Справочник специализаций
    /// </summary>
    public List<Specialization> Specializations { get; }

    /// <summary>
    /// Тренеры
    /// </summary>
    public List<Trainer> Trainers { get; }

    /// <summary>
    /// Клиенты
    /// </summary>
    public List<Client> Clients { get; }

    /// <summary>
    /// Записи на занятия
    /// </summary>
    public List<Enrollment> Enrollments { get; }

    /// <summary>
    /// Заполняет коллекции данными из датасида
    /// </summary>
    public FitnessClubFixture()
    {
        Specializations = DataSeeder.GetSpecializations();
        Trainers = DataSeeder.GetTrainers(Specializations);
        Clients = DataSeeder.GetClients();
        Enrollments = DataSeeder.GetEnrollments(Clients, Trainers, Now);
    }
}