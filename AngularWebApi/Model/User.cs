using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularWebApi.Model
{
    public class User
    {
        public int UserId { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string UserName { get; set; }

        [Column(TypeName = "varchar(300)")]
        public string EmailId { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string? Password { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string? PhoneNumber { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string? Address { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string? RefreshToken { get; set; }
    }
}
