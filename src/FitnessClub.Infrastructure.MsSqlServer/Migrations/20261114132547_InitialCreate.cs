using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FitnessClub.Infrastructure.MsSqlServer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Phone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    MembershipStart = table.Column<DateOnly>(type: "date", nullable: false),
                    MembershipEnd = table.Column<DateOnly>(type: "date", nullable: false),
                    PassportNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specializations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specializations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trainers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpecializationId = table.Column<int>(type: "int", nullable: false),
                    ExperienceYears = table.Column<int>(type: "int", nullable: false),
                    PassportNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trainers_Specializations_SpecializationId",
                        column: x => x.SpecializationId,
                        principalTable: "Specializations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    TrainerId = table.Column<int>(type: "int", nullable: false),
                    Hall = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StartsAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndsAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsTrial = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enrollments_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollments_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "BirthDate", "FullName", "Gender", "MembershipEnd", "MembershipStart", "PassportNumber", "Phone" },
                values: new object[,]
                {
                    { 1, new DateOnly(1994, 4, 9), "Носков Тимофей Аркадьевич", "Male", new DateOnly(2026, 4, 25), new DateOnly(2025, 11, 1), "4512 208341", "+7 (912) 480-31-76" },
                    { 2, new DateOnly(1999, 9, 21), "Юдина Олеся Романовна", "Female", new DateOnly(2026, 5, 19), new DateOnly(2026, 2, 9), "4613 519027", "+7 (921) 733-08-14" },
                    { 3, new DateOnly(1986, 1, 13), "Баранова Вероника Алексеевна", "Female", new DateOnly(2026, 3, 21), new DateOnly(2025, 5, 20), "4711 376480", "+7 (905) 117-59-42" },
                    { 4, new DateOnly(2001, 5, 30), "Чернова Ангелина Денисовна", "Female", new DateOnly(2026, 5, 10), new DateOnly(2026, 3, 11), "4514 902615", "+7 (963) 254-90-07" },
                    { 5, new DateOnly(1991, 11, 2), "Дорохов Илья Владимирович", "Male", new DateOnly(2026, 7, 29), new DateOnly(2026, 4, 30), "4615 148263", "+7 (927) 601-44-25" },
                    { 6, new DateOnly(1996, 8, 17), "Соколова Александра Олеговна", "Female", new DateOnly(2026, 8, 13), new DateOnly(2026, 5, 15), "4716 753098", "+7 (937) 328-15-63" },
                    { 7, new DateOnly(1975, 3, 6), "Пономарёв Юрий Евгеньевич", "Male", new DateOnly(2026, 5, 20), new DateOnly(2025, 12, 21), "4517 620419", "+7 (903) 945-72-18" },
                    { 8, new DateOnly(2004, 12, 24), "Исаков Марк Олегович", "Male", new DateOnly(2026, 6, 19), new DateOnly(2026, 5, 20), "4618 384756", "+7 (952) 076-83-59" },
                    { 9, new DateOnly(1989, 7, 1), "Чичкова Татьяна Витальевна", "Female", new DateOnly(2026, 10, 7), new DateOnly(2026, 4, 10), "4719 097532", "+7 (908) 592-27-30" },
                    { 10, new DateOnly(1998, 2, 8), "Гаврилова Ева Даниловна", "Female", new DateOnly(2026, 8, 25), new DateOnly(2026, 5, 27), "4520 856201", "+7 (919) 864-05-91" }
                });

            migrationBuilder.InsertData(
                table: "Specializations",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Йога" },
                    { 2, "Кроссфит" },
                    { 3, "Бокс" },
                    { 4, "Пилатес" },
                    { 5, "Плавание" },
                    { 6, "Силовые тренировки" },
                    { 7, "Стретчинг" },
                    { 8, "Танцы" },
                    { 9, "Функциональный тренинг" },
                    { 10, "Кардио" }
                });

            migrationBuilder.InsertData(
                table: "Trainers",
                columns: new[] { "Id", "BirthDate", "ExperienceYears", "FullName", "Gender", "PassportNumber", "SpecializationId" },
                values: new object[,]
                {
                    { 1, new DateOnly(1990, 2, 11), 8, "Лебедев Артур Ильдарович", "Male", "4008 731254", 3 },
                    { 2, new DateOnly(1985, 6, 30), 12, "Ковалёва Ирина Борисовна", "Female", "4109 468120", 1 },
                    { 3, new DateOnly(1997, 10, 4), 2, "Захаров Глеб Матвеевич", "Male", "4210 095837", 2 },
                    { 4, new DateOnly(1993, 1, 19), 5, "Мельникова Вера Аркадьевна", "Female", "4011 582649", 4 },
                    { 5, new DateOnly(1988, 8, 8), 4, "Ткачук Денис Русланович", "Male", "4112 219306", 5 },
                    { 6, new DateOnly(1979, 12, 25), 15, "Аксёнова Людмила Петровна", "Female", "4213 874591", 7 },
                    { 7, new DateOnly(2000, 3, 27), 1, "Бондарев Кирилл Захарович", "Male", "4014 306728", 6 },
                    { 8, new DateOnly(1996, 7, 14), 6, "Шевцова Диана Артёмовна", "Female", "4115 640913", 8 },
                    { 9, new DateOnly(1992, 5, 2), 3, "Рябов Станислав Львович", "Male", "4216 175084", 9 },
                    { 10, new DateOnly(1981, 11, 16), 10, "Гришин Олег Тимурович", "Male", "4017 923467", 10 }
                });

            migrationBuilder.InsertData(
                table: "Enrollments",
                columns: new[] { "Id", "ClientId", "EndsAt", "Hall", "IsTrial", "StartsAt", "TrainerId" },
                values: new object[,]
                {
                    { 1, 5, new DateTime(2026, 11, 4, 10, 0, 0, 0, DateTimeKind.Unspecified), "Boxing", false, new DateTime(2026, 11, 4, 9, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, 6, new DateTime(2026, 11, 12, 19, 0, 0, 0, DateTimeKind.Unspecified), "Boxing", false, new DateTime(2026, 11, 12, 18, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3, 9, new DateTime(2026, 11, 28, 20, 0, 0, 0, DateTimeKind.Unspecified), "Boxing", false, new DateTime(2026, 11, 28, 19, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 4, 7, new DateTime(2026, 10, 31, 23, 0, 0, 0, DateTimeKind.Unspecified), "Boxing", false, new DateTime(2026, 10, 31, 22, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 5, 5, new DateTime(2026, 12, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), "Boxing", false, new DateTime(2026, 12, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), 8 },
                    { 6, 9, new DateTime(2026, 11, 13, 12, 0, 0, 0, DateTimeKind.Unspecified), "Swimming", false, new DateTime(2026, 11, 13, 11, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 7, 6, new DateTime(2026, 11, 13, 11, 30, 0, 0, DateTimeKind.Unspecified), "Dance", false, new DateTime(2026, 11, 13, 10, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 8, 8, new DateTime(2026, 11, 13, 13, 0, 0, 0, DateTimeKind.Unspecified), "Strength", false, new DateTime(2026, 11, 13, 12, 0, 0, 0, DateTimeKind.Unspecified), 8 },
                    { 9, 7, new DateTime(2026, 11, 13, 16, 0, 0, 0, DateTimeKind.Unspecified), "Cardio", false, new DateTime(2026, 11, 13, 15, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 10, 5, new DateTime(2026, 11, 12, 13, 0, 0, 0, DateTimeKind.Unspecified), "Yoga", false, new DateTime(2026, 11, 12, 12, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 11, 9, new DateTime(2026, 11, 7, 11, 0, 0, 0, DateTimeKind.Unspecified), "Yoga", true, new DateTime(2026, 11, 7, 10, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 12, 7, new DateTime(2026, 11, 14, 9, 0, 0, 0, DateTimeKind.Unspecified), "Strength", false, new DateTime(2026, 11, 14, 8, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 13, 6, new DateTime(2026, 11, 21, 18, 0, 0, 0, DateTimeKind.Unspecified), "Dance", false, new DateTime(2026, 11, 21, 17, 0, 0, 0, DateTimeKind.Unspecified), 8 },
                    { 14, 8, new DateTime(2026, 10, 15, 8, 0, 0, 0, DateTimeKind.Unspecified), "Swimming", false, new DateTime(2026, 10, 15, 7, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 15, 5, new DateTime(2026, 10, 22, 21, 0, 0, 0, DateTimeKind.Unspecified), "Cardio", false, new DateTime(2026, 10, 22, 20, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 16, 9, new DateTime(2026, 12, 10, 13, 0, 0, 0, DateTimeKind.Unspecified), "Swimming", true, new DateTime(2026, 12, 10, 12, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 17, 6, new DateTime(2026, 11, 3, 11, 0, 0, 0, DateTimeKind.Unspecified), "Cardio", false, new DateTime(2026, 11, 3, 10, 0, 0, 0, DateTimeKind.Unspecified), 10 },
                    { 18, 7, new DateTime(2026, 11, 26, 14, 0, 0, 0, DateTimeKind.Unspecified), "Yoga", false, new DateTime(2026, 11, 26, 13, 0, 0, 0, DateTimeKind.Unspecified), 8 },
                    { 19, 8, new DateTime(2026, 10, 9, 17, 0, 0, 0, DateTimeKind.Unspecified), "Dance", false, new DateTime(2026, 10, 9, 16, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 20, 9, new DateTime(2026, 12, 3, 19, 0, 0, 0, DateTimeKind.Unspecified), "Strength", false, new DateTime(2026, 12, 3, 18, 0, 0, 0, DateTimeKind.Unspecified), 10 },
                    { 21, 5, new DateTime(2026, 11, 8, 8, 0, 0, 0, DateTimeKind.Unspecified), "Swimming", false, new DateTime(2026, 11, 8, 7, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 22, 6, new DateTime(2026, 10, 28, 13, 0, 0, 0, DateTimeKind.Unspecified), "Dance", true, new DateTime(2026, 10, 28, 12, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 23, 9, new DateTime(2026, 11, 15, 10, 0, 0, 0, DateTimeKind.Unspecified), "Yoga", false, new DateTime(2026, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_PassportNumber",
                table: "Clients",
                column: "PassportNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_ClientId",
                table: "Enrollments",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_TrainerId",
                table: "Enrollments",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_Specializations_Name",
                table: "Specializations",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trainers_PassportNumber",
                table: "Trainers",
                column: "PassportNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trainers_SpecializationId",
                table: "Trainers",
                column: "SpecializationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Trainers");

            migrationBuilder.DropTable(
                name: "Specializations");
        }
    }
}
