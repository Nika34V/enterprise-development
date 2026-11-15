namespace FitnessClub.Contracts.Exceptions;

/// <summary>
/// Переданные данные некорректны (приводит к ответу 400)
/// </summary>
/// <param name="message">Понятное пользователю описание ошибки в данных</param>
public class InvalidRequestException(string message) : Exception(message);
