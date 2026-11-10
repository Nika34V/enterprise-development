using System.ComponentModel.DataAnnotations;
using FitnessClub.Shared.Enums;

namespace FitnessClub.Contracts.Dto;

/// <summary>
/// DTO для создания или изменения тренера
/// </summary>
public class TrainerEditDto
{
    /// <summary>
    /// Номер паспорта (не длиннее 20 символов)
    /// </summary>
    [Required(ErrorMessage = "Номер паспорта обязателен")]
    [MaxLength(20, ErrorMessage = "Номер паспорта не может быть длиннее 20 символов")]
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Фамилия, имя и отчество (не длиннее 200 символов)
    /// </summary>
    [Required(ErrorMessage = "ФИО обязательно")]
    [MaxLength(200, ErrorMessage = "ФИО не может быть длиннее 200 символов")]
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
    [Required(ErrorMessage = "Специализация обязательна")]
    public required int SpecializationId { get; set; }

    /// <summary>
    /// Стаж работы в полных годах (от 0 до 80)
    /// </summary>
    [Range(0, 80, ErrorMessage = "Стаж должен быть от 0 до 80 лет")]
    public int ExperienceYears { get; set; }
}
