using System.ComponentModel.DataAnnotations;

namespace MonitoringApi.Models
{
    public class UserModel
    {
        [MinLength(1)]
        [MaxLength(50)]
        public string? FirstName { get; set; }
        
        [MinLength(1)]
        [MaxLength(50)]
        public string? LastName { get; set; }

        [Phone]
        public string? Phone{ get; set; }
        public string? UserName { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? Password { get; set; }
        
        [Required]
        [Range(0, int.MaxValue)]
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
