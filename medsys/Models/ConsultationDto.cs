namespace medsys.Models
{
    public class ConsultationDto
    {
        public class AddConsultationDto
        {
            public string DoctorId { get; set; } = string.Empty;    
            public string PatitentId { get; set; } = string.Empty;
            public DateTime ConsultationDate { get; set; }
            public string ?AdditionalInfo { get; set; }
        }
    }
}
