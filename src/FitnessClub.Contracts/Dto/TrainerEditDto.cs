using System.ComponentModel.DataAnnotations;
using FitnessClub.Domain;

namespace FitnessClub.Contracts.Dto;

/// <summary>
/// DTO для создания или изменения тренера
/// </summary>
public class TrainerEditDto
{
    [Required(ErrorMessage = "Номер паспорта обязателен")]
    [MaxLength(20, ErrorMessage = "Номер паспорта не может быть длиннее 20 символов")]
    public required string PassportNumber { get; set; }

    [Required(ErrorMessage = "ФИО обязательно")]
    [MaxLength(200, ErrorMessage = "ФИО не может быть длиннее 200 символов")]
    public required string FullName { get; set; }

    public Gender Gender { get; set; }

    public DateOnly BirthDate { get; set; }

    [Required(ErrorMessage = "Специализация обязательна")]
    public required int SpecializationId { get; set; }

    [Range(0, 80, ErrorMessage = "Стаж должен быть от 0 до 80 лет")]
    public int ExperienceYears { get; set; }
}