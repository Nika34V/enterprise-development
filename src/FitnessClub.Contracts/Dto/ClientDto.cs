using FitnessClub.Domain;

namespace FitnessClub.Contracts.Dto;

/// <summary>
/// DTO клиента фитнес-клуба
/// </summary>
public class ClientDto
{
    public int Id { get; set; }
    public required string PassportNumber { get; set; }
    public required string FullName { get; set; }
    public Gender Gender { get; set; }
    public DateOnly BirthDate { get; set; }
    public required string Phone { get; set; }
    public DateOnly MembershipStart { get; set; }
    public DateOnly MembershipEnd { get; set; }
}