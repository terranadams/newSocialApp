using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;


[ApiController]
[Route("api/[controller]")] // 'controller' is going to be replaced with the first part of the class name (Users). localhost:5001/api/users 
public class UsersController(DataContext context) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<AppUser>> GetUsers() 
    {
        var users = context.Users.ToList();

        return users;
    }

    [HttpGet("{id:int}")] // api/users/3
    public ActionResult<AppUser> GetUser(int id) 
    {
        var user = context.Users.Find(id);

        if (user == null) return NotFound();

        return user;
    }
}