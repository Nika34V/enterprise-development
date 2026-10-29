namespace FitnessClub.Domain.Tests;

/// <summary>
/// LINQ-запросы предметной области фитнес-клуба
/// </summary>
public class FitnessClubTests(FitnessClubFixture fixture) : IClassFixture<FitnessClubFixture>
{
    /// <summary>
    /// Запрос 1: тренеры со стажем не менее 5 лет.
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
            "Мельникова Вера Аркадьевна", // ровно 5 лет
            "Шевцова Диана Артёмовна"
        ];

        var actual = fixture.Trainers
            .Where(t => t.ExperienceYears >= 5)
            .Select(t => t.FullName)
            .Order()
            .ToList();

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Запрос 2: доступность зала для записи в данный момент.
    /// Зал занят, если в нём идёт занятие
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
        var isAvailable = !fixture.Enrollments.Any(e => e.Hall == hall && e.IsOngoingAt(fixture.Now));

        Assert.Equal(expected, isAvailable);
    }

    /// <summary>
    /// Запрос 3: клиенты с просроченным абонементом, по ФИО.
    /// Абонемент, заканчивающийся сегодня, ещё действует
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

        var actual = fixture.Clients
            .Where(c => c.MembershipEnd < fixture.Today)
            .OrderBy(c => c.FullName)
            .Select(c => c.FullName)
            .ToList();

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Запрос 4: занятия выбранного зала за текущий месяц.
    /// Занятия на границах месяца (30 апреля, 1 июня) в выборку не входят
    /// </summary>
    [Fact]
    public void Sessions_InSelectedHall_ThisMonthOnly()
    {
        const Hall selected = Hall.Boxing;
        int[] expectedIds = [1, 2, 3];

        var actualIds = fixture.Enrollments
            .Where(e => e.Hall == selected
                        && e.StartsAt.Year == fixture.Now.Year
                        && e.StartsAt.Month == fixture.Now.Month)
            .Select(e => e.Id)
            .Order()
            .ToList();

        Assert.Equal(expectedIds, actualIds);
    }

    /// <summary>
    /// Запрос 5: пять самых популярных тренеров.
    /// Число занятий: Лебедев 6, Ковалёва 5, Шевцова 4, Мельникова 3, Гришин 2
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

        var actual = fixture.Enrollments
            .GroupBy(e => e.Trainer.Id)
            .Select(g => new { TrainerId = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .ThenBy(x => x.TrainerId)
            .Take(5)
            .Select(x => fixture.Trainers.Single(t => t.Id == x.TrainerId).FullName)
            .ToList();

        Assert.Equal(expected, actual);
    }
}