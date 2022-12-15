namespace mytube.Models
{
    public class CommentItem
    {
        public long ID { get; set; }
        public string Body { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public ICollection<VideoItem>? Video {get; set;} 
        public ICollection<UserItem>? User { get; set; }
        public bool? IsPinned { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set;}
        public DateTime? LastUpdatedBy { get; set; }
        public DateTime? DeletedAt { get; set;
        }

    }
}
