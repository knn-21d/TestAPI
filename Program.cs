using Microsoft.EntityFrameworkCore;
using TestAPI.Data;
using TestAPI.Services;

namespace TestAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ApplicationContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")), contextLifetime: ServiceLifetime.Transient, optionsLifetime: ServiceLifetime.Transient);
            builder.Services.AddTransient<CountersService>();
            builder.Services.AddTransient<CountersDataProvider>();
            builder.Services.AddHostedService<BackgroundTaskService>();
            builder.Services.AddMemoryCache();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}