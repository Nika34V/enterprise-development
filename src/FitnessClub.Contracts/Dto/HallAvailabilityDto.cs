using FitnessClub.Shared.Enums;

namespace FitnessClub.Contracts.Dto;

/// <summary>
/// DTO доступности зала на заданный момент времени
/// </summary>
public class HallAvailabilityDto
{
    /// <summary>
    /// Проверяемый зал
    /// </summary>
    public Hall Hall { get; set; }

    /// <summary>
    /// Доступен ли зал для записи: <c>true</c>, если в этот момент в нём нет занятия
    /// </summary>
    public bool IsAvailable { get; set; }
}
