namespace FitnessClub.Domain;

/// <summary>
/// Клиент фитнес-клуба
/// </summary>
public class Client : Person
{
    /// <summary>
    /// Контактный номер телефона
    /// </summary>
    public required string Phone { get; set; }

    /// <summary>
    /// Дата начала абонемента 
    /// </summary>
    public required DateOnly MembershipStart { get; set; }

    /// <summary>
    /// Дата окончания абонемента 
    /// </summary>
    public required DateOnly MembershipEnd { get; set; }
}