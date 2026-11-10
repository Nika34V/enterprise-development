using FitnessClub.Shared.Enums;

namespace FitnessClub.Contracts.Dto;

/// <summary>
/// DTO записи клиента на занятие
/// </summary>
public class EnrollmentDto
{
    /// <summary>
    /// Уникальный идентификатор записи
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public int ClientId { get; set; }

    /// <summary>
    /// ФИО клиента
    /// </summary>
    public string? ClientName { get; set; }

    /// <summary>
    /// Идентификатор тренера
    /// </summary>
    public int TrainerId { get; set; }

    /// <summary>
    /// ФИО тренера
    /// </summary>
    public string? TrainerName { get; set; }

    /// <summary>
    /// Зал, в котором проходит занятие
    /// </summary>
    public Hall Hall { get; set; }

    /// <summary>
    /// Дата и время начала занятия
    /// </summary>
    public DateTime StartsAt { get; set; }

    /// <summary>
    /// Дата и время окончания занятия
    /// </summary>
    public DateTime EndsAt { get; set; }

    /// <summary>
    /// Является ли посещение пробным
    /// </summary>
    public bool IsTrial { get; set; }
}
