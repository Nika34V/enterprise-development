using FitnessClub.Shared.Enums;

namespace FitnessClub.Contracts.Dto;

/// <summary>
/// DTO клиента фитнес-клуба
/// </summary>
public class ClientDto
{
    /// <summary>
    /// Уникальный идентификатор клиента
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Номер паспорта
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Фамилия, имя и отчество
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Пол
    /// </summary>
    public Gender Gender { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateOnly BirthDate { get; set; }

    /// <summary>
    /// Контактный номер телефона
    /// </summary>
    public required string Phone { get; set; }

    /// <summary>
    /// Дата начала абонемента
    /// </summary>
    public DateOnly MembershipStart { get; set; }

    /// <summary>
    /// Дата окончания абонемента
    /// </summary>
    public DateOnly MembershipEnd { get; set; }
}
