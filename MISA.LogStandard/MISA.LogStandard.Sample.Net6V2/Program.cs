using NLog;
using NLog.Extensions.Logging;
using NLog.Web;
using NLog.Web.LayoutRenderers;


namespace MISA.LogStandard.Sample.Net6V2
{
    public class Program
    {
        public static void Main(string[] args)
        {
           var logger= NLog.LogManager.Setup().LoadConfigurationFromFile("Config\\nlog.config");
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.      
            builder.Services.AddLogging(loggingBuilder =>
            {                
                loggingBuilder.ClearProviders();
                loggingBuilder.AddNLog();
            });
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Host.UseNLog();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.Use(async (context, next) =>
            {                
                await next.Invoke();
            });

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
