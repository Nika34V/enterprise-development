using FitnessClub.Contracts.Dto;
using FitnessClub.Domain;

namespace FitnessClub.Application.Mapping;

/// <summary>
/// Маппинг между сущностью Enrollment и её DTO
/// </summary>
public static class EnrollmentMapper
{
    public static EnrollmentDto ToDto(Enrollment entity) => new()
    {
        Id = entity.Id,
        ClientId = entity.Client.Id,
        ClientName = entity.Client.FullName,
        TrainerId = entity.Trainer.Id,
        TrainerName = entity.Trainer.FullName,
        Hall = entity.Hall,
        StartsAt = entity.StartsAt,
        EndsAt = entity.EndsAt,
        IsTrial = entity.IsTrial
    };

    public static Enrollment ToEntity(EnrollmentEditDto dto, Client client, Trainer trainer) => new()
    {
        Client = client,
        Trainer = trainer,
        Hall = dto.Hall,
        StartsAt = dto.StartsAt,
        EndsAt = dto.EndsAt,
        IsTrial = dto.IsTrial
    };

    public static void ApplyTo(Enrollment entity, EnrollmentEditDto dto, Client client, Trainer trainer)
    {
        entity.Client = client;
        entity.Trainer = trainer;
        entity.Hall = dto.Hall;
        entity.StartsAt = dto.StartsAt;
        entity.EndsAt = dto.EndsAt;
        entity.IsTrial = dto.IsTrial;
    }
}