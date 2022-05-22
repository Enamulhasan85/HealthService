using HealthService.cs;
using HealthService.DataAccess;
using Microsoft.Owin;
using Owin;
using System.Linq;

[assembly: OwinStartupAttribute(typeof(HealthService.Startup))]
namespace HealthService
{
    public partial class Startup
    {
        private HealthServiceContext db = new HealthServiceContext();

        public void Configuration(IAppBuilder app)
        {
            var Role1 = db.Roles.FirstOrDefault(r => r.RoleName == "systemadmin");
            if(Role1 == null) 
            {
                Role role = new Role()
                {
                    RoleName = "systemadmin",
                };

                db.Roles.Add(role);
                db.SaveChanges();
            }

            var Role2 = db.Roles.FirstOrDefault(r => r.RoleName == "admin");
            if (Role2 == null)
            {
                Role role = new Role()
                {
                    RoleName = "admin",
                };

                db.Roles.Add(role);
                db.SaveChanges();
            }

            var Role3 = db.Roles.FirstOrDefault(r => r.RoleName == "upazilla user");
            if (Role3 == null)
            {
                Role role = new Role()
                {
                    RoleName = "upazilla user",
                };

                db.Roles.Add(role);
                db.SaveChanges();
            }

            var user1 = db.Users.FirstOrDefault(r => r.Username == "systemadmin");
            if(user1 == null)
            {
                var user = new User()
                {
                    Username = "systemadmin",
                    FirstName = "Enam",
                    LastName = "Hasan",
                    Email = "sysenam@gmail.com",
                    Password = "123",
                };
                user.IsActive = true;
                user.Roles = db.Roles.Where(r => r.RoleName == "systemadmin").ToList();
                db.Users.Add(user);
                db.SaveChanges();
            }
            
        }
    }
}
