﻿using ADSBackend.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;

namespace ADSBackend.Configuration
{
    public class ApplicationUserSeed
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUserSeed(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public void CreateAdminUser()
        {
            if (_userManager.FindByNameAsync("admin@roverpass.me").Result != null)
            {
                return;
            }

            var adminUser = new ApplicationUser
            {
                UserName = "admin@roverpass.me",
                Email = "admin@roverpass.me",
                FirstName = "Admin"
            };

            IdentityResult result;
            try
            {
                result = _userManager.CreateAsync(adminUser, "Password123!").Result;
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while creating the admin user: " + e.InnerException);
            }

            if (!result.Succeeded)
            {
                throw new Exception("The following error(s) occurred while creating the admin user: " + string.Join(" ", result.Errors.Select(e => e.Description)));
            }

            _userManager.AddToRoleAsync(adminUser, "Admin").Wait();
        }
    }
}
