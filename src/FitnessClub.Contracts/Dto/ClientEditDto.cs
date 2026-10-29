using System.ComponentModel.DataAnnotations;
using FitnessClub.Domain;

namespace FitnessClub.Contracts.Dto;

/// <summary>
/// DTO для создания или изменения клиента
/// </summary>
public class ClientEditDto
{
    [Required(ErrorMessage = "Номер паспорта обязателен")]
    [MaxLength(20, ErrorMessage = "Номер паспорта не может быть длиннее 20 символов")]
    public required string PassportNumber { get; set; }

    [Required(ErrorMessage = "ФИО обязательно")]
    [MaxLength(200, ErrorMessage = "ФИО не может быть длиннее 200 символов")]
    public required string FullName { get; set; }

    public Gender Gender { get; set; }

    public DateOnly BirthDate { get; set; }

    [Required(ErrorMessage = "Телефон обязателен")]
    [Phone(ErrorMessage = "Некорректный формат телефона")]
    public required string Phone { get; set; }

    public DateOnly MembershipStart { get; set; }

    public DateOnly MembershipEnd { get; set; }
}