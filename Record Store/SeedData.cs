namespace Record_Store
{
    public static class SeedData
    {
        public static void AddAlbumData(WebApplication app)
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
                ParentGenre = ParentGenre.METAL,
                Stock = 1
            };

            var madagascar2 = new Album
            {
                Id = 2,
                Artist = "Various Artists",
                Name = "Madagascar: Escape 2 Africa",
                Year = 2008,
                ParentGenre = ParentGenre.CLASSICAL,
                Stock = 1
            };

            db.Albums.Add(takeMeBackToEden);
            db.Albums.Add(madagascar2);

            db.SaveChanges();
        }
    }
}
