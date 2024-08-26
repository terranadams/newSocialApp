using System.Reflection.Metadata.Ecma335;

namespace API.DTOs;

public class PhotoDto
{
    public int Id {get; set;}
    public string? Url {get; set;}
    public bool IsMain {get; set;}
}