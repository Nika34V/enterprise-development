using FitnessClub.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessClub.Infrastructure.MsSqlServer.Configurations;

/// <summary>
/// Настройка таблицы записей на занятия
/// </summary>
public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
{
    private static readonly DateTime SeedMoment = new(2026, 11, 13, 12, 0, 0);

    /// <summary>
    /// Описывает таблицу, связи с клиентом и тренером и начальные данные
    /// </summary>
    /// <param name="builder">Построитель настроек сущности</param>
    public void Configure(EntityTypeBuilder<Enrollment> builder)
    {
        builder.ToTable("Enrollments");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Hall).HasConversion<string>().HasMaxLength(20);

        builder.HasOne(e => e.Client)
            .WithMany()
            .HasForeignKey("ClientId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Trainer)
            .WithMany()
            .HasForeignKey("TrainerId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        var trainers = DataSeeder.GetTrainers(DataSeeder.GetSpecializations());
        var clients = DataSeeder.GetClients();

        builder.HasData(DataSeeder.GetEnrollments(clients, trainers, SeedMoment).Select(e => new
        {
            e.Id,
            ClientId = e.Client.Id,
            TrainerId = e.Trainer.Id,
            e.Hall,
            e.StartsAt,
            e.EndsAt,
            e.IsTrial
        }));
    }
}
