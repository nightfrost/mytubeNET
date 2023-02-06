using Microsoft.AspNetCore.Mvc;
using mytube.Models;

namespace mytube.Services
{
    public class VideoItemsServiceImpl : IVideoItemService
    {
        private readonly MytubeContext _context;

        public VideoItemsServiceImpl(MytubeContext context)
        {
            _context = context;
        }

        public Task<IActionResult> DeleteVideoItem(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<VideoItem>> GetVideoItem(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<IEnumerable<VideoItem>>> GetVideos()
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<VideoItem>> PostVideoItem()
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> PutVideoItem(int id, VideoItem videoItem)
        {
            throw new NotImplementedException();
        }

        public bool VideoItemExists(int id)
        {
            throw new NotImplementedException();
        }
    }
}
