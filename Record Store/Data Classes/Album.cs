namespace Record_Store
{
    public class Album
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public int Year { get; set; }
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
