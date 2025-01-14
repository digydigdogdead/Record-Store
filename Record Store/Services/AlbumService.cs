﻿using Record_Store.Models;

namespace Record_Store.Services
{
    public interface IAlbumService
    {
        List<Album> GetAllAlbums();
        Album? GetAlbumById(int id);
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
    }
}
