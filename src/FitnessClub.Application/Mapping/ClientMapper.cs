using FitnessClub.Contracts.Dto;
using FitnessClub.Domain;

namespace FitnessClub.Application.Mapping;

/// <summary>
/// Маппинг между сущностью Client и её DTO
/// </summary>
public static class ClientMapper
{
    public static ClientDto ToDto(Client entity) => new()
    {
        Id = entity.Id,
        PassportNumber = entity.PassportNumber,
        FullName = entity.FullName,
        Gender = entity.Gender,
        BirthDate = entity.BirthDate,
        Phone = entity.Phone,
        MembershipStart = entity.MembershipStart,
        MembershipEnd = entity.MembershipEnd
    };

    public static Client ToEntity(ClientEditDto dto) => new()
    {
        PassportNumber = dto.PassportNumber,
        FullName = dto.FullName,
        Gender = dto.Gender,
        BirthDate = dto.BirthDate,
        Phone = dto.Phone,
        MembershipStart = dto.MembershipStart,
        MembershipEnd = dto.MembershipEnd
    };

    public static void ApplyTo(Client entity, ClientEditDto dto)
    {
        entity.PassportNumber = dto.PassportNumber;
        entity.FullName = dto.FullName;
        entity.Gender = dto.Gender;
        entity.BirthDate = dto.BirthDate;
        entity.Phone = dto.Phone;
        entity.MembershipStart = dto.MembershipStart;
        entity.MembershipEnd = dto.MembershipEnd;
    }
}