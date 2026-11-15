using FitnessClub.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessClub.Infrastructure.MsSqlServer.Configurations;

/// <summary>
/// Настройка таблицы клиентов
/// </summary>
public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    /// <summary>
    /// Описывает таблицу и начальные данные
    /// </summary>
    /// <param name="builder">Построитель настроек сущности</param>
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("Clients");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.PassportNumber).IsRequired().HasMaxLength(20);
        builder.HasIndex(c => c.PassportNumber).IsUnique();
        builder.Property(c => c.FullName).IsRequired().HasMaxLength(200);
        builder.Property(c => c.Gender).HasConversion<string>().HasMaxLength(10);
        builder.Property(c => c.Phone).IsRequired().HasMaxLength(30);

        builder.HasData(DataSeeder.GetClients());
    }
}
