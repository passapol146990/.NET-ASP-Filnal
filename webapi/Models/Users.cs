using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models
{
    public class Users
    {
        public int? Id { get; set; }
        [DisplayName("Input name")]
        public string? Name { get; set; }
        [DisplayName("Input email")]
        public string? Email { get; set; }
        [DisplayName("Input password")]
        public string? Password { get; set; }
        public string? imageUrl { get; set; }
        [NotMapped]
        [DisplayName("Upload Image")]
        public IFormFile? ImageFile { get; set; }
    }
}
