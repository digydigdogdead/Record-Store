using Microsoft.AspNetCore.Mvc;
using Record_Store.Services;

namespace Record_Store.Controllers
{
    public class AlbumController : Controller
    {
        private IAlbumService _albumService;

        public AlbumController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
