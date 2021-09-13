using System.ComponentModel.DataAnnotations;

namespace keepr.Models
{
  public class Vault
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
    public bool? isPrivate { get; set; }
    public string CreatorId { get; set; }
    public Profile Creator { get; set; }

  }
}