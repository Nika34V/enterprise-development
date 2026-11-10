namespace FitnessClub.Contracts.Dto;

/// <summary>
/// DTO тренера с количеством записей на занятия (для рейтинга популярности)
/// </summary>
public class TopTrainerDto
{
    /// <summary>
    /// Тренер
    /// </summary>
    public required TrainerDto Trainer { get; set; }

    /// <summary>
    /// Количество записей на занятия у этого тренера
    /// </summary>
    public int EnrollmentsCount { get; set; }
}
