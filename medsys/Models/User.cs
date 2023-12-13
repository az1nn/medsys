using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace medsys.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("id")]
        public string Id { get; set; }

        [Column("name")]
        public string FullName { get; set; }

        [Column("email")]
        public string LoginEmail { get; set; }

        [Column("hashpassword")]
        public string HashedPassword { get; set; }

        [Column("isDoctor")]
        public bool IsDoctor { get; set; }

    }
}
