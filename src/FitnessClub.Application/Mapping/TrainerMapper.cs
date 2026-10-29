using FitnessClub.Contracts.Dto;
using FitnessClub.Domain;

namespace FitnessClub.Application.Mapping;

/// <summary>
/// Маппинг между сущностью Trainer и её DTO
/// </summary>
public static class TrainerMapper
{
    public static TrainerDto ToDto(Trainer entity) => new()
    {
        Id = entity.Id,
        PassportNumber = entity.PassportNumber,
        FullName = entity.FullName,
        Gender = entity.Gender,
        BirthDate = entity.BirthDate,
        SpecializationId = entity.Specialization.Id,
        SpecializationName = entity.Specialization.Name,
        ExperienceYears = entity.ExperienceYears
    };

    public static Trainer ToEntity(TrainerEditDto dto, Specialization specialization) => new()
    {
        PassportNumber = dto.PassportNumber,
        FullName = dto.FullName,
        Gender = dto.Gender,
        BirthDate = dto.BirthDate,
        Specialization = specialization,
        ExperienceYears = dto.ExperienceYears
    };

    public static void ApplyTo(Trainer entity, TrainerEditDto dto, Specialization specialization)
    {
        entity.PassportNumber = dto.PassportNumber;
        entity.FullName = dto.FullName;
        entity.Gender = dto.Gender;
        entity.BirthDate = dto.BirthDate;
        entity.Specialization = specialization;
        entity.ExperienceYears = dto.ExperienceYears;
    }
}