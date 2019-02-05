using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UnitTestDemo.Models
{
    public class UserRolesViewModel
    {
        [Required]
        public string Id { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
    }

    public class UserList
    {
        ApplicationUserManager UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(ApplicationDbContext.Create()));

        public UserList()
        {

        }

        public UserList(ApplicationUser user)
        {
            Id = user.Id;
            UserName = user.UserName;
            Email = user.Email;
            PhoneNumber = user.PhoneNumber;
            RoleList = UserManager.GetRoles(user.Id);
        }

        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public IList<string> RoleList { get; set; }
        public string Roles => string.Join(", ", RoleList);

    }
}