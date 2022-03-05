using PreferenceCenterAPI.Domain;

namespace PreferenceCenterAPI.DAL
{
    public class UserProvider : IUserProvider, IEventProvider
    {
        PreferenceCenterContext ctx = new PreferenceCenterContext();

        public UserPreference GetUser(Guid id) => ctx.Users
                                                        .Include("Consents")
                                                        .SingleOrDefault(u => u.Id == id);

        public UserPreference GetUser(string email) => ctx.Users
                                                        .Include("Consents")
                                                        .SingleOrDefault(u => u.Email == email);

        public void AddUser(UserPreference newUser)
        {
            ctx.Users.Add(newUser);
            ctx.SaveChanges();
        }

        public int DeleteUser(Guid id) 
        {
            var user = GetUser(id);
            ctx.Users.Remove(user);
            return ctx.SaveChanges();
        }

        public int DeleteUser(string email)
        {
            var user = GetUser(email);
            ctx.Users.Remove(user);
            return ctx.SaveChanges();
        }

        public int AddEvents(Consent[] consents)
        {
            ctx.Consents.AddRange(consents);
            return ctx.SaveChanges();
        }

        public bool CheckUserExist(Guid id) => ctx.Users.Any(x => x.Id == id);
    }
}
