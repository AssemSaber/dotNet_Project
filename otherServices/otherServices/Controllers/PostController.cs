using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using otherServices.Data;
using otherServices.Data_Project.Models;

//using otherServices.Data.Models;

namespace otherServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly AppDbContext2 _db;
        public PostController(AppDbContext2 db)
        {
            this._db = db;
        }

       
        [HttpGet("waitingPosts")]
        public IActionResult getWaitingposts() // done
        {
            var result = _db.Posts.Where(p => p.FlagWaitingPost == 1);
            if (result == null)
            {
                return NotFound();
            }
            else return Ok(result);
        }


        [HttpGet("showPosts")]
        public IActionResult showPosts()
        {
            var posts = _db.Posts
        .Where(p => (p.FlagWaitingPost == 0) && (p.RentalStatus == "available"))
        .Include(p => p.Comments)  // Explicitly include Comments
        .Select(p => new PostDTo
        {
            Landlord = new UserDto  // since you have one object, I have moved up, because the order is important
            {
                UserId = p.Landlord.UserId,
                UserName = p.Landlord.UserName,
                FName = p.Landlord.FName,
                LName = p.Landlord.LName
            },
            PostId = p.PostId,
            Title = p.Title,
            Body = p.Description,
            Loca = p.Location,
            Date = p.DatePost,
            Price = p.Price,
            Comments = p.Comments.Select(c => new CommentDto // since you have multiple objects
            {
                CommentId = c.CommentId,
                PostId = c.PostId,
                comment_written = c.Description,
                DateComment = c.DateComment

            }).ToList()
        })
        .ToList();

            ;
            if (posts == null)
            {
                return NotFound();
            }
            else return Ok(posts);
        }
        [HttpPost("savePost")]
        public IActionResult SavePost( [FromBody] SavePostDTo SavePostDTo)
        {
            var savedPost = new SavedPost
            {
                TenantId = SavePostDTo.LandlordId, // Id of landlord in user table
                PostId = SavePostDTo.PostId,    // Id of post
                DateSaved = DateTime.Now
            };
            _db.Add(savedPost);
            _db.SaveChanges();
            return Ok("The post is saved");
        }
    }
}
