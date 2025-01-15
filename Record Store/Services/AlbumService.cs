using Record_Store.Models;

namespace Record_Store.Services
{
    public interface IAlbumService
    {
        List<Album> GetAllAlbums();
        Album? GetAlbumById(int id);
        Album? PostAlbum(Album album, out string feedback);
        Album? UpdateAlbum(AlbumDTO album, out string feedback);
        bool TryDeleteAlbum(int id, out string feedback);
        List<Album> GetAlbumsByArtist(string artist);
        Album? PurchaseAlbum(int id, out string feedback);
    }
    public class AlbumService : IAlbumService
    {
        private IAlbumModel _model;
        public AlbumService(IAlbumModel model)
        {
            _model = model;
        }

        public List<Album> GetAllAlbums() 
        {
            return _model.GetAllAlbums();
        }

        public Album? GetAlbumById(int id)
        {
            return _model.GetAlbumById(id);
        }

        public Album? PostAlbum(Album album, out string feedback)
        {
            if (String.IsNullOrEmpty(album.Artist)
                || String.IsNullOrEmpty(album.Name))
            {
                feedback = "Missing required fields";
                return null;
            }

            if (String.IsNullOrEmpty(album.Subgenre))
            {
                album.Subgenre = album.ParentGenre.ToString();
            }

            return _model.PostAlbum(album, out feedback);
        }

        public Album? UpdateAlbum(AlbumDTO album, out string feedback)
        {
            return _model.UpdateAlbum(album, out feedback);
        }

        public bool TryDeleteAlbum(int id, out string feedback)
        {
            return _model.TryDeleteAlbum(id, out feedback);
        }

        public List<Album> GetAlbumsByArtist(string artist)
        {
            return _model.GetAlbumsByArtist(artist);
        }

        public Album? PurchaseAlbum(int id, out string feedback)
        {
            var albumToPurchase = _model.GetAlbumById(id);

            if (albumToPurchase == null)
            {
                feedback = "Album not found.";
                return null;
            }

            if (albumToPurchase.Stock < 1)
            {
                feedback = "Not enough stock.";
                return null;
            }
            else
            {
                AlbumDTO purchaseUpdate = new AlbumDTO
                {
                    Id = albumToPurchase.Id,
                    Stock = albumToPurchase.Stock - 1
                };
                return _model.UpdateAlbum(purchaseUpdate, out feedback);
            }
        }
    }
}
