using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<ActionResult<String>> DeleteVideoItem(int id)
        {
            try
            {
                var videoItem = await _context.Videos.FindAsync(id);
                if (videoItem == null)
                {
                    return String.Format("001- No video with id: {0} exists.", id);
                }
                else
                {
                    _context.Videos.Remove(videoItem);
                    await _context.SaveChangesAsync();
                    return String.Format("002- Video with id: {0} has been deleted.", id);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went horribly wrong... See below for info.");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                return "003- User was not deleted.";
            }
        }

        public async Task<ActionResult<VideoItem>> GetVideoItem(int id)
        {
            return await _context.Videos.Include(p => p.User).FirstAsync(p => p.ID == id);
        }

        public async Task<ActionResult<IEnumerable<VideoItem>>> GetVideos()
        {
            return await _context.Videos.ToListAsync();
        }

        public async Task<ActionResult<VideoItem>> PostVideoItem(HttpRequest request)
        {
            VideoItem videoItem = new VideoItem();
            byte[] fileAsByteArray;
            var file = request.Form.Files[0];
            string name = request.Form["name"];
            string userId = request.Form["userid"];

            try
            {
                var userItem = await _context.Users.FindAsync(long.Parse(userId));
                
                if (userItem == null)
                {
                    Console.WriteLine("User not found on create video. Returning empty video item.");
                    return videoItem;
                }

                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        fileAsByteArray = ms.ToArray();
                        videoItem.Data = fileAsByteArray;
                        videoItem.Name = name;
                        videoItem.User = userItem;
                    }
                }

                _context.Videos.Add(videoItem);
                await _context.SaveChangesAsync();

                return videoItem;
            }
            catch (Exception ex)
            {
                Console.WriteLine("returning empty video object, see below for details.");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(videoItem.ToString());
                return videoItem;
            }
        }

        public async Task<ActionResult<VideoItem>> PutVideoItem(int id, VideoItem videoItem)
        {
            var videoItemFromDB = await _context.Videos.FindAsync(id);

            if (videoItemFromDB == null)
            {
                return videoItemFromDB;
            }

            videoItemFromDB = videoItem;

            try
            {
                _context.Entry(videoItemFromDB).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return videoItemFromDB;
            }
            catch (Exception e)
            {
                //return null reference and handle in controller.
                VideoItem tempItem = null;
                Console.WriteLine("Updating video with ID: {0} failed. See stack.");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                return tempItem;
            }
        }

        public bool VideoItemExists(int id)
        {
            return _context.Videos.Any(e => e.ID == id);
        }
    }
}
