namespace mytube.Models
{
    public class PlaylistItem
    {
        public int Id { get; set; }
        public string PlaylistName { get; set; }
        
        public string PlaylistDescription { get; set; }
        public string PlaylistType { get; set;}
        public string PlaylistUrl { get; set; }
        public string PlaylistTitle { get; set;}
        public ICollection<VideoItem>? Video { get; set; }
        public ICollection<UserItem>? User { get; set; }
    }
}
