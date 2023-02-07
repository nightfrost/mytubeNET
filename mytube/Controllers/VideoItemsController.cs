using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mytube.Models;
using mytube.Services;

namespace mytube.Controllers
{
    //[EnableCors("AllowSpecificOrigin")] - Not sure if this is possible
    [EnableCors]
    [Route("api/video")]
    [ApiController]
    public class VideoItemsController : ControllerBase
    {
        private readonly IVideoItemService _videoItemService;

        public VideoItemsController(IVideoItemService videoItemService)
        {
            _videoItemService = videoItemService;
        }

        // GET: api/VideoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VideoItem>>> GetVideos()
        {
            return await _videoItemService.GetVideos();
        }

        // GET: api/VideoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VideoItem>> GetVideoItem(int id)
        {
            var result = await _videoItemService.GetVideoItem(id);

            return result.Value == null ? NotFound() : result;
        }

        // PUT: api/VideoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<VideoItem>> PutVideoItem(int id, VideoItem videoItem)
        {
            var result = await _videoItemService.PutVideoItem(id, videoItem);

            return result.Value == null ? NotFound() : result;
        }

        // POST: api/VideoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost, DisableRequestSizeLimit]
        public async Task<ActionResult<VideoItem>> PostVideoItem()
        {
            var result = await _videoItemService.PostVideoItem(Request);

            return result == null ? BadRequest() : result;
        }

        // DELETE: api/VideoItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteVideoItem(int id)
        {
            var result = await _videoItemService.DeleteVideoItem(id);

            return result.Value.Contains("002-") ? result.Value : NotFound(result.Value);
        }
    }
}
