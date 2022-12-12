namespace mytube.Models
{
    public class VideoItem
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public byte[] Data { get; set; }

        public UserItem User { get; set; }
    }
}
