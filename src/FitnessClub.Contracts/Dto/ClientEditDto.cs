using System.ComponentModel.DataAnnotations;
using FitnessClub.Shared.Enums;

namespace FitnessClub.Contracts.Dto;

/// <summary>
/// DTO для создания или изменения клиента
/// </summary>
public class ClientEditDto
{
    /// <summary>
    /// Номер паспорта (не длиннее 20 символов)
    /// </summary>
    [Required(ErrorMessage = "Номер паспорта обязателен")]
    [MaxLength(20, ErrorMessage = "Номер паспорта не может быть длиннее 20 символов")]
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Фамилия, имя и отчество (не длиннее 200 символов)
    /// </summary>
    [Required(ErrorMessage = "ФИО обязательно")]
    [MaxLength(200, ErrorMessage = "ФИО не может быть длиннее 200 символов")]
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
    [Required(ErrorMessage = "Телефон обязателен")]
    [Phone(ErrorMessage = "Некорректный формат телефона")]
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
