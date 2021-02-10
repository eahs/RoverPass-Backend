using ADSBackend.Data;
using ADSBackend.Helpers;
using ADSBackend.Models;
using ADSBackend.Models.AdminViewModels;
using ADSBackend.Models.ApiModels;
using ADSBackend.Models.AuthenticationModels;
using ADSBackend.Models.Identity;
using ADSBackend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks; 
namespace ADSBackend.Controllers.Api.v1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/v1/users")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IEmailSender _emailSender;

        public UsersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IUserService userService, RoleManager<ApplicationRole> roleManager, IEmailSender emailSender)
        {
            _context = context;
            _userService = userService;
            _userManager = userManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ApiResponse> Authenticate(AuthenticateRequest model)
        {
            var member = await _userService.Authenticate(model);

            if (member == null)
                return new ApiResponse(System.Net.HttpStatusCode.NotFound, model, "Email or password is incorrect");

            return new ApiResponse(System.Net.HttpStatusCode.OK, member);
        }

        // GET: api/v1/Members/{id}
        /// <summary>
        /// Returns a specific member
        /// </summary>
        /// <param name="id"></param>    
        [HttpGet("{id}")]
        public async Task<ApiResponse> GetUser(int id)
        {
            // TODO: Add validation for an id
            var httpUser = (ApplicationUser)HttpContext.Items["User"];

            var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);

            // TODO: Strip all data that isn't supposed to be public from this api response
            var response = new
            {
                UserId = user.Id,
                user.FirstName,
                user.LastName
            };

            return new ApiResponse(System.Net.HttpStatusCode.OK, response);  
        }

        private const string CreateMemberBindingFields = "FirstName,LastName,Email,Password";
        private const string UpdateMemberBindingFields = "FirstName,LastName,Email";

        // POST: api/v1/users/
        /// <summary>
        /// Create a new member
        /// </summary>
        /// <param name="user"></param>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ApiResponse> CreateMember ([Bind (CreateMemberBindingFields)]UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                // Return all validation errors
                return new ApiResponse(System.Net.HttpStatusCode.BadRequest, null, "An error has occurred", ModelState);
            }

            var _user = new ApplicationUser
            {
                UserName = user.Email,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            _user.PasswordHash = _userManager.PasswordHasher.HashPassword(_user, user.Password);

            // create user
            await _userManager.CreateAsync(_user);

            // assign new role
            await _userManager.AddToRoleAsync(_user, "Student");

            // send confirmation email
            var confirmationCode = await _userManager.GenerateEmailConfirmationTokenAsync(_user);
            var confirmationLink = Url.EmailConfirmationLink(_user.Id, confirmationCode, Request.Scheme);
            await _emailSender.SendEmailConfirmationAsync(_user.Email, confirmationLink);

            var response = new
            {
                UserId = _user.Id,
                user.FirstName,
                user.LastName,
                user.Email
            };

            return new ApiResponse(System.Net.HttpStatusCode.OK, response);
        }

        // PUT: api/v1/users/
        /// <summary>
        /// Update an existing member
        /// </summary>
        /// <param name="user"></param>   
        [HttpPut]
        public async Task<ApiResponse> UpdateUser([Bind(UpdateMemberBindingFields)]ApplicationUser user)
        {
            var httpUser = (ApplicationUser) HttpContext.Items["User"];
            var newUser = await _context.Users.FirstOrDefaultAsync(m => m.Id == httpUser.Id);
            
            if (newUser == null)
            {
                return new ApiResponse(System.Net.HttpStatusCode.NotFound, null, "User not found");
            }
            
            newUser.FirstName = user.FirstName ?? newUser.FirstName;
            newUser.LastName = user.LastName ?? newUser.LastName;

            TryValidateModel(newUser);
            ModelState.Scrub(UpdateMemberBindingFields);  // Remove all errors that aren't related to the binding fields

            // Add custom errors to fields
            //ModelState.AddModelError("Email", "Something else with email is wrong");
          
            if (!ModelState.IsValid)
            {
                // Return all validation errors
                return new ApiResponse(System.Net.HttpStatusCode.BadRequest, null, "An error has occurred", ModelState);
            }

            _context.Users.Update(newUser);
            await _context.SaveChangesAsync();

            var response = new
            {
                UserId = newUser.Id,
                user.FirstName,
                user.LastName,
                newUser.Email
            };

            return new ApiResponse(System.Net.HttpStatusCode.OK, response);
            
        }

        

    }
}