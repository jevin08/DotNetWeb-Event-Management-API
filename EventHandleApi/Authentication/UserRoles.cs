namespace EventHandleApi.Authentication
{
    public class UserRoles
    {
        public List<String> Roles = new List<string>();
        public UserRoles() {
            Roles.AddRange(new string[]
            {
                "Admin",
                "Mentor"
            });
        }
    }
}
