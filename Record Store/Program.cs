
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Record_Store.Models;
using Record_Store.Services;
using System;

namespace Record_Store
{
    public class Program
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
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IAlbumModel, AlbumModel>();
            builder.Services.AddScoped<IAlbumService, AlbumService>();

            var app = builder.Build();
            AddAlbumData(app);

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

        static void AddAlbumData(WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetService<RecordStoreDbContext>();


            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var takeMeBackToEden = new Album
            {
                Id = 1,
                Artist = "Sleep Token",
                Name = "Take Me Back To Eden",
                Year = 2023,
                ParentGenre = ParentGenre.METAL
            };

            var madagascar2 = new Album
            {
                Id = 2,
                Artist = "Various Artists",
                Name = "Madagascar: Escape 2 Africa",
                Year = 2008,
                ParentGenre = ParentGenre.CLASSICAL
            };

            db.Albums.Add(takeMeBackToEden);
            db.Albums.Add(madagascar2);
            
            db.SaveChanges();
        }
    }
}
