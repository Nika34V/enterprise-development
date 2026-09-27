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
    public static List<Trainer> GetTrainers(List<Specialization> specializations)
    {
        Specialization Spec(int id) => specializations.Single(s => s.Id == id);

        Trainer Make(int id, string passport, string name, Gender gender,
            DateOnly birth, int specId, int experience) => new()
            {
                Id = id,
                PassportNumber = passport,
                FullName = name,
                Gender = gender,
                BirthDate = birth,
                Specialization = Spec(specId),
                ExperienceYears = experience
            };

        return
        [
            Make(1,  "4008 731254", "Лебедев Артур Ильдарович",     Gender.Male,   new(1990, 2, 11),  3, 8),
            Make(2,  "4109 468120", "Ковалёва Ирина Борисовна",     Gender.Female, new(1985, 6, 30),  1, 12),
            Make(3,  "4210 095837", "Захаров Глеб Матвеевич",       Gender.Male,   new(1997, 10, 4),  2, 2),
            Make(4,  "4011 582649", "Мельникова Вера Аркадьевна",   Gender.Female, new(1993, 1, 19),  4, 5),
            Make(5,  "4112 219306", "Ткачук Денис Русланович",      Gender.Male,   new(1988, 8, 8),   5, 4),
            Make(6,  "4213 874591", "Аксёнова Людмила Петровна",    Gender.Female, new(1979, 12, 25), 7, 15),
            Make(7,  "4014 306728", "Бондарев Кирилл Захарович",    Gender.Male,   new(2000, 3, 27),  6, 1),
            Make(8,  "4115 640913", "Шевцова Диана Артёмовна",      Gender.Female, new(1996, 7, 14),  8, 6),
            Make(9,  "4216 175084", "Рябов Станислав Львович",      Gender.Male,   new(1992, 5, 2),   9, 3),
            Make(10, "4017 923467", "Гришин Олег Тимурович",        Gender.Male,   new(1981, 11, 16), 10, 10)
        ];
    }

    /// <summary>
    /// Клиенты. Границы абонементов задаются смещением в днях от переданной даты
    /// </summary>
    public static List<Client> GetClients(DateOnly today)
    {
        Client Make(int id, string passport, string name, Gender gender, DateOnly birth,
            string phone, int startOffsetDays, int endOffsetDays) => new()
            {
                Id = id,
                PassportNumber = passport,
                FullName = name,
                Gender = gender,
                BirthDate = birth,
                Phone = phone,
                MembershipStart = today.AddDays(startOffsetDays),
                MembershipEnd = today.AddDays(endOffsetDays)
            };

        return
        [
            Make(1,  "4512 208341", "Носков Тимофей Аркадьевич",   Gender.Male,   new(1994, 4, 9),   "+7 (912) 480-31-76", -200, -25),
            Make(2,  "4613 519027", "Юдина Олеся Романовна",       Gender.Female, new(1999, 9, 21),  "+7 (921) 733-08-14", -100, -1),
            Make(3,  "4711 376480", "Баранова Вероника Алексеевна",      Gender.Male,   new(1986, 1, 13),  "+7 (905) 117-59-42", -365, -60),
            Make(4,  "4514 902615", "Чернова Ангелина Денисовна",  Gender.Female, new(2001, 5, 30),  "+7 (963) 254-90-07", -70, -10),
            Make(5,  "4615 148263", "Дорохов Илья Владимирович",   Gender.Male,   new(1991, 11, 2),  "+7 (927) 601-44-25", -20, 70),
            Make(6,  "4716 753098", "Соколова Александра Олеговна",     Gender.Female, new(1996, 8, 17),  "+7 (937) 328-15-63", -5, 85),
            Make(7,  "4517 620419", "Пономарёв Юрий Евгеньевич",   Gender.Male,   new(1975, 3, 6),   "+7 (903) 945-72-18", -150, 0),
            Make(8,  "4618 384756", "Исаков Марк Олегович",        Gender.Male,   new(2004, 12, 24), "+7 (952) 076-83-59", 0, 30),
            Make(9,  "4719 097532", "Чичкова Татьяна Витальевна",     Gender.Female, new(1989, 7, 1),   "+7 (908) 592-27-30", -40, 140),
            Make(10, "4520 856201", "Гаврилова Ева Даниловна",     Gender.Female, new(1998, 2, 8),   "+7 (919) 864-05-91", 7, 97)
        ];
    }

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
            Make(7,  2, 6, Hall.Dance,    now.Date.AddHours(10), minutes: 90),         
            Make(8,  8, 8, Hall.Strength, now),                                        
            Make(9,  4, 7, Hall.Cardio,   now.Date.AddHours(15)),                      
            Make(10, 1, 5, Hall.Yoga,     now.AddDays(-1)),                            
            Make(11, 2, 9, Hall.Yoga,     At(thisMonth, 7, 10), isTrial: true),
            Make(12, 2, 7, Hall.Strength, At(thisMonth, 14, 8)),
            Make(13, 8, 6, Hall.Dance,    At(thisMonth, 21, 17)),
            Make(14, 1, 8, Hall.Swimming, At(lastMonth, 15, 7)),
            Make(15, 4, 5, Hall.Cardio,   At(lastMonth, 22, 20)),
            Make(16, 2, 9, Hall.Swimming, At(nextMonth, 10, 12), isTrial: true),
            Make(17, 10, 6, Hall.Cardio,  At(thisMonth, 3, 10)),
            Make(18, 8, 7, Hall.Yoga,     At(thisMonth, 26, 13)),
            Make(19, 4, 8, Hall.Dance,    At(lastMonth, 9, 16)),
            Make(20, 10, 9, Hall.Strength, At(nextMonth, 3, 18)),
            Make(21, 5, 5, Hall.Swimming, At(thisMonth, 8, 7)),
            Make(22, 3, 6, Hall.Dance,    At(lastMonth, 28, 12), isTrial: true),
            Make(23, 6, 9, Hall.Yoga,     At(thisMonth, 15, 9))
        ];
    }
}