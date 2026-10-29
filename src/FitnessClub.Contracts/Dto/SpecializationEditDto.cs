using System.ComponentModel.DataAnnotations;

namespace FitnessClub.Contracts.Dto;

/// <summary>
/// DTO для создания или изменения специализации
/// </summary>
public class SpecializationEditDto
{
    [Required(ErrorMessage = "Название специализации обязательно")]
    [MaxLength(100, ErrorMessage = "Название не может быть длиннее 100 символов")]
    public required string Name { get; set; }
}