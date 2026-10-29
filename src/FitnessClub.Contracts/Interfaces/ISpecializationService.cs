using FitnessClub.Contracts.Dto;

namespace FitnessClub.Contracts.Interfaces;

/// <summary>
/// Сервис управления справочником специализаций
/// </summary>
public interface ISpecializationService : IEntityService<SpecializationDto, SpecializationEditDto>;