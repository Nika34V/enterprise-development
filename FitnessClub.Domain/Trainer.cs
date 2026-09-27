namespace FitnessClub.Domain;

/// <summary>
/// Тренер фитнес-клуба
/// </summary>
public class Trainer : Person
{
    /// <summary>
    /// Специализация тренера из справочника <see cref="Domain.Specialization"/>
    /// </summary>
    public required Specialization Specialization { get; set; }

    /// <summary>
    /// Стаж работы в полных годах
    /// </summary>
    public required int ExperienceYears { get; set; }
}