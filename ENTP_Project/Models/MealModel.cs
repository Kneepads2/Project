using System.Numerics;

namespace ENTP_Project.Models
{
    public class MealModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? ImageUrl { get; set; }
        public string? Diet { get; set; }
        public string? Description { get; set; }
        public string? Plan { get; set; }
    }
}
