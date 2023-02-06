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

        public async Task<String> DeleteVideoItem(int id)
        {
            try
            {
                var videoItem = await _context.Videos.FindAsync(id);
                if (videoItem == null)
                {
                    return String.Format("No user with id: {0} exists.", id);
                }
                else
                {
                    _context.Videos.Remove(videoItem);
                    return String.Format("User with id: {0} has been deleted.", id);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went horribly wrong... See below for info.");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                return "User was not deleted.";
            }
        }

        public async Task<VideoItem> GetVideoItem(int id)
        {
            var videoItem = await _context.Videos.Include(p => p.User).FirstAsync(p => p.ID == id);

            if (videoItem == null)
            {
                videoItem = new VideoItem();
                return videoItem;
            } else
            {
                return videoItem;
            }
        }

        public async Task<IEnumerable<VideoItem>> GetVideos()
        {
            return await _context.Videos.ToListAsync();
        }

        public async Task<VideoItem> PostVideoItem(HttpRequest request)
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

        public async Task<String> PutVideoItem(int id, VideoItem videoItem)
        {
            var videoItemFromDB = await _context.Videos.FindAsync(id);

            videoItemFromDB = videoItem;

            try
            {
                _context.Entry(videoItemFromDB).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return "Video updated.";
            }
            catch (Exception e)
            {
                Console.WriteLine("Updating video with ID: {0} failed. See stack.");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                return "Video update failed. See stack.";
            }
        }

        public bool VideoItemExists(int id)
        {
            return _context.Videos.Any(e => e.ID == id);
        }
    }
}
