using FitnessClub.Contracts.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.ExceptionHandling;

/// <summary>
/// Глобальный обработчик исключений: превращает исключения сервисов в ответы с нужным HTTP-кодом.
/// Текст непредвиденных ошибок пользователю не показывается, они только записываются в лог
/// </summary>
/// <param name="logger">Логгер для записи непредвиденных ошибок</param>
public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    /// <summary>
    /// Формирует ответ для исключения
    /// </summary>
    /// <param name="httpContext">Контекст текущего запроса</param>
    /// <param name="exception">Возникшее исключение</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns><c>true</c>: исключение обработано</returns>
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        ProblemDetails problem;

        switch (exception)
        {
            case NotFoundException:
                problem = new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Объект не найден",
                    Detail = exception.Message
                };
                break;

            case InvalidRequestException:
                problem = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Некорректный запрос",
                    Detail = exception.Message
                };
                break;

            default:
                logger.LogError(exception, "Необработанная ошибка при обработке запроса {Method} {Path}",
                    httpContext.Request.Method, httpContext.Request.Path);
                problem = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Внутренняя ошибка сервера",
                    Detail = "Произошла внутренняя ошибка. Попробуйте повторить запрос позже"
                };
                break;
        }

        httpContext.Response.StatusCode = problem.Status ?? StatusCodes.Status500InternalServerError;
        await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken);

        return true;
    }
}
