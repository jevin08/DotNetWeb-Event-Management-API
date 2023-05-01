using System.IdentityModel.Tokens.Jwt;
using System.Text;
using EventHandleApi.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EventHandleApi.Services
{
    public class AppUserService
    {
        private readonly IMongoCollection<AppUser> _appUsersCollection;

        private readonly string key = null!;

        public AppUserService(IOptions<EventHandleSettings> eventHandleSettings)
        {
            var mongoClient = new MongoClient(
                eventHandleSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                eventHandleSettings.Value.DatabaseName);

            _appUsersCollection = mongoDatabase.GetCollection<AppUser>(
                eventHandleSettings.Value.AppUsersCollectionName);

            key = eventHandleSettings.Value.JwtKey;
        }

        public async Task<List<AppUser>> GetAsync() =>
            await _appUsersCollection.Find(_ => true).ToListAsync();

        public async Task<AppUser?> FindByEmail(string email) =>
            await _appUsersCollection.Find(x => x.Email == email).FirstOrDefaultAsync();

        public async Task<AppUser?> GetAsync(string id) =>
            await _appUsersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(AppUser newAppUser) =>
            await _appUsersCollection.InsertOneAsync(newAppUser);

        public async Task UpdateAsync(string id, AppUser updatedAppUser) =>
            await _appUsersCollection.ReplaceOneAsync(x => x.Id == id, updatedAppUser);

        public async Task RemoveAsync(string id) =>
            await _appUsersCollection.DeleteOneAsync(x => x.Id == id);

        public async Task<string?> Authenticate(string email, string password)
        {
            AppUser appUser = _appUsersCollection.Find(x => x.Email == email && x.Password == password).FirstOrDefault();
            if (appUser == null)
                return null;
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenKey = Encoding.ASCII.GetBytes(key);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, email),
                }),
                Expires= DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
