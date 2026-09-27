namespace FitnessClub.Domain.Tests;

/// <summary>
/// Тренеры со стажем не менее 5 лет
/// </summary>
public class ExperiencedTrainersTests(TestData data) : IClassFixture<TestData>
{
    /// <summary>
    /// Граница включена: тренер ровно с пятью годами должен попасть в выборку
    /// </summary>
    [Fact]
    public void Trainers_WithFiveOrMoreYears_AreSelected()
    {
        string[] expected =
        [
            "Аксёнова Людмила Петровна",
            "Гришин Олег Тимурович",
            "Ковалёва Ирина Борисовна",
            "Лебедев Артур Ильдарович",
            "Мельникова Вера Аркадьевна", 
            "Шевцова Диана Артёмовна"
        ];

        var actual = data.Trainers
            .Where(t => t.ExperienceYears >= 5)
            .Select(t => t.FullName)
            .Order()
            .ToList();

        Assert.Equal(expected, actual);
    }
}