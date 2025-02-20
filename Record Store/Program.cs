
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Record_Store.Models;
using Record_Store.Services;
using System;

namespace Record_Store
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            builder.Services.AddDbContext<RecordStoreDbContext>(o => o.UseSqlite(connection));

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddHealthChecks()
                .AddSqlite(
                connectionString: builder.Configuration.GetConnectionString("Connection"),
                name: "Database connection health check",
                failureStatus: HealthStatus.Degraded); ;
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IAlbumModel, AlbumModel>();
            builder.Services.AddScoped<IAlbumService, AlbumService>();

            var app = builder.Build();
            SeedData.AddAlbumData(app);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapHealthChecks("/health");

            app.MapControllers();

            app.Run();
        }

        
    }
}
