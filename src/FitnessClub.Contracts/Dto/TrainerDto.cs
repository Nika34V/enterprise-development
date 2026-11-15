using FitnessClub.Shared.Enums;

namespace FitnessClub.Contracts.Dto;

/// <summary>
/// DTO тренера фитнес-клуба
/// </summary>
public class TrainerDto
{
    /// <summary>
    /// Уникальный идентификатор тренера
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Номер паспорта
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Фамилия, имя и отчество
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Пол
    /// </summary>
    public Gender Gender { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateOnly BirthDate { get; set; }

    /// <summary>
    /// Идентификатор специализации из справочника
    /// </summary>
    public int SpecializationId { get; set; }

    /// <summary>
    /// Название специализации
    /// </summary>
    public string? SpecializationName { get; set; }

    /// <summary>
    /// Стаж работы в полных годах
    /// </summary>
    public int ExperienceYears { get; set; }
}
