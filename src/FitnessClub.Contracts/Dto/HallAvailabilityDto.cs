using FitnessClub.Domain;

namespace FitnessClub.Contracts.Dto;

/// <summary>
/// DTO доступности зала на заданный момент времени
/// </summary>
public class HallAvailabilityDto
{
    public Hall Hall { get; set; }
    public bool IsAvailable { get; set; }
}