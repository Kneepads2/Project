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
    }
}
