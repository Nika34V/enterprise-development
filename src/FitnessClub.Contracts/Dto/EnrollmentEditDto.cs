using System.ComponentModel.DataAnnotations;
using FitnessClub.Domain;

namespace FitnessClub.Contracts.Dto;

/// <summary>
/// DTO для создания или изменения записи на занятие
/// </summary>
public class EnrollmentEditDto
{
    [Required(ErrorMessage = "Идентификатор клиента обязателен")]
    public required int ClientId { get; set; }

    [Required(ErrorMessage = "Идентификатор тренера обязателен")]
    public required int TrainerId { get; set; }

    public Hall Hall { get; set; }

    [Required(ErrorMessage = "Дата и время начала обязательны")]
    public required DateTime StartsAt { get; set; }

    [Required(ErrorMessage = "Дата и время окончания обязательны")]
    public required DateTime EndsAt { get; set; }

    public bool IsTrial { get; set; }
}