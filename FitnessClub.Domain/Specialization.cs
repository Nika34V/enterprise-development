namespace FitnessClub.Domain;

/// <summary>
/// Справочник специализаций тренеров
/// </summary>
public class Specialization
{
    /// <summary>
    /// Уникальный идентификатор специализации
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Название специализации
    /// </summary>
    public required string Name { get; set; }
}