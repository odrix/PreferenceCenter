using PreferenceCenterAPI.Domain;

namespace PreferenceCenterAPI.DAL
{
    public class UserProvider : IUserProvider, IEventProvider
    {
        PreferenceCenterContext ctx = new PreferenceCenterContext();

        public UserPreference GetUser(Guid id) => ctx.Users.SingleOrDefault(u => u.Id == id);

        public UserPreference GetUser(string email) => ctx.Users.SingleOrDefault(u => u.Email == email);

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

        public int AddEvents(Guid userId, Consent[] consents)
        {
            ctx.Events.AddRange(consents.Select(c => new Event()
            {
                Created = DateTime.Now,
                Enabled = c.Enabled,
                Id = c.Id,
                UserId = userId,
            }));
            return ctx.SaveChanges();
        }

        public bool GetLastConsentOf(Guid userId, EnumConsent consent)
        {
            return (from x in ctx.Events
                    where x.UserId == userId && x.Id == consent
                    orderby x.Created descending
                    select x.Enabled).FirstOrDefault();
        }

        public bool CheckUserExist(Guid id) => ctx.Users.Any(x => x.Id == id);
    }
}
