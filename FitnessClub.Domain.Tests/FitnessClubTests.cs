namespace FitnessClub.Domain.Tests;

/// <summary>
/// LINQ-запросы предметной области фитнес-клуба
/// </summary>
public class FitnessClubTests
{
    private static readonly DateTime Now = new(2026, 5, 20, 11, 30, 0);
    private static readonly DateOnly Today = DateOnly.FromDateTime(Now);

    private readonly List<Specialization> _specializations;
    private readonly List<Trainer> _trainers;
    private readonly List<Client> _clients;
    private readonly List<Enrollment> _enrollments;

    public FitnessClubTests()
    {
        _specializations = DataSeeder.GetSpecializations();
        _trainers = DataSeeder.GetTrainers(_specializations);
        _clients = DataSeeder.GetClients();
        _enrollments = DataSeeder.GetEnrollments(_clients, _trainers, Now);
    }

    /// <summary>
    /// Тренеры со стажем не менее 5 лет
    /// </summary>
    [Fact]
    public void Trainers_WithFiveOrMoreYears_AreSelected()
    {
        var actual = _trainers
            .Where(t => t.ExperienceYears >= 5)
            .Select(t => t.FullName)
            .Order()
            .ToList();

        Assert.All(actual, name => Assert.Contains(_trainers, t => t.FullName == name && t.ExperienceYears >= 5));
        Assert.Equal(_trainers.Count(t => t.ExperienceYears >= 5), actual.Count);
    }

    /// <summary>
    /// Доступность зала для записи в данный момент
    /// </summary>
    [Theory]
    [InlineData(Hall.Swimming, false)]
    [InlineData(Hall.Strength, false)]
    [InlineData(Hall.Dance, true)]
    [InlineData(Hall.Cardio, true)]
    [InlineData(Hall.Yoga, true)]
    [InlineData(Hall.Boxing, true)]
    public void Hall_AtCurrentMoment_IsAvailableOnlyWithoutOngoingSession(Hall hall, bool expected)
    {
        var isAvailable = !_enrollments.Any(e => e.Hall == hall && e.IsOngoingAt(Now));

        Assert.Equal(expected, isAvailable);
    }

    /// <summary>
    /// Клиенты с просроченным абонементом, по ФИО
    /// </summary>
    [Fact]
    public void Clients_WithExpiredMembership_AreOrderedByFullName()
    {
        var expected = _clients
            .Where(c => c.MembershipEnd < Today)
            .Select(c => c.FullName)
            .Order()
            .ToList();

        var actual = _clients
            .Where(c => c.MembershipEnd < Today)
            .OrderBy(c => c.FullName)
            .Select(c => c.FullName)
            .ToList();

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Занятия выбранного зала за текущий месяц
    /// </summary>
    [Fact]
    public void Sessions_InSelectedHall_ThisMonthOnly()
    {
        const Hall selected = Hall.Boxing;

        var actual = _enrollments
            .Where(e => e.Hall == selected
                        && e.StartsAt.Year == Now.Year
                        && e.StartsAt.Month == Now.Month)
            .ToList();

        Assert.All(actual, e => Assert.Equal(selected, e.Hall));
        Assert.All(actual, e => Assert.Equal(Now.Month, e.StartsAt.Month));
    }

    /// <summary>
    /// Пять самых популярных тренеров
    /// </summary>
    [Fact]
    public void Trainers_TopFiveByEnrollmentCount_AreInDescendingOrder()
    {
        var top = _enrollments
            .GroupBy(e => e.Trainer.Id)
            .Select(g => new { TrainerId = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .ThenBy(x => x.TrainerId)
            .Take(5)
            .ToList();

        Assert.Equal(5, top.Count);
        for (var i = 1; i < top.Count; i++)
            Assert.True(top[i - 1].Count >= top[i].Count);
    }
}