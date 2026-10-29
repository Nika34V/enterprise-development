namespace FitnessClub.Domain;

/// <summary>
/// Начальные данные фитнес-клуба
/// </summary>
public static class DataSeeder
{
    /// <summary>
    /// Справочник специализаций тренеров
    /// </summary>
    public static List<Specialization> GetSpecializations() =>
    [
        new() { Id = 1, Name = "Йога" },
        new() { Id = 2, Name = "Кроссфит" },
        new() { Id = 3, Name = "Бокс" },
        new() { Id = 4, Name = "Пилатес" },
        new() { Id = 5, Name = "Плавание" },
        new() { Id = 6, Name = "Силовые тренировки" },
        new() { Id = 7, Name = "Стретчинг" },
        new() { Id = 8, Name = "Танцы" },
        new() { Id = 9, Name = "Функциональный тренинг" },
        new() { Id = 10, Name = "Кардио" }
    ];

    /// <summary>
    /// Тренеры
    /// </summary>
    public static List<Trainer> GetTrainers(List<Specialization> specializations) =>
    [
            new Trainer
            {
                Id = 1,
                PassportNumber = "4008 731254",
                FullName = "Лебедев Артур Ильдарович",
                Gender = Gender.Male,
                BirthDate = new DateOnly(1990, 2, 11),
                Specialization = specializations.Single(s => s.Id == 3),
                ExperienceYears = 8
            },
            new Trainer
            {
                Id = 2,
                PassportNumber = "4109 468120",
                FullName = "Ковалёва Ирина Борисовна",
                Gender = Gender.Female,
                BirthDate = new DateOnly(1985, 6, 30),
                Specialization = specializations.Single(s => s.Id == 1),
                ExperienceYears = 12
            },
            new Trainer
            {
                Id = 3,
                PassportNumber = "4210 095837",
                FullName = "Захаров Глеб Матвеевич",
                Gender = Gender.Male,
                BirthDate = new DateOnly(1997, 10, 4),
                Specialization = specializations.Single(s => s.Id == 2),
                ExperienceYears = 2
            },
            new Trainer
            {
                Id = 4,
                PassportNumber = "4011 582649",
                FullName = "Мельникова Вера Аркадьевна",
                Gender = Gender.Female,
                BirthDate = new DateOnly(1993, 1, 19),
                Specialization = specializations.Single(s => s.Id == 4),
                ExperienceYears = 5
            },
            new Trainer
            {
                Id = 5,
                PassportNumber = "4112 219306",
                FullName = "Ткачук Денис Русланович",
                Gender = Gender.Male,
                BirthDate = new DateOnly(1988, 8, 8),
                Specialization = specializations.Single(s => s.Id == 5),
                ExperienceYears = 4
            },
            new Trainer
            {
                Id = 6,
                PassportNumber = "4213 874591",
                FullName = "Аксёнова Людмила Петровна",
                Gender = Gender.Female,
                BirthDate = new DateOnly(1979, 12, 25),
                Specialization = specializations.Single(s => s.Id == 7),
                ExperienceYears = 15
            },
            new Trainer
            {
                Id = 7,
                PassportNumber = "4014 306728",
                FullName = "Бондарев Кирилл Захарович",
                Gender = Gender.Male,
                BirthDate = new DateOnly(2000, 3, 27),
                Specialization = specializations.Single(s => s.Id == 6),
                ExperienceYears = 1
            },
            new Trainer
            {
                Id = 8,
                PassportNumber = "4115 640913",
                FullName = "Шевцова Диана Артёмовна",
                Gender = Gender.Female,
                BirthDate = new DateOnly(1996, 7, 14),
                Specialization = specializations.Single(s => s.Id == 8),
                ExperienceYears = 6
            },
            new Trainer
            {
                Id = 9,
                PassportNumber = "4216 175084",
                FullName = "Рябов Станислав Львович",
                Gender = Gender.Male,
                BirthDate = new DateOnly(1992, 5, 2),
                Specialization = specializations.Single(s => s.Id == 9),
                ExperienceYears = 3
            },
            new Trainer
            {
                Id = 10,
                PassportNumber = "4017 923467",
                FullName = "Гришин Олег Тимурович",
                Gender = Gender.Male,
                BirthDate = new DateOnly(1981, 11, 16),
                Specialization = specializations.Single(s => s.Id == 10),
                ExperienceYears = 10
            }
        ];

    /// <summary>
    /// Клиенты
    /// </summary>
    public static List<Client> GetClients() =>
    [
        new Client
        {
            Id = 1,
            PassportNumber = "4512 208341",
            FullName = "Носков Тимофей Аркадьевич",
            Gender = Gender.Male,
            BirthDate = new DateOnly(1994, 4, 9),
            Phone = "+7 (912) 480-31-76",
            MembershipStart = new DateOnly(2025, 11, 1),
            MembershipEnd = new DateOnly(2026, 4, 25)
        },
        new Client
        {
            Id = 2,
            PassportNumber = "4613 519027",
            FullName = "Юдина Олеся Романовна",
            Gender = Gender.Female,
            BirthDate = new DateOnly(1999, 9, 21),
            Phone = "+7 (921) 733-08-14",
            MembershipStart = new DateOnly(2026, 2, 9),
            MembershipEnd = new DateOnly(2026, 5, 19)
        },
        new Client
        {
            Id = 3,
            PassportNumber = "4711 376480",
            FullName = "Баранова Вероника Алексеевна",
            Gender = Gender.Female,
            BirthDate = new DateOnly(1986, 1, 13),
            Phone = "+7 (905) 117-59-42",
            MembershipStart = new DateOnly(2025, 5, 20),
            MembershipEnd = new DateOnly(2026, 3, 21)
        },
        new Client
        {
            Id = 4,
            PassportNumber = "4514 902615",
            FullName = "Чернова Ангелина Денисовна",
            Gender = Gender.Female,
            BirthDate = new DateOnly(2001, 5, 30),
            Phone = "+7 (963) 254-90-07",
            MembershipStart = new DateOnly(2026, 3, 11),
            MembershipEnd = new DateOnly(2026, 5, 10)
        },
        new Client
        {
            Id = 5,
            PassportNumber = "4615 148263",
            FullName = "Дорохов Илья Владимирович",
            Gender = Gender.Male,
            BirthDate = new DateOnly(1991, 11, 2),
            Phone = "+7 (927) 601-44-25",
            MembershipStart = new DateOnly(2026, 4, 30),
            MembershipEnd = new DateOnly(2026, 7, 29)
        },
        new Client
        {
            Id = 6,
            PassportNumber = "4716 753098",
            FullName = "Соколова Александра Олеговна",
            Gender = Gender.Female,
            BirthDate = new DateOnly(1996, 8, 17),
            Phone = "+7 (937) 328-15-63",
            MembershipStart = new DateOnly(2026, 5, 15),
            MembershipEnd = new DateOnly(2026, 8, 13)
        },
        new Client
        {
            Id = 7,
            PassportNumber = "4517 620419",
            FullName = "Пономарёв Юрий Евгеньевич",
            Gender = Gender.Male,
            BirthDate = new DateOnly(1975, 3, 6),
            Phone = "+7 (903) 945-72-18",
            MembershipStart = new DateOnly(2025, 12, 21),
            MembershipEnd = new DateOnly(2026, 5, 20)
        },
        new Client
        {
            Id = 8,
            PassportNumber = "4618 384756",
            FullName = "Исаков Марк Олегович",
            Gender = Gender.Male,
            BirthDate = new DateOnly(2004, 12, 24),
            Phone = "+7 (952) 076-83-59",
            MembershipStart = new DateOnly(2026, 5, 20),
            MembershipEnd = new DateOnly(2026, 6, 19)
        },
        new Client
        {
            Id = 9,
            PassportNumber = "4719 097532",
            FullName = "Чичкова Татьяна Витальевна",
            Gender = Gender.Female,
            BirthDate = new DateOnly(1989, 7, 1),
            Phone = "+7 (908) 592-27-30",
            MembershipStart = new DateOnly(2026, 4, 10),
            MembershipEnd = new DateOnly(2026, 10, 7)
        },
        new Client
        {
            Id = 10,
            PassportNumber = "4520 856201",
            FullName = "Гаврилова Ева Даниловна",
            Gender = Gender.Female,
            BirthDate = new DateOnly(1998, 2, 8),
            Phone = "+7 (919) 864-05-91",
            MembershipStart = new DateOnly(2026, 5, 27),
            MembershipEnd = new DateOnly(2026, 8, 25)
        }
    ];

    /// <summary>
    /// Записи на занятия
    /// </summary>
    public static List<Enrollment> GetEnrollments(List<Client> clients, List<Trainer> trainers, DateTime now)
    {
        var thisMonth = new DateTime(now.Year, now.Month, 1);
        var lastMonth = thisMonth.AddMonths(-1);
        var nextMonth = thisMonth.AddMonths(1);

        DateTime At(DateTime month, int day, int hour) => new(month.Year, month.Month, day, hour, 0, 0);

        Enrollment Make(int id, int trainerId, int clientId, Hall hall, DateTime start,
            int minutes = 60, bool isTrial = false) => new()
            {
                Id = id,
                Trainer = trainers.Single(t => t.Id == trainerId),
                Client = clients.Single(c => c.Id == clientId),
                Hall = hall,
                StartsAt = start,
                EndsAt = start.AddMinutes(minutes),
                IsTrial = isTrial
            };

        return
        [
            Make(1,  1, 5, Hall.Boxing, At(thisMonth, 4, 9)),
            Make(2,  1, 6, Hall.Boxing, At(thisMonth, 12, 18)),
            Make(3,  2, 9, Hall.Boxing, At(thisMonth, 28, 19)),
            Make(4,  1, 7, Hall.Boxing, At(lastMonth, DateTime.DaysInMonth(lastMonth.Year, lastMonth.Month), 22)),
            Make(5,  8, 5, Hall.Boxing, At(nextMonth, 1, 8)),
            Make(6,  1, 9, Hall.Swimming, now.Date.AddHours(11)),
            Make(7,  2, 6, Hall.Dance, now.Date.AddHours(10), minutes: 90),
            Make(8,  8, 8, Hall.Strength, now),
            Make(9,  4, 7, Hall.Cardio, now.Date.AddHours(15)),
            Make(10, 1, 5, Hall.Yoga, now.AddDays(-1)),
            Make(11, 2, 9, Hall.Yoga, At(thisMonth, 7, 10), isTrial: true),
            Make(12, 2, 7, Hall.Strength, At(thisMonth, 14, 8)),
            Make(13, 8, 6, Hall.Dance, At(thisMonth, 21, 17)),
            Make(14, 1, 8, Hall.Swimming, At(lastMonth, 15, 7)),
            Make(15, 4, 5, Hall.Cardio, At(lastMonth, 22, 20)),
            Make(16, 2, 9, Hall.Swimming, At(nextMonth, 10, 12), isTrial: true),
            Make(17, 10, 6, Hall.Cardio, At(thisMonth, 3, 10)),
            Make(18, 8, 7, Hall.Yoga, At(thisMonth, 26, 13)),
            Make(19, 4, 8, Hall.Dance, At(lastMonth, 9, 16)),
            Make(20, 10, 9, Hall.Strength, At(nextMonth, 3, 18)),
            Make(21, 5, 5, Hall.Swimming, At(thisMonth, 8, 7)),
            Make(22, 3, 6, Hall.Dance, At(lastMonth, 28, 12), isTrial: true),
            Make(23, 6, 9, Hall.Yoga, At(thisMonth, 15, 9))
        ];
    }
}