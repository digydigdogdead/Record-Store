﻿using Microsoft.AspNetCore.Mvc;
using Record_Store.Services;

namespace Record_Store.Controllers
{
    [ApiController]
    [Route("/albums")]
    public class AlbumController : Controller
    {
        private IAlbumService _albumService;

        public AlbumController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        [HttpGet]
        public IActionResult GetAllAlbums()
        {
            var result = _albumService.GetAllAlbums();
            if (result.Count > 1) return Ok(result);
            else return NoContent();
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetAlbumById(int id)
        {
            var album = _albumService.GetAlbumById(id);
            if (album == null) return NotFound();
            else return Ok(album);
        }

        [HttpPost]
        public IActionResult PostAlbum(Album album)
        {
            var result = _albumService.PostAlbum(album, out string feedback);

            if (result is null) return BadRequest(feedback);
            return Ok(result);
        }

        [HttpPatch]
        [Route("{id}")]
        public IActionResult PatchAlbum(int id, AlbumDTO albumDTO)
        {
            albumDTO.Id = id;

            var serviceResult = _albumService.UpdateAlbum(albumDTO, out string feedback);

            if (serviceResult is null && feedback == "Album not found.")
            {
                return NotFound();
            }
            else if (serviceResult is null) return BadRequest(feedback);
            else return Ok(serviceResult);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteAlbum(int id)
        {
            bool result = _albumService.TryDeleteAlbum(id, out string feedback);

            if (!result && feedback == "Album not found.")
            {
                return NotFound();
            }
            else if (!result) return BadRequest(feedback);
            else return NoContent();
        }

        [HttpGet]
        [Route("/artist/{artist}")]
        public IActionResult GetAlbumsByArtist(string artist)
        {
            var result = _albumService.GetAlbumsByArtist(artist);

            if (result.Count == 0) return NotFound();

            return Ok(result);
        }

        [HttpPatch]
        [Route("buy/{id}")]
        public IActionResult PurchaseAlbum(int id)
        {
            var result = _albumService.PurchaseAlbum(id, out string feedback);

            if (result is null && feedback == "Album not found.")
            {
                return NotFound();
            }
            else if (result is null) return BadRequest(feedback);
            else return Ok(result);
        }
    }
}
