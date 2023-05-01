using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EventHandleApi.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string Firstname { get; set; } = null!;
        
        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; } = null!;
        
        [Required(ErrorMessage = "Age is required")]
        public int Age { get; set; }
        
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = null!;
        
        [Required(ErrorMessage = "Mobile number is required")]
        public string MobileNumber { get; set; } = null!;
        
        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; } = null!;
        
        [Required(ErrorMessage = "Level of study is required")]
        public string StudyLevel { get; set; } = null!;
        
        AppUser? mentor { get; set; }

        [JsonPropertyName("events")]
        [BsonElement("events")]
        public List<Event> events { get; set; } = new List<Event>();
    }
}
