using Microsoft.AspNetCore.Mvc;
using mytube.Models;

namespace mytube.Services
{
    public interface IVideoItemService
    {
        public Task<IEnumerable<VideoItem>> GetVideos();

        public Task<VideoItem> GetVideoItem(int id);

        public Task<String> PutVideoItem(int id, VideoItem videoItem);

        public Task<VideoItem> PostVideoItem(HttpRequest httpRequest);

        public Task<String> DeleteVideoItem(int id);

        public bool VideoItemExists(int id);
    }
}
