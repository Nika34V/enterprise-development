# Разработка корпоративных приложений лабораторная №3

## Мой вариант

| | |
|---|---|
| Номер варианта | 3 |
| Предметная область | Фитнес-клуб |
| База данных | MS SQL Server |
| Шина сообщений | RabbitMQ |

### Предметная область

В базе данных фитнес-клуба хранится информация о записях клиентов на персональные занятия к тренерам.

- **Клиент**: номер паспорта, ФИО, пол, дата рождения, телефон, дата начала абонемента, дата окончания абонемента.
- **Тренер**: номер паспорта, ФИО, пол, дата рождения, специализация, стаж работы. Специализация — справочник.
- **Клиент** и **Тренер** наследуются от общего базового класса `Person` (общие свойства: паспорт, ФИО, пол, дата рождения).
- **Запись на занятие** (контракт): дата и время начала и окончания занятия, зал, признак пробного посещения, клиент, тренер.

### Аналитические запросы

1. Тренеры со стажем работы не менее заданного количества лет.
2. Проверка, доступен ли зал для записи в указанный момент времени.
3. Клиенты с просроченным абонементом, упорядоченные по ФИО.
4. Занятия выбранного зала за указанный год и месяц.
5. Топ-5 самых популярных тренеров по числу записей на занятия (с количеством записей у каждого).

## Что реализовано в лабораторной работе 3

- Новый проект `FitnessClub.Infrastructure.MsSqlServer` с реализацией тех же интерфейсов репозиториев, что и в `Infrastructure.InMemory`, но поверх базы данных. Переход с хранения в памяти на базу данных выполнен заменой одной строки регистрации в DI (`AddInMemoryPersistence()` на `AddMsSqlServerPersistence(...)`), сервисы и контроллеры не менялись.
- `FitnessClubDbContext` и настройки таблиц (`IEntityTypeConfiguration`): ограничения длины полей, уникальные индексы (номер паспорта, название специализации), внешние ключи, хранение перечислений `Gender` и `Hall` в виде строк.
- Миграция `InitialCreate`: создаёт таблицы `Specializations`, `Trainers`, `Clients`, `Enrollments` и заполняет их начальными данными из `DataSeeder` (10+ записей в каждой таблице).
- Миграции применяются автоматически при старте сервера.
- Оркестратор .NET Aspire (`AppHost`) запускает SQL Server в контейнере, создаёт базу `fitnessclub`, передаёт серверу строку подключения и запускает сервер только после готовности базы.
- Каскадное удаление: при удалении клиента или тренера удаляются и их записи на занятия. Специализацию, назначенную тренерам, удалить нельзя (ответ 400).

## Технологический стек

- Платформа: .NET 10.0
- Веб-фреймворк: ASP.NET Core Web API
- ORM: Entity Framework Core 10, провайдер для MS SQL Server, миграции
- База данных: MS SQL Server (контейнер Docker, запускается через Aspire)
- Маппинг: ручной маппинг (Domain ↔ DTO) в сервисном слое
- Обработка ошибок: исключения сервисов + глобальный `IExceptionHandler` (ответы в формате ProblemDetails)
- Документация API: OpenAPI (Microsoft.AspNetCore.OpenApi) + Scalar UI с интеграцией XML-комментариев
- Фреймворк тестирования: xUnit v3
- Continuous Integration: GitHub Actions (автоматический запуск тестов при push / pull_request)
- Оркестрация: .NET Aspire (AppHost)
- Архитектура: слоистая (Shared → Domain → Contracts → Application → Infrastructure → Api), Repository pattern

## Структура решения

```
FitnessClub.slnx
├── .github/
│   └── workflows/
│       └── Test.yml                                — CI: восстановление, сборка, запуск тестов
│
├── src/
│   ├── FitnessClub.Shared/                         — общие типы (enum Gender, Hall)
│   │   └── Enums/
│   │
│   ├── FitnessClub.Domain/                         — доменная модель
│   │   ├── Entities/                               — Person, Client, Trainer, Specialization, Enrollment
│   │   ├── Seeds/
│   │   │   └── DataSeeder.cs                       — датасид (10+ экземпляров каждого класса)
│   │   └── Interfaces/                             — IRepository<T> и интерфейсы репозиториев сущностей
│   │
│   ├── FitnessClub.Contracts/                      — контракты API
│   │   ├── Dto/                                    — DTO для чтения и для создания/изменения
│   │   ├── Exceptions/                             — NotFoundException, InvalidRequestException
│   │   └── Interfaces/                             — IEntityService<TDto, TEditDto> и интерфейсы сервисов
│   │
│   ├── FitnessClub.Application/                    — бизнес-логика
│   │   ├── Mapping/                                — ручной маппинг Domain ↔ DTO (по одному файлу на сущность)
│   │   ├── Services/                               — CRUD-сервисы (с проверкой данных и логированием) и AnalyticsService
│   │   └── DependencyInjection/                    — регистрация сервисов
│   │
│   ├── FitnessClub.Infrastructure.InMemory/        — реализация репозиториев поверх коллекций в памяти
│   │   ├── FitnessClubInMemoryStore.cs             — хранилище данных в памяти
│   │   ├── Repositories/                           — InMemory-репозитории
│   │   └── DependencyInjection/                    — регистрация репозиториев
│   │
│   ├── FitnessClub.Infrastructure.MsSqlServer/     — реализация репозиториев поверх MS SQL Server (EF Core)
│   │   ├── FitnessClubDbContext.cs                 — контекст базы данных
│   │   ├── FitnessClubDbContextFactory.cs          — фабрика контекста для инструментов миграций
│   │   ├── Configurations/                         — настройки таблиц и начальные данные
│   │   ├── Repositories/                           — EF-репозитории
│   │   ├── Migrations/                             — миграции (InitialCreate)
│   │   └── DependencyInjection/                    — регистрация контекста и репозиториев, применение миграций
│   │
│   ├── FitnessClub.Api/                            — REST API
│   │   ├── Controllers/                            — ClientsController, TrainersController,
│   │   │                                             SpecializationsController, EnrollmentsController,
│   │   │                                             AnalyticsController
│   │   ├── ExceptionHandling/                      — глобальный обработчик исключений
│   │   └── Program.cs                              — конфигурация DI, OpenAPI, Scalar, middleware
│   │
│   └── FitnessClub.AppHost/                        — оркестратор .NET Aspire: SQL Server, база данных и сервер
│
└── tests/
    └── FitnessClub.Domain.Tests/                   — unit-тесты (xUnit v3)
```