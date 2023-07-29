using System.ComponentModel.DataAnnotations;

namespace boards.Infrastructure;

public class BoardDb
{
    public String Slug { get; set; }
    public String Name { get; set; }
}