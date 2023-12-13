using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace medsys.Entities
{
    [Table("users")]
    public class User
    {
        [Key]
        public string Id { get; set; }
        public string FullName { get; set; }
        public string LoginEmail { get; set; }

        [JsonIgnore]
        public string HashedPassword { get; set; }
        public bool IsDoctor { get; set; }

    }
}
