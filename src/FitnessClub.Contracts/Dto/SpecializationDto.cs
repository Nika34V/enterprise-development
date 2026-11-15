namespace FitnessClub.Contracts.Dto;

/// <summary>
/// DTO специализации тренера
/// </summary>
public class SpecializationDto
{
    /// <summary>
    /// Уникальный идентификатор специализации
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название специализации
    /// </summary>
    public required string Name { get; set; }
}
