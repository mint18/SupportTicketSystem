using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using TicketSystem.API.Middlewares;
using Xunit;

namespace TicketSystem.API.Tests.Middlewares;

public class RequestLoggingMiddlewareUnitTests
{
    [Fact]
    public async Task InvokeAsync_LogsRequestInformation()
    {
        // arrange
        var loggerMock = new Mock<ILogger<RequestLoggingMiddleware>>();
        string capturedLogMessage = null;

        loggerMock.Setup(x => x.Log(
            It.IsAny<LogLevel>(),
            It.IsAny<EventId>(),
            It.IsAny<It.IsAnyType>(),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception, string>>()))
            .Callback(new InvocationAction(invocation =>
            {
                capturedLogMessage = invocation.Arguments[2].ToString();
            }));

        var context = new DefaultHttpContext();
        context.Request.Method = "GET";
        context.Request.Path = "/api/tickets";

        var middleware = new RequestLoggingMiddleware(ctx => Task.CompletedTask, loggerMock.Object);

        // act
        await middleware.InvokeAsync(context);

        // assert
        capturedLogMessage.Should().Contain("HTTP GET /api/tickets");
    }

    [Fact]
    public async Task InvokeAsync_MeasuresResponseTime()
    {
        // arrange
        var loggerMock = new Mock<ILogger<RequestLoggingMiddleware>>();
        string capturedLogMessage = null;

        loggerMock.Setup(x => x.Log(
           It.IsAny<LogLevel>(),
           It.IsAny<EventId>(),
           It.IsAny<It.IsAnyType>(),
           It.IsAny<Exception>(),
           It.IsAny<Func<It.IsAnyType, Exception, string>>()))
           .Callback(new InvocationAction(invocation =>
           {
               capturedLogMessage = invocation.Arguments[2].ToString();
           }));

        var context = new DefaultHttpContext();
        context.Request.Method = "PATCH";
        context.Request.Path = "/api/tickets";

        static async Task next(HttpContext httpContext)
        {
            await Task.Delay(5550); // Simulate work
        }

        var middleware = new RequestLoggingMiddleware(next, loggerMock.Object);

        // act
        await middleware.InvokeAsync(context);

        // assert
        capturedLogMessage.Should().Contain("ms");
    }
}