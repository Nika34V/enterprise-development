namespace FitnessClub.Domain;

/// <summary>
/// Базовый класс клиента и тренера содержит общие свойства 
/// </summary>
public abstract class Person
{
    /// <summary>
    /// Уникальный идентификатор в рамках своей коллекции
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Номер паспорта
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Фамилия, имя и отчество одной строкой. Сортировка в запросах
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Пол человека
    /// </summary>
    public required Gender Gender { get; set; }

    /// <summary>
    /// Дата рождения 
    /// </summary>
    public required DateOnly BirthDate { get; set; }
}