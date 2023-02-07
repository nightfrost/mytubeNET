using Microsoft.AspNetCore.Mvc;
using mytube.Models;

namespace mytube.Services
{
    public interface IVideoItemService
    {
        public Task<ActionResult<IEnumerable<VideoItem>>> GetVideos();

        public Task<ActionResult<VideoItem>> GetVideoItem(int id);

        public Task<ActionResult<VideoItem>> PutVideoItem(int id, VideoItem videoItem);

        public Task<ActionResult<VideoItem>> PostVideoItem(HttpRequest httpRequest);

        public Task<ActionResult<String>> DeleteVideoItem(int id);

        public bool VideoItemExists(int id);
    }
}
