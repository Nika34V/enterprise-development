namespace FitnessClub.Domain.Tests;

/// <summary>
/// Клиенты с просроченным абонементом
/// </summary>
public class ExpiredMembershipTests(TestData data) : IClassFixture<TestData>
{
    /// <summary>
    /// Результат упорядочен по ФИО
    /// </summary>
    [Fact]
    public void Clients_WithExpiredMembership_AreOrderedByFullName()
    {
        string[] expected =
        [
            "Баранова Вероника Алексеевна",
            "Носков Тимофей Аркадьевич",
            "Чернова Ангелина Денисовна",
            "Юдина Олеся Романовна"
        ];

        var actual = data.Clients
            .Where(c => c.MembershipEnd < data.Today)
            .OrderBy(c => c.FullName)
            .Select(c => c.FullName)
            .ToList();

        Assert.Equal(expected, actual);
    }
}