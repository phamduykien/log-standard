﻿using NLog;
using NLog.Extensions.Logging;
var builder = WebApplication.CreateBuilder(args);
NLog.LogManager.Setup().SetupInternalLogger(s =>
{
    System.Diagnostics.Debug.WriteLine(s);
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Thêm nlog để ghi log đến logstash
builder.Services.AddLogging(loggingBuilder =>
{

    loggingBuilder.ClearProviders();
    loggingBuilder.AddNLog("Config\\nlog.config");
});

//builder.Logging.AddNLog("Config/nlog.xml");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
