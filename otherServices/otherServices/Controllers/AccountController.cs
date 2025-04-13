using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using otherServices.Data_Project.Models;

namespace otherServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext2 _db;
        public AccountController(AppDbContext2 db)
        {
            this._db = db;
        }

        [HttpPut("acceptAccount/{Id:int}")]
        public IActionResult acceptAccount(int Id, [FromBody] UserUpdateDto userDto)
        {
            var user = _db.Users.FirstOrDefault(u => u.UserId == Id);
            if (user == null)
            {
                return NotFound();
            }

            // Update user properties from the DTO
            user.UserId = userDto.UserId;
            user.UserName = userDto.UserName;
            user.Email = userDto.Email;
            user.FName = userDto.FName;
            user.LName = userDto.LName;

            user.FlagWaitingUser = 0;
            _db.SaveChanges();

            return Ok(user);
        }
        [HttpPut("deleteAccount/{Id:int}")]
        public IActionResult deleteAccount(int Id, [FromBody] UserUpdateDto userDto)
        {
            var user = _db.Users.FirstOrDefault(u => u.UserId == Id);
            if (user == null)
            {
                return NotFound();
            }

            // Update user properties from the DTO
            user.UserId = userDto.UserId;
            user.UserName = userDto.UserName;
            user.Email = userDto.Email;
            user.FName = userDto.FName;
            user.LName = userDto.LName;

            user.FlagWaitingUser = 2;
            _db.SaveChanges();

            return Ok(user);
        }
    }
}
