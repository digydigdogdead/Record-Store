namespace Record_Store.Models
{
    public interface IAlbumModel
    {
        List<Album> GetAllAlbums();
        Album? GetAlbumById(int id);

    }
    public class AlbumModel : IAlbumModel
    {
        private RecordStoreDbContext _db;

        public AlbumModel(RecordStoreDbContext db)
        {
            _db = db;
        }

        public List<Album> GetAllAlbums()
        {
            return _db.Albums.ToList();
        }

        public Album? GetAlbumById(int id)
        {
            return _db.Albums.FirstOrDefault(a => a.Id == id);
        }

        public bool TryPostAlbum(Album album, out string feedback)
        {
            var allAlbums = GetAllAlbums();

            if (allAlbums.Any(a => a.Id == album.Id))
            {
                feedback = "The supplied Id is not unique.";
                return false;
            }

            try
            {
                _db.Albums.Add(album);
                _db.SaveChanges();
                feedback = "Success";
                return true;
            }
            catch (Exception ex)
            {
                feedback = ex.Message;
                return false;
            }
        }
    }
}
