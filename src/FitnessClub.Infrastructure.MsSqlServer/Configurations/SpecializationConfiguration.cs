using FitnessClub.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessClub.Infrastructure.MsSqlServer.Configurations;

/// <summary>
/// Настройка таблицы специализаций
/// </summary>
public class SpecializationConfiguration : IEntityTypeConfiguration<Specialization>
{
    /// <summary>
    /// Описывает таблицу и начальные данные
    /// </summary>
    /// <param name="builder">Построитель настроек сущности</param>
    public void Configure(EntityTypeBuilder<Specialization> builder)
    {
        builder.ToTable("Specializations");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Name).IsRequired().HasMaxLength(100);
        builder.HasIndex(s => s.Name).IsUnique();

        builder.HasData(DataSeeder.GetSpecializations());
    }
}
