using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//using otherServices.Data.Model;
//using otherServices.Data.Models;
using otherServices.Data_Project.Models;

namespace otherServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext2 _db;
        public UserController(AppDbContext2 db)
        {
            this._db = db;
        }
        [HttpGet]
        public IActionResult GetUsers()
        {
            var result = _db.Users;// retreive all
            if (result == null)
            {
                return NotFound("Not found");
            }
            else
                return Ok(result);
        }

     

        [HttpGet("waitingLandlords")]
        public IActionResult getwaitinglandlords() // done
        {
            var result = _db.Users.Where(l => l.FlagWaitingUser == 1);
            if (result == null)
            {
                return NotFound();
            }
            else return Ok(result);
        }

        ////[HttpGet("{id:int}")]
        //[HttpGet]
        //public IActionResult getSavedPosts()
        //{
        //    try
        //    {
        //        var result = _db.SavedPosts.Include(sp => sp.Post)
        //                .ThenInclude(p => p.Landlord)
        //            .Include(sp => sp.Tenant)
        //            .Select(sp => new SavedPostDto
        //            {
        //                TenantId = sp.TenantId,
        //                PostId = sp.PostId,
        //                Post = new PostDTo
        //                {
        //                    PostId = sp.Post.PostId,
        //                    Title = sp.Post.Title,
        //                    Body = sp.Post.Description,
        //                    Price = sp.Post.Price,
        //                    Loca = sp.Post.Location,
        //                    Date = sp.Post.DatePost,
        //                    Landlord = new UserDto
        //                    {
        //                        UserId = sp.Post.Landlord.UserId,
        //                        UserName = sp.Post.Landlord.UserName,
        //                        FName = sp.Post.Landlord.FName,
        //                        LName = sp.Post.Landlord.LName
        //                    }
        //                },
        //                Tenant = new UserDto
        //                {
        //                    UserId = sp.Tenant.UserId,
        //                    UserName = sp.Tenant.UserName,
        //                    FName = sp.Tenant.FName,
        //                    LName = sp.Tenant.LName
        //                }
        //            }).ToList();

        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //        return StatusCode(500, ex.Message);
        //    }
        //}


        [HttpGet("showSavedPosts/{Id:int}")]
        public IActionResult showSavedPosts(int Id)
        {
            var savedPosts = _db.SavedPosts
       .Where(sp => sp.TenantId == Id)
       .Include(sp => sp.Post) // you include object post 
           .ThenInclude(p => p.Landlord) // then from post object, Include landlord info from User table since it has an object refrences to user called landlord as object
        .Include(sp => sp.Post)
           .ThenInclude(p => p.Comments)
           .Select(sp => new PostDTo
       {
           PostId = sp.Post.PostId,
           Title = sp.Post.Title,
           Body = sp.Post.Description,
           Loca = sp.Post.Location,
           Date = sp.Post.DatePost,
           Price = sp.Post.Price,
           Landlord = new UserDto
           {
               UserId = sp.Post.Landlord.UserId,
               UserName = sp.Post.Landlord.UserName,
               FName = sp.Post.Landlord.FName,
               LName = sp.Post.Landlord.LName
           },
           // we select becase one:many and it returns many objects
           Comments = sp.Post.Comments.Select(c => new CommentDto
           {
               CommentId = c.CommentId,
               PostId = c.PostId,
               comment_written =c.Description,
               DateComment = c.DateComment
               
           }).ToList()
           })
       .ToList();

            if (savedPosts == null || savedPosts.Count == 0)
            {
                return NotFound("No saved posts found for this user.");
            }

            return Ok(savedPosts);

        }
    }
}
