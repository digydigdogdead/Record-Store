using Record_Store.Models;

namespace Record_Store.Services
{
    public interface IAlbumService
    {

    }
    public class AlbumService : IAlbumService
    {
        private IAlbumModel _album;
        public AlbumService(IAlbumModel album)
        {
            _album = album;
        }
    }
}
