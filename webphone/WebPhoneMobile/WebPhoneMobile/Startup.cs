using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebPhoneMobile.Models;

[assembly: OwinStartupAttribute(typeof(WebPhoneMobile.Startup))]
namespace WebPhoneMobile
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            InitUserRole();
        }
        private void InitUserRole()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new
            RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new
            UserStore<ApplicationUser>(context));
            try
            {
                // tạo role Admin
                if (!roleManager.RoleExists("Admin"))//chưa có mới tạo
                {
                    var role = new IdentityRole();
                    role.Name = "Admin";
                    roleManager.Create(role);
                    // tạo user
                    var user = new ApplicationUser();
                    user.UserName = "admin@wp.com";
                    user.Email = "admin@wp.com";
                    string pass = "190502";
                    // 123Aa!
                    // sau này login sẽ thay đổi pass
                    var chkUser = userManager.Create(user, pass);
                    // đưa user  vào role Admin
                    if (chkUser.Succeeded)
                        userManager.AddToRole(user.Id, "Admin");
                }
            }
            catch { }
        }
    }
}
