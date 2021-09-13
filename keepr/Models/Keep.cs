using System.ComponentModel.DataAnnotations;

namespace keepr.Models
{
  public class Keep
  {

    public int Id { get; set; }
    [Required]
    [MinLength(3)]
    [MaxLength(255)]

    public string Name { get; set; }
    [Required]
    [MinLength(3)]
    [MaxLength(255)]

    public string Description { get; set; }
    public string Img { get; set; }
    public int Views { get; set; }
    public int Shares { get; set; }
    public int Keeps { get; set; }

    public string CreatorId { get; set; }
    public Profile Creator { get; set; }

  }
}