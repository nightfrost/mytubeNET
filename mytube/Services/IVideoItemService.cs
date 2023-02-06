using Microsoft.AspNetCore.Mvc;
using mytube.Models;

namespace mytube.Services
{
    public interface IVideoItemService
    {
        public Task<ActionResult<IEnumerable<VideoItem>>> GetVideos();

        public Task<ActionResult<VideoItem>> GetVideoItem(int id);

        public Task<IActionResult> PutVideoItem(int id, VideoItem videoItem);

        public Task<ActionResult<VideoItem>> PostVideoItem();

        public Task<IActionResult> DeleteVideoItem(int id);

        public bool VideoItemExists(int id);
    }
}
