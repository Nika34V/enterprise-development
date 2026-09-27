namespace FitnessClub.Domain;

/// <summary>
/// Запись клиента на персональное занятие к тренеру 
/// </summary>
public class Enrollment
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Клиент, записавшийся на занятие
    /// </summary>
    public required Client Client { get; set; }

    /// <summary>
    /// Тренер, проводящий занятие
    /// </summary>
    public required Trainer Trainer { get; set; }

    /// <summary>
    /// Зал, в котором проходит занятие
    /// </summary>
    public required Hall Hall { get; set; }

    /// <summary>
    /// Дата и время начала занятия
    /// </summary>
    public required DateTime StartsAt { get; set; }

    /// <summary>
    /// Дата и время окончания занятия
    /// </summary>
    public required DateTime EndsAt { get; set; }

    /// <summary>
    /// Является ли посещение пробным
    /// </summary>
    public required bool IsTrial { get; set; }

    /// <summary>
    /// Проверяет, идёт ли занятие в указанный момент
    /// </summary>
    public bool IsOngoingAt(DateTime moment) =>
        StartsAt <= moment && moment < EndsAt;
}