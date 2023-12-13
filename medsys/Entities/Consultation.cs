using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace medsys.Entities
{
    [Table("consultations")]
    public class Consultation
    {
        [ForeignKey("User")]
        public string PatitentId { get; set; }
        [ForeignKey("User")]
        public string DoctorId { get; set; }
        [Key]
        public string ConsultationId { get; set; }
        public DateTime ConsultationDate { get; set; }
        public DateTime CreationDate { get; set; }
        public string additionalInfo { get; set; }


    }
}
