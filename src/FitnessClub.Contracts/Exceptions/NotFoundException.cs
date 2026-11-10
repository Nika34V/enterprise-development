namespace FitnessClub.Contracts.Exceptions;

/// <summary>
/// Запрошенный объект не найден (приводит к ответу 404)
/// </summary>
/// <param name="message">Понятное пользователю описание того, что не найдено</param>
public class NotFoundException(string message) : Exception(message);
