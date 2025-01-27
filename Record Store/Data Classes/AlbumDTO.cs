using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Record_Store
{
    public class AlbumDTO
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Artist { get; set; }
        [Range(1920, 2026)]
        public int? Year { get; set; }
        public ParentGenre? ParentGenre { get; set; }
        public string? Subgenre { get; set; }
        public int? Stock {  get; set; }
    }
}
