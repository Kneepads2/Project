using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace ENTP_Project.Models
{
    public class WorkoutModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? ImageUrl { get; set; }
        public string? Difficulty { get; set; }
        public string? Description { get; set; }
        public string? Instructor { get; set; }
        public string? Plan { get; set; }
        public int UserId { get; set; }  // Foreign Key

        [ForeignKey("UserId")]
        public UserModel Creator { get; set; }  // Navigation property
    }
}
