namespace medsys.Models
{
    public class UserDTO
    {
        public string FullName { get; set; }

        public string LoginEmail { get; set; }
        public string Password { get; set; }
        public bool IsDoctor { get; set; }
    }
}