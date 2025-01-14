﻿using Record_Store.Models;

namespace Record_Store.Services
{
    public interface IAlbumService
    {
        List<Album> GetAllAlbums();
        Album? GetAlbumById(int id);
        Album? TryPostAlbum(Album album, out string feedback);
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

        public Album? TryPostAlbum(Album album, out string feedback)
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

            return _model.TryPostAlbum(album, out feedback);
        }
    }
}
