using FitnessClub.Contracts.Dto;

namespace FitnessClub.Contracts.Interfaces;

/// <summary>
/// Сервис управления записями на занятия.
/// CreateAsync/UpdateAsync могут выбросить <see cref="ArgumentException"/>, если клиент или тренер не найден
/// </summary>
public interface IEnrollmentService : IEntityService<EnrollmentDto, EnrollmentEditDto>;