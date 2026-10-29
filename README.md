# Разработка корпоративных приложений лабораторная №2
## Вариант

| | |
|---|---|
| Номер варианта | 3 |
| Предметная область | Фитнес-клуб |

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
5. Топ-5 самых популярных тренеров по числу записей на занятия.

## Технологический стек

- Платформа: .NET 10.0
- Веб-фреймворк: ASP.NET Core Web API
- Маппинг: ручной маппинг (Domain ↔ DTO) в сервисном слое
- Документация API: Swagger UI / OpenAPI с интеграцией XML-комментариев
- Фреймворк тестирования: xUnit v3
- Continuous Integration: GitHub Actions (автоматический запуск тестов при push / pull_request)
- Оркестрация: .NET Aspire (AppHost)
- Архитектура: слоистая (Domain → Contracts → Application → Infrastructure.InMemory → Api), Repository pattern

## Структура решения

```
FitnessClub.slnx
├── .github/
│   └── workflows/
│       └── Test.yml                          — CI: восстановление, сборка, запуск тестов
│
├── src/
│   ├── FitnessClub.Domain/                   — доменная модель
│   │   ├── Entities/                         — Person, Client, Trainer, Specialization, Enrollment
│   │   ├── Enums/                            — Gender, Hall
│   │   ├── Seeds/                            — датасид (10+ экземпляров каждого класса)
│   │   └── Interfaces/                       — IRepository<T> и интерфейсы репозиториев сущностей
│   │
│   ├── FitnessClub.Contracts/                — контракты API
│   │   ├── Dto/                              — DTO для чтения и для создания/изменения
│   │   └── Interfaces/                       — IEntityService<TDto, TEditDto> и интерфейсы сервисов
│   │
│   ├── FitnessClub.Application/              — бизнес-логика
│   │   ├── Mapping/                          — ручной маппинг Domain ↔ DTO 
│   │   ├── Services/                         — CRUD-сервисы и AnalyticsService
│   │   └── DependencyInjection/               — регистрация сервисов
│   │
│   ├── FitnessClub.Infrastructure.InMemory/  — реализация репозиториев поверх коллекций в памяти
│   │   ├── FitnessClubInMemoryStore.cs       — общее хранилище данных 
│   │   ├── Repositories/                     — InMemory-репозитории
│   │   └── DependencyInjection/               — регистрация репозиториев
│   │
│   ├── FitnessClub.Api/                      — REST API
│   │   ├── Controllers/                      — ClientsController, TrainersController,
│   │   │                                       SpecializationsController, EnrollmentsController,
│   │   │                                       AnalyticsController
│   │   └── Program.cs                        — конфигурация DI, Swagger, middleware
│   │
│   └── FitnessClub.AppHost/                  — оркестратор .NET Aspire, точка запуска системы
│
└── tests/
    └── FitnessClub.Domain.Tests/             — unit-тесты (xUnit v3)
```



