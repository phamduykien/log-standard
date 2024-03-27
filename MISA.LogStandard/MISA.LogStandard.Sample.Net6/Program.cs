using NLog;
using NLog.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);
GlobalDiagnosticsContext.Set("myVariableName", "myValue");
// Add services to the container.
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.ClearProviders();
    loggingBuilder.AddNLog("Config\\nlog.config");
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.Use(async (context, next) =>
{
    if (context.TraceIdentifier == null)
    {
        context.TraceIdentifier = Guid.NewGuid().ToString();
    }
    await next.Invoke();
});
app.UseAuthorization();

app.MapControllers();

app.Run();
