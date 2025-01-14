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
    }
}
