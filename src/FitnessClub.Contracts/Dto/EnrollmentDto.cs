using FitnessClub.Domain;

namespace FitnessClub.Contracts.Dto;

/// <summary>
/// DTO записи клиента на занятие 
/// </summary>
public class EnrollmentDto
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public string? ClientName { get; set; }
    public int TrainerId { get; set; }
    public string? TrainerName { get; set; }
    public Hall Hall { get; set; }
    public DateTime StartsAt { get; set; }
    public DateTime EndsAt { get; set; }
    public bool IsTrial { get; set; }
}