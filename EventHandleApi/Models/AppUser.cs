using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace EventHandleApi.Models
{
    public class AppUser
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string Firstname { get; set; } = null!;
        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; } = null!;
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = null!;
        [Required(ErrorMessage = "Age is required")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Mobile number is required")]
        public string MobileNumber { get; set; } = string.Empty;
        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; } = string.Empty;
        [Required(ErrorMessage = "Level of study is required")]
        public string StudyLevel { get; set; } = string.Empty;

        public string Role { get; set; } = "Mentor";

        List<User> fellow = new List<User>();
    }
}
