using System.ComponentModel.DataAnnotations;
using FitnessClub.Shared.Enums;

namespace FitnessClub.Contracts.Dto;

/// <summary>
/// DTO для создания или изменения записи на занятие
/// </summary>
public class EnrollmentEditDto
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    [Required(ErrorMessage = "Идентификатор клиента обязателен")]
    public required int ClientId { get; set; }

    /// <summary>
    /// Идентификатор тренера
    /// </summary>
    [Required(ErrorMessage = "Идентификатор тренера обязателен")]
    public required int TrainerId { get; set; }

    /// <summary>
    /// Зал, в котором проходит занятие
    /// </summary>
    public Hall Hall { get; set; }

    /// <summary>
    /// Дата и время начала занятия
    /// </summary>
    [Required(ErrorMessage = "Дата и время начала обязательны")]
    public required DateTime StartsAt { get; set; }

    /// <summary>
    /// Дата и время окончания занятия
    /// </summary>
    [Required(ErrorMessage = "Дата и время окончания обязательны")]
    public required DateTime EndsAt { get; set; }

    /// <summary>
    /// Является ли посещение пробным
    /// </summary>
    public bool IsTrial { get; set; }
}
