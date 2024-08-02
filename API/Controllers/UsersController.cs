using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;


// [ApiController] // This is now coming from BaseApiController
// [Route("api/[controller]")] // 'controller' is going to be replaced with the first part of the class name (Users). localhost:5001/api/users . This is now coming from BaseApiController
public class UsersController(DataContext context) : BaseApiController
{
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers() 
    {
        var users = await context.Users.ToListAsync();

        return users;
    }

    [Authorize]
    [HttpGet("{id:int}")] // api/users/3
    public async Task<ActionResult<AppUser>> GetUser(int id) 
    {
        var user = await context.Users.FindAsync(id);

        if (user == null) return NotFound();

        return user;
    }
}