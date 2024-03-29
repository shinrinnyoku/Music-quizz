using System.Collections.Generic;
using System.Linq;
using MusicQuizAPI.Models.Database;

namespace MusicQuizAPI.Database
{
    public class UserRepository
    {
        private MusicQuizDbContext Db { get; set; }
        
        public UserRepository(MusicQuizDbContext db)
        {
            Db = db;
        }

        public bool ExistWithEmail(string email) 
            => Db.Users.Any(u => u.Email == email);

        public bool ExistWithUsername(string username) 
            => Db.Users.Any(u => u.Username == username);
        
        public int Add(User user) 
        {
            Db.Users.Add(user);
            return Db.SaveChanges();
        }

        public int Update(User user)
        {
            Db.Users.Update(user);
            return Db.SaveChanges();
        }

        public User GetByUsername(string username) 
            => Db.Users.FirstOrDefault(u => u.Username == username);

        public User GetByEmail(string email) 
            => Db.Users.FirstOrDefault(u => u.Email == email);

        public User Get(int id) 
            => Db.Users.FirstOrDefault(u => u.UserID == id);

        public List<User> GetAllThatContainsName(string name) 
            => Db.Users.Where(u => u.Username.ToLower().Contains(name)).ToList();
    }
}