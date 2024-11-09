using System.Numerics;

namespace ENTP_Project.Models
{
    public class RegistrationModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Role { get; set; }
        public string? Diet { get; set; }
        public string? Plan { get; set; }
        public int? Weight { get; set; }
    }
}
