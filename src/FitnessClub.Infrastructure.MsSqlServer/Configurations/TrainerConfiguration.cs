using FitnessClub.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessClub.Infrastructure.MsSqlServer.Configurations;

/// <summary>
/// Настройка таблицы тренеров
/// </summary>
public class TrainerConfiguration : IEntityTypeConfiguration<Trainer>
{
    /// <summary>
    /// Описывает таблицу, связь со специализацией и начальные данные
    /// </summary>
    /// <param name="builder">Построитель настроек сущности</param>
    public void Configure(EntityTypeBuilder<Trainer> builder)
    {
        builder.ToTable("Trainers");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.PassportNumber).IsRequired().HasMaxLength(20);
        builder.HasIndex(t => t.PassportNumber).IsUnique();
        builder.Property(t => t.FullName).IsRequired().HasMaxLength(200);
        builder.Property(t => t.Gender).HasConversion<string>().HasMaxLength(10);

        builder.HasOne(t => t.Specialization)
            .WithMany()
            .HasForeignKey("SpecializationId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(DataSeeder.GetTrainers(DataSeeder.GetSpecializations()).Select(t => new
        {
            t.Id,
            t.PassportNumber,
            t.FullName,
            t.Gender,
            t.BirthDate,
            t.ExperienceYears,
            SpecializationId = t.Specialization.Id
        }));
    }
}
