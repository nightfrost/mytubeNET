namespace mytube.Models
{
    public class VideoItem
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public byte[]? Poster { get; set; } //Adding byte for poster images

        public DateTime? ReleaseDate { get; set; } //adding release date, for filtering later in ts

        public byte[] Data { get; set; }

        public UserItem User { get; set; }
    }
}
