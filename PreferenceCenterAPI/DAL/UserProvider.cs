using PreferenceCenterAPI.Domain;

namespace PreferenceCenterAPI.DAL
{
    public class UserProvider : IUserProvider
    {
        PreferenceCenterContext ctx = new PreferenceCenterContext();
        public void Add(UserPreference newUser)
        {
            ctx.Users.Add(newUser);
            ctx.SaveChanges();
        }

        public int Delete(Guid id) 
        {
            var user = Get(id);
            ctx.Users.Remove(user);
            return ctx.SaveChanges();
        }

        public int Delete(string email)
        {
            var user = Get(email);
            ctx.Users.Remove(user);
            return ctx.SaveChanges();
        }

        public UserPreference Get(Guid id) => ctx.Users.SingleOrDefault(u => u.Id == id);

        public UserPreference Get(string email) => ctx.Users.SingleOrDefault(u => u.Email == email);
    }
}
