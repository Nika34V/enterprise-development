using FitnessClub.Contracts.Dto;
using FitnessClub.Domain;

namespace FitnessClub.Application.Mapping;

/// <summary>
/// Маппинг между сущностью Specialization и её DTO
/// </summary>
public static class SpecializationMapper
{
    public static SpecializationDto ToDto(Specialization entity) => new()
    {
        Id = entity.Id,
        Name = entity.Name
    };

    public static Specialization ToEntity(SpecializationEditDto dto) => new()
    {
        Name = dto.Name
    };

    public static void ApplyTo(Specialization entity, SpecializationEditDto dto)
    {
        entity.Name = dto.Name;
    }
}