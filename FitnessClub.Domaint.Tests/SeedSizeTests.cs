namespace FitnessClub.Domain.Tests;

/// <summary>
/// Проверка объёма DataSeeder
/// </summary>
public class SeedSizeTests(TestData data) : IClassFixture<TestData>
{
    /// <summary>
    /// В DataSeeder не менее 10 экземпляров каждого класса
    /// </summary>
    [Fact]
    public void Seed_EveryEntity_HasAtLeastTenInstances()
    {
        Assert.True(data.Specializations.Count >= 10);
        Assert.True(data.Trainers.Count >= 10);
        Assert.True(data.Clients.Count >= 10);
        Assert.True(data.Enrollments.Count >= 10);
    }
}