using FitnessClub.Contracts.Dto;

namespace FitnessClub.Contracts.Interfaces;

/// <summary>
/// Сервис управления записями на занятия
/// </summary>
public interface IEnrollmentService : IEntityService<EnrollmentDto, EnrollmentEditDto>;
