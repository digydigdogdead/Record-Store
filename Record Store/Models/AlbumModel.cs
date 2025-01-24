using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Record_Store.Models
{
    public interface IAlbumModel
    {
        List<Album> GetAllAlbums();
        Album? GetAlbumById(int id);
        Album? PostAlbum(Album album, out string feedback);
        Album? UpdateAlbum(AlbumDTO albumUpdate, out string feedback);
        bool TryDeleteAlbum(int albumId, out string feedback);
        List<Album> GetAlbumsByArtist(string artist);
        List<Album> GetAllAlbumsInStock();

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

        public List<Album> GetAlbumsByArtist(string artist)
        {
            var allAlbums = GetAllAlbums();

            return (from album in allAlbums
                    where album.Artist.ToUpper() == artist.ToUpper()
                    select album).ToList(); 
        }

        public Album? GetAlbumById(int id)
        {
            return _db.Albums.FirstOrDefault(a => a.Id == id);
        }

        public List<Album> GetAllAlbumsInStock()
        {
            return (from album in _db.Albums
                    where album.Stock > 0
                    select album).ToList();
        }

        public Album? PostAlbum(Album album, out string feedback)
        {
            var allAlbums = GetAllAlbums();

            if (allAlbums.Any(a => a.Id == album.Id))
            {
                feedback = "The supplied Id is not unique.";
                return null;
            }

            try
            {
                _db.Albums.Add(album);
                _db.SaveChanges();
                feedback = "Success";
                return album;
            }
            catch (Exception ex)
            {
                feedback = ex.Message;
                return null;
            }
        }

        public Album? UpdateAlbum(AlbumDTO albumUpdate, out string feedback)
        {
            var oldAlbum = GetAlbumById((int)albumUpdate.Id);

            if (oldAlbum == null)
            {
                feedback = "Album not found.";
                return null;
            }

            if (!String.IsNullOrEmpty(albumUpdate.Name) &&
                albumUpdate.Name != oldAlbum.Name)
            {
                oldAlbum.Name = albumUpdate.Name;
            }

            if (!String.IsNullOrEmpty(albumUpdate.Artist) &&
                albumUpdate.Artist != oldAlbum.Artist)
            {
                oldAlbum.Artist = albumUpdate.Artist;
            }

            if (albumUpdate.ParentGenre != null && albumUpdate.ParentGenre != oldAlbum.ParentGenre)
            {
                oldAlbum.ParentGenre = (ParentGenre)albumUpdate.ParentGenre;
            }
                

            if (!String.IsNullOrEmpty(albumUpdate.Subgenre) &&
                albumUpdate.Subgenre != oldAlbum.Subgenre)
            {
                oldAlbum.Subgenre = albumUpdate.Subgenre;
            }

            if (albumUpdate.Year != null && albumUpdate.Year != oldAlbum.Year)
            {
                oldAlbum.Year = (int)albumUpdate.Year;
            }

            if (albumUpdate.Stock != null && albumUpdate.Stock != oldAlbum.Stock)
            {
                oldAlbum.Stock = (int)albumUpdate.Stock;
            }

            try
            {
                _db.Albums.Update(oldAlbum);
                _db.SaveChanges();
                feedback = "Success";
                return oldAlbum;
            }
            catch (Exception ex)
            {
                feedback = ex.Message;
                return null;
            }

        }
        public bool TryDeleteAlbum(int albumId, out string feedback)
        {
            Album? albumToDelete = GetAlbumById(albumId);

            if (albumToDelete == null)
            {
                feedback = "Album not found.";
                return false;
            }

            try
            {
                _db.Albums.Remove(albumToDelete);
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
