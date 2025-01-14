namespace Record_Store.Models
{
    public interface IAlbumModel
    {

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
    }
}
