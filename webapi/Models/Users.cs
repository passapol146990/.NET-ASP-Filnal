using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models
{
    public class Users
    {
        [Key]
        public int Id { get; set; }

        [Column("name")]
        [DisplayName("Input name")]
        public string? name { get; set; }

        [Column("email")]
        [DisplayName("Input email")]
        public string? email { get; set; }

        [Column("password")]
        [DisplayName("Input password")]
        public string? password { get; set; }

        [Column("imageUrl")]
        public string? imageUrl { get; set; }

        [NotMapped]
        [DisplayName("Upload Image")]
        public IFormFile? ImageFile { get; set; }

        // Properties with Pascal case for easier access in code
        [NotMapped]
        public string? Name
        {
            get => name;
            set => name = value;
        }

        [NotMapped]
        public string? Email
        {
            get => email;
            set => email = value;
        }

        [NotMapped]
        public string? Password
        {
            get => password;
            set => password = value;
        }
    }
}
