using Microsoft.AspNetCore.Mvc;
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
            
    }
}
