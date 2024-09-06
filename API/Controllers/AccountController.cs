using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController(DataContext context, ITokenService tokenService) : BaseApiController
{
    [HttpPost("register")] // account/register
    public async Task<ActionResult<UserDto>> Register(RegisterDTO registerDTO)
    {
        if (await UserExists(registerDTO.Username)) return BadRequest("Username already taken");
        return Ok();

        // using var hmac = new HMACSHA512(); // Create a new instance of HMACSHA512 to hash the password.

        // var user = new AppUser 
        // {
        //     UserName = registerDTO.Username.ToLower(),
        //     PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)), // Compute the hash of the password.
        //     PasswordSalt = hmac.Key // Save the key (salt) used in the hashing algorithm.
        // };

        // context.Users.Add(user); // Add the new user to the database context.
        // await context.SaveChangesAsync(); // Save the changes to the database asynchronously.

        // return new UserDto
        // {
        //     Username = user.UserName,
        //     Token = tokenService.CreateToken(user)
        // };
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        // Retrieve the user from the database based on the username.
        var user = await context.Users
            .Include(p => p.Photos)
            .FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

        // If the user is not found, return an Unauthorized response.
        if (user == null) return Unauthorized("Invalid username or password");

        // Use HMACSHA512 with the stored password salt to compute the hash of the provided password.
        using var hmac = new HMACSHA512(user.PasswordSalt);

        // Compute the hash of the input password.
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        // Compare the computed hash with the stored hash to verify the password.
        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
        }

        return new UserDto
        {
            Username = user.UserName,
            Token = tokenService.CreateToken(user),
            PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url
        };
    }

    private async Task<bool> UserExists(string username) // Define a private helper method to check if a user exists based on the username.
    {
        // Check if any user in the database matches the provided username (case-insensitive).
        return await context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
    }
}