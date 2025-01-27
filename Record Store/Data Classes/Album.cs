using System.ComponentModel.DataAnnotations;

namespace Record_Store
{
    public class Album
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Artist { get; set; }
        [Required, Range(1920, 2026)]
        public int Year { get; set; }
        [Required]
        public ParentGenre ParentGenre { get; set; }
        public string? Subgenre { get; set; }
        public int Stock { get; set; } = 1;
    }

    public enum ParentGenre
    {
        ROCK,
        POP,
        METAL,
        JAZZ,
        RAP,
        CLASSICAL
    }
}
