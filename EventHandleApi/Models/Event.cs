using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EventHandleApi.Models
{
    public class Event
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [JsonPropertyName("Name")]
        [BsonElement("Name")]
        [Required(ErrorMessage = "Event name is required")]
        public string EventName { get; set; } = null!;

        [JsonPropertyName("Description")]
        [BsonElement("Description")]
        public string EventDescription { get; set; } = null!;


        [JsonPropertyName("StartTime")]
        [BsonElement("StartTime")]
        [Required(ErrorMessage = "Start time is required")]
        public DateTime EventStartTime { get; set; }

        [Required(ErrorMessage = "Ending time is required")]
        [JsonPropertyName("EndTime")]
        [BsonElement("EndTime")]
        public DateTime EventEndTime { get; set; }

        [Required(ErrorMessage = "Address time is required")]
        [JsonPropertyName("Venue")]
        [BsonElement("Venue")]
        public string EventVenue { get; set; } = null!;

        [Required(ErrorMessage = "Speaker is not mentioned")]
        [JsonPropertyName("Speaker")]
        [BsonElement("Speaker")]
        public User EventSpeaker { get; set; } = null!;

        [JsonPropertyName("Fees")]
        [BsonElement("Fees")]
        public int EventFees { get; set; }

        [JsonPropertyName("users")]
        [BsonElement("Users")]
        public List<User> users { get; set; } = new List<User>();
    }
}
