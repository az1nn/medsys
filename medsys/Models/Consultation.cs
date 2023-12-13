using System.ComponentModel.DataAnnotations.Schema;

namespace medsys.Models
{
    [Table("consults")]
    public class Consultation
    {
        public string PatitentId { get; set; }
        public string DoctorId { get; set; }
        public DateTime ConsultationDate { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
