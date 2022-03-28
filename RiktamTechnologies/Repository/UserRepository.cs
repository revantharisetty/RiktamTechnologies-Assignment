using Microsoft.EntityFrameworkCore;
using RiktamTechnologies.Models;
using RiktamTechnologies.Repository.Interfaces;

namespace RiktamTechnologies.Repository
{
    public class UserRepository: IUserRepository
    {
        RiktamContext db;
        public UserRepository(RiktamContext _db)
        {
            db = _db;
        }

        public async Task<List<User>> GetUsers()
        {
            if (db != null)
            {
                return await db.Users.ToListAsync();
            }

            return null;
        }

        public async Task<User> GetUser(int? userId)
        {
            if (db != null)
            {
                return await (from u in db.Users
                              where u.UserId == userId

                              select new User
                              {
                                  UserId = u.UserId,
                                  UserName = u.UserName,
                                  FirstName = u.FirstName,
                                  LastName = u.LastName,
                                  MobileNumber = u.MobileNumber,
                                  UserPassword = u.UserPassword
                              }).FirstOrDefaultAsync();
            }

            return null;
        }

        public async Task<bool> AuthenticateUser(string userName, string password)
        {
            if (db != null)
            {
                var user = await db.Users.FirstOrDefaultAsync(x => x.UserName == userName && x.UserPassword==password);
                if(user != null)
                    return true;
            }

            return false;
        }

        public async Task<int> AddUser(User user)
        {
            if (db != null)
            {
                await db.Users.AddAsync(user);
                await db.SaveChangesAsync();
                return user.UserId;
            }

            return 0;
        }

        public async Task<int> DeleteUser(int? userId)
        {
            int result = 0;

            if (db != null)
            {
                var user = await db.Users.FirstOrDefaultAsync(x => x.UserId == userId);

                if (user != null)
                {
                    db.Users.Remove(user);
                    result = await db.SaveChangesAsync();
                }
                return result;
            }

            return result;
        }


        public async Task UpdateUser(User user)
        {
            if (db != null)
            {
                db.Users.Update(user);
                await db.SaveChangesAsync();
            }
        }
    }
}
