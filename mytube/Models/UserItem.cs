namespace mytube.Models
{
    public class UserItem
    {
        public long ID { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string username { get; set; }

        public string password { get; set; }

        public string email { get; set; }

        public string? phone { get; set; }

        public string? dob { get; set; }

        public string? nationality { get; set; }

        public int? age { get; set; }

        public DateTime createdAt { get; set; }

        public bool enabled { get; set; }

        public ICollection<VideoItem>? Videos { get; set; }
    }
}
