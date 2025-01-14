namespace Record_Store
{
    public class Album
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Artist { get; set; }
        public required int Year { get; set; }
        public required ParentGenre ParentGenre { get; set; }
        public string? Subgenre { get; set; }
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
