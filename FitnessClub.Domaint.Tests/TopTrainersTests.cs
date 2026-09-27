namespace FitnessClub.Domain.Tests;

/// <summary>
/// Пять самых популярных тренеров
/// </summary>
public class TopTrainersTests(TestData data) : IClassFixture<TestData>
{
    /// <summary>
    /// Популярность определяется числом записей. При равном числе порядок задаёт Id тренера
    /// </summary>
    [Fact]
    public void Trainers_TopFiveByEnrollmentCount_AreInDescendingOrder()
    {
        string[] expected =
        [
            "Лебедев Артур Ильдарович",
            "Ковалёва Ирина Борисовна",
            "Шевцова Диана Артёмовна",
            "Мельникова Вера Аркадьевна",
            "Гришин Олег Тимурович"
        ];

        var actual = data.Enrollments
            .GroupBy(e => e.Trainer.Id)
            .Select(g => new { TrainerId = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .ThenBy(x => x.TrainerId)
            .Take(5)
            .Select(x => data.Trainers.Single(t => t.Id == x.TrainerId).FullName)
            .ToList();

        Assert.Equal(expected, actual);
    }
}