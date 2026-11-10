using FitnessClub.Contracts.Dto;

namespace FitnessClub.Contracts.Interfaces;

/// <summary>
/// Сервис управления тренерами
/// </summary>
public interface ITrainerService : IEntityService<TrainerDto, TrainerEditDto>;
