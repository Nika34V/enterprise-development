using FitnessClub.Domain;

namespace FitnessClub.Contracts.Dto;

/// <summary>
/// DTO тренера фитнес-клуба
/// </summary>
public class TrainerDto
{
    public int Id { get; set; }
    public required string PassportNumber { get; set; }
    public required string FullName { get; set; }
    public Gender Gender { get; set; }
    public DateOnly BirthDate { get; set; }
    public int SpecializationId { get; set; }
    public string? SpecializationName { get; set; }
    public int ExperienceYears { get; set; }
}