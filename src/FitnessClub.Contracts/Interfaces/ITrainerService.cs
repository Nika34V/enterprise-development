using FitnessClub.Contracts.Dto;

namespace FitnessClub.Contracts.Interfaces;

/// <summary>
/// Сервис управления тренерами.
/// CreateAsync/UpdateAsync могут выбросить <see cref="ArgumentException"/>, если специализация не найдена
/// </summary>
public interface ITrainerService : IEntityService<TrainerDto, TrainerEditDto>;