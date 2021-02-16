//using LibraryManagement.Model;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace LibraryManagement.Repositories
//{
//    public interface IAuthRepository
//    {
//        Task Register(IdentityUser user, string password);
//        //Task Login(IdentityUser user);
//        Task<IdentityUser> CheckUser(string username, string password);
//        Task <IdentityUser>UserExist(string username);
//        Task<IdentityUser> GetRole(IdentityUser user);
//        Task<IdentityRole> RoleExistAdmin(string admin);
//        Task<IdentityRole> RoleExistUser(string user);
//    }

//    public class AuthRepository : IAuthRepository
//    {
//        public UserManager<IdentityUser> _userManager;
//        //public SignInManager<IdentityUser> _signInManager;
//        public  IConfiguration _configuration;
//        public  RoleManager<IdentityRole> _roleManager;

//        public AuthRepository(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
//        {
//            _userManager = userManager;
//            //_signInManager = signInManager;
//            _configuration = configuration;
//            _roleManager = roleManager;
//        }

//        public async Task <IdentityUser> UserExist(string username)
//        {
//            var user = await _userManager.FindByNameAsync(username);
//            if(user != null)
//                throw new Exception($"The username {username} already exists !!");
//            return user;
//        }
//        public async Task Register(IdentityUser user, string password)
//        {
//            await _userManager.CreateAsync(user, password);
//        }

//        //public async Task Login(IdentityUser user)
//        //{
//        //    await _signInManager.SignInAsync(user, false);
//        //}

//        public async Task<IdentityUser> CheckUser(string username, string password)
//        {
//            var user = await _userManager.FindByNameAsync(username);

//            if (user == null)
//                throw new Exception($"The user with username {username} does not exists.");

//            var result = await _userManager.CheckPasswordAsync(user, password);

//            if (!result)
//                throw new Exception("The Username and Password do not match.");

//            return user;
//        }
//        public async Task<IdentityUser> GetRole(IdentityUser user)
//        {
//            var userRoles = await _userManager.GetRolesAsync(user);
//            return user;
//        }

//        public async Task<IdentityRole> RoleExistAdmin(string Admin)
//        {
//            var RoleExist  = _roleManager.RoleExistsAsync(Admin);
//            if (RoleExist == null)
//            {
//                await _roleManager.CreateAsync(new IdentityRole(Admin));
//            }

//            return RoleExist;
//        }
//        public async Task<IdentityRole> RoleExistUser(string User)
//        {
//            var RoleExist = _roleManager.RoleExistsAsync(User);
//            if (RoleExist == null)
//            {
//                await _roleManager.CreateAsync(new IdentityRole(User));
//            }

//            return RoleExist;
//        }


//    }
//}
