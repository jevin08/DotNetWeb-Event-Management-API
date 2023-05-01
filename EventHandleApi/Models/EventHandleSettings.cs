using Microsoft.Extensions.Configuration;

namespace EventHandleApi.Models
{
    public class EventHandleSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string EventsCollectionName { get; set; } = null!;
        
        public string UsersCollectionName { get; set; } = null!;
        
        public string AppUsersCollectionName { get; set; } = null!;

        public string JwtKey { get; set; } = null!;
    }
}
