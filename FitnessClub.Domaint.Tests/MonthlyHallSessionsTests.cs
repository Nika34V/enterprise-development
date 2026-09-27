namespace FitnessClub.Domain.Tests;

/// <summary>
/// Занятия выбранного зала за текущий месяц
/// </summary>
public class MonthlyHallSessionsTests(TestData data) : IClassFixture<TestData>
{
    /// <summary>
    /// Занятие в последний день прошлого месяца (22:00) и занятие первого числа следующего месяца (08:00) в выборку не входят
    /// </summary>
    [Fact]
    public void Sessions_InSelectedHall_ThisMonthOnly()
    {
        const Hall selected = Hall.Boxing;
        int[] expectedIds = [1, 2, 3];

        var actualIds = data.Enrollments
            .Where(e => e.Hall == selected
                        && e.StartsAt.Year == data.Now.Year
                        && e.StartsAt.Month == data.Now.Month)
            .Select(e => e.Id)
            .Order()
            .ToList();

        Assert.Equal(expectedIds, actualIds);
    }
}