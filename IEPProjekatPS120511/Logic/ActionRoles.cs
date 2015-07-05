using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IEPProjekatPS120511.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IEPProjekatPS120511.Logic
{
    internal class RoleActions
    {
        internal void AddUserAndRole()
        {
            // Access the application context and create result variables.
            Models.ApplicationDbContext context = new ApplicationDbContext();
            IdentityResult IdRoleResult;
            IdentityResult IdUserResult;

            // Create a RoleStore object by using the ApplicationDbContext object. 
            // The RoleStore is only allowed to contain IdentityRole objects.
            var roleStore = new RoleStore<IdentityRole>(context);

            // Create a RoleManager object that is only allowed to contain IdentityRole objects.
            // When creating the RoleManager object, you pass in (as a parameter) a new RoleStore object. 
            var roleMgr = new RoleManager<IdentityRole>(roleStore);

            // Then, you create the "canEdit" role if it doesn't already exist.
            if (!roleMgr.RoleExists("SuperAdmin"))
            {
                IdRoleResult = roleMgr.Create(new IdentityRole { Name = "SuperAdmin" });
            }

            if (!roleMgr.RoleExists("RegUser"))
            {
                IdRoleResult = roleMgr.Create(new IdentityRole { Name = "RegUser" });
            }

            // Create a UserManager object based on the UserStore object and the ApplicationDbContext  
            // object. Note that you can create new objects and use them as parameters in
            // a single line of code, rather than using multiple lines of code, as you did
            // for the RoleManager object.
            var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            if (userMgr.FindByEmail("AdminAcc@swShop.com") == null)
            {
                var appUser = new ApplicationUser
                {
                    UserName = "AdminSw",
                    Email = "AdminAcc@swShop.com"
                };
                IdUserResult = userMgr.Create(appUser, "Pantelic93.");
            }
            // If the new "canEdit" user was successfully created, 
            // add the "canEdit" user to the "canEdit" role. 
            if (!userMgr.IsInRole(userMgr.FindByEmail("AdminAcc@swShop.com").Id, "SuperAdmin"))
            {
                IdUserResult = userMgr.AddToRole(userMgr.FindByEmail("AdminAcc@swShop.com").Id, "SuperAdmin");
            }
        }

      
    }
}