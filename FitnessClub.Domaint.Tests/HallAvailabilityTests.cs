namespace FitnessClub.Domain.Tests;

/// <summary>
/// Доступность зала для записи в данный момент
/// </summary>
public class HallAvailabilityTests(TestData data) : IClassFixture<TestData>
{
    /// <summary>
    /// Зал занят, если в нём идёт занятие. Проверка границ
    /// </summary>
    [Theory]
    [InlineData(Hall.Swimming, false)] // занятие 11:00–12:00
    [InlineData(Hall.Strength, false)] // занятие началось ровно в 11:30
    [InlineData(Hall.Dance, true)]     // занятие закончилось ровно в 11:30
    [InlineData(Hall.Cardio, true)]    // занятие только в 15:00
    [InlineData(Hall.Yoga, true)]      // занятие было вчера
    [InlineData(Hall.Boxing, true)]    // занятий сегодня нет
    public void Hall_AtCurrentMoment_IsAvailableOnlyWithoutOngoingSession(Hall hall, bool expected)
    {
        var isAvailable = !data.Enrollments.Any(e => e.Hall == hall && e.IsOngoingAt(data.Now));

        Assert.Equal(expected, isAvailable);
    }
}