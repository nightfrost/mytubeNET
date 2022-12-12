using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mytube.Models;

namespace mytube.Controllers
{
    [Route("api/video")]
    [ApiController]
    public class VideoItemsController : ControllerBase
    {
        private readonly MytubeContext _context;

        public VideoItemsController(MytubeContext context)
        {
            _context = context;
        }

        // GET: api/VideoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VideoItem>>> GetVideos()
        {
            return await _context.Videos.ToListAsync();
        }

        // GET: api/VideoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VideoItem>> GetVideoItem(int id)
        {
            var videoItem = await _context.Videos.Include(p => p.User).FirstAsync(p => p.ID == id);

            if (videoItem == null)
            {
                return NotFound();
            }

            return videoItem;
        }

        // PUT: api/VideoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVideoItem(int id, VideoItem videoItem)
        {
            if (id != videoItem.ID)
            {
                return BadRequest();
            }

            _context.Entry(videoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VideoItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/VideoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost, DisableRequestSizeLimit]
        public async Task<ActionResult<VideoItem>> PostVideoItem()
        {
            VideoItem videoItem = new VideoItem();
            var tempUserItem = await _context.Users.FindAsync(2L);
            try
            {
                byte[] fileAsByteArray;
                var file = Request.Form.Files[0];
                string name = Request.Form.Keys.ElementAt(0);
                string userId = Request.Form.Keys.ElementAt(0);
                Console.WriteLine(name);
                Console.WriteLine(userId);

                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        fileAsByteArray = ms.ToArray();
                        videoItem.Data = fileAsByteArray;
                        videoItem.Name = name;
                        
                        videoItem.User = tempUserItem;
                    }
                }

                _context.Videos.Add(videoItem);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetVideoItem", new { id = videoItem.ID }, videoItem);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            

            return NoContent();
        }

        // DELETE: api/VideoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVideoItem(int id)
        {
            var videoItem = await _context.Videos.FindAsync(id);
            if (videoItem == null)
            {
                return NotFound();
            }

            _context.Videos.Remove(videoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VideoItemExists(int id)
        {
            return _context.Videos.Any(e => e.ID == id);
        }
    }
}
