using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniFacebook.Data;
using MiniFacebook.Models.Entities;
using MiniFacebook.Models.ViewModels;


namespace MiniFacebook.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        private readonly UserManager<User> userManager;
        private readonly UserManager<User> userSearchManager;
        private readonly RoleManager<Role> roleManager;

        //private readonly RoleManager<User> roleManager;
        public readonly UserState userState = new UserState();

        public AdminController(UserManager<User> userManager, UserManager<User> userSearchManager, RoleManager<Role> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.userSearchManager = userSearchManager;
        }

        //public async Task<List<IdentityUser>> GetUsersAsync()
        //{
        //    using (var context = new ApplicationDbContext())
        //    {
        //        return await context.Users.ToListAsync();
        //    }
        //}

        //[HttpGet]
        //public IActionResult Index()
        //{

        //    return UsersAsync(null);
        //}
        [HttpGet]
        public async Task<IActionResult> UsersAsync(string Usrname)
        {

            ViewData["Roles"] = new SelectList(roleManager.Roles, "Id", "Name");
            if (Usrname == null)
            {
                List<ModifiyUserStateAndState> users = new List<ModifiyUserStateAndState>();
                var x = userManager.Users;
                foreach (var item in x)
                {
                    ModifiyUserStateAndState user = new ModifiyUserStateAndState();

                    user.UserId = item.Id;
                    user.UserName = item.UserName;
                    if (item.UserState == UserState.blocked)
                        user.UserState = false;
                    else
                        user.UserState = true;
                    IList<String> _role = await userManager.GetRolesAsync(item);

                    user.RoleName = _role.FirstOrDefault();
                    users.Add(user);
                }

                return View(users);
            }
            else
            {
                List<ModifiyUserStateAndState> usersList = new List<ModifiyUserStateAndState>();

                var _users = userManager.Users.Where((a) => a.UserName.Contains(Usrname));
                foreach (var item in _users)
                {
                    ModifiyUserStateAndState user = new ModifiyUserStateAndState();

                    user.UserId = item.Id;
                    user.UserName = item.UserName;
                    if (item.UserState == UserState.blocked)
                        user.UserState = false;
                    else
                        user.UserState = true;
                    IList<String> _role = await userManager.GetRolesAsync(item);

                    user.RoleName = _role.FirstOrDefault();
                    usersList.Add(user);

                }

                //foreach (var _user in _users)
                // userSearchManager.UpdateAsync(_user);
                return View(usersList);
            }


            //ViewBag.Role = userRole;
            //ViewBag.userState = userState;

            //  ViewBag.Authority = roleManager.Roles;
        }



        [HttpPost]
        public async Task<IActionResult> updatestatus(ModifiyUserStateAndState adminInput)
        {
            var _user = userManager.Users.FirstOrDefault((a) => a.Id == adminInput.UserId);
            if (adminInput.UserState == true)
                _user.UserState = UserState.blocked;
            else
                _user.UserState = UserState.notblocked;
            IdentityResult x = await userManager.UpdateAsync(_user);
            return Json(_user);
        }




        [HttpPost]
        public async Task<IActionResult> updateRole(ModifiyUserStateAndState adminInput)
        {
            var _user = userManager.Users.FirstOrDefault((a) => a.Id == adminInput.UserId);
            
            IList<String> _role = await userManager.GetRolesAsync(_user);

            await userManager.RemoveFromRolesAsync(_user, _role);
            var findRole = await roleManager.FindByIdAsync(adminInput.RoleName);
            await userManager.AddToRoleAsync(_user, findRole.Name);


            IdentityResult x = await userManager.UpdateAsync(_user);
            return Json(_user);
        }





          [HttpGet]
        public async Task<IActionResult> reLoadTData(string Usrname)
        {

            ViewData["Roles"] = new SelectList(roleManager.Roles, "Id", "Name");
   
                List<ModifiyUserStateAndState> usersList = new List<ModifiyUserStateAndState>();

                var _users = userManager.Users.Where((a) => a.UserName.Contains(Usrname));
                foreach (var item in _users)
                {
                    ModifiyUserStateAndState user = new ModifiyUserStateAndState();

                    user.UserId = item.Id;
                    user.UserName = item.UserName;
                    if (item.UserState == UserState.blocked)
                        user.UserState = false;
                    else
                        user.UserState = true;
                    IList<String> _role = await userManager.GetRolesAsync(item);

                    user.RoleName = _role.FirstOrDefault();
                    usersList.Add(user);

                }
                return View(usersList);
            }






        //[HttpPost]
        //public async Task<IActionResult> UsersAsync(User adminInput)
        //{
        //    //if(adminInput.UserState==)

        //    if (ModelState.IsValid) {
        //        //roleManager.FindByIdAsync(adminInput.Id).Status;
        //        //foreach (var item in adminInput)
        //        //{
        //            var _user = userManager.Users.FirstOrDefault((a) => a.Id == adminInput.Id);
        //            _user.UserState = adminInput.UserState;
        //            _user.UserRole = adminInput.UserRole;
        //            //userManager.UpdateAsync(adminInput);
        //            IdentityResult x = await userManager.UpdateAsync(_user);

        //        //}
        //        return View(userManager);
        //    }
        //    return View();
        //}
        [HttpPost]
        public async Task<IActionResult> UsersAsync(ModifiyUserStateAndState adminInput, string Usrname)
        {
            //if(adminInput.UserState==)



            ViewData["Roles"] = new SelectList(roleManager.Roles, "Id", "Name");

            if (ModelState.IsValid)
            {
                //roleManager.FindByIdAsync(adminInput.Id).Status;
                //foreach (var item in adminInput)
                //{

                var _user = userManager.Users.FirstOrDefault((a) => a.Id == adminInput.UserId);
                if (adminInput.UserState == false)
                    _user.UserState = UserState.blocked;
                else
                    _user.UserState = UserState.notblocked;
                IList<String> _role = await userManager.GetRolesAsync(_user);

                await userManager.RemoveFromRolesAsync(_user, _role);
                var findRole = await roleManager.FindByIdAsync(adminInput.RoleName);
                await userManager.AddToRoleAsync(_user, findRole.Name);


                IdentityResult x = await userManager.UpdateAsync(_user);

                //}
                
            }
            List<ModifiyUserStateAndState> users = new List<ModifiyUserStateAndState>();
            var xuser = userManager.Users;


            foreach (var item in xuser)
            {
                ModifiyUserStateAndState user = new ModifiyUserStateAndState();

                user.UserId = item.Id;
                user.UserName = item.UserName;
                if (item.UserState == UserState.blocked)
                    user.UserState = false;
                else
                    user.UserState = true;
                IList<String> _roles = await userManager.GetRolesAsync(item);

                user.RoleName = _roles.FirstOrDefault();
                users.Add(user);
            }
            return View(users);
        }





        [HttpGet]
        public IActionResult ManageRoles()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ManageRoles(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                // We just need to specify a unique role name to create a new role
                Role _Role = new Role
                {
                    Name = model.RoleName,
                    Description = model.RoleDescription
                };

                // Saves the role in the underlying AspNetRoles table
                var result = await roleManager.CreateAsync(_Role);

                if (result.Succeeded)
                {
                    return Json("success");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }
        public IActionResult ListRoles()
        {
            return Json(roleManager.Roles);
        }

        public async Task<IActionResult> getbyID(string Id)
        {
            Role role = await roleManager.FindByIdAsync(Id);
            if (role != null)
            {
                var model = new CreateRoleViewModel
                {
                    Id = role.Id,
                    RoleName = role.Name,
                    RoleDescription = role.Description
                };
                return Json(model);
            }
            return Json("error");
        }
        [HttpPost]
        public async Task<IActionResult> EditRole(CreateRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                return Json("error");
            }
            else
            {
                role.Name = model.RoleName;
                role.Description = model.RoleDescription;
                // Update the Role using UpdateAsync
                var result = await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return Json("Success");
                }
                return Json("error");
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRole (string ID)
        {
            Role role = await roleManager.FindByIdAsync(ID);
            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return Json("Success");
                else
                    return Json("Error");
            }
            return Json("Error");

        }
        [HttpGet]
        public IActionResult AddUser()
        {

            ViewData["Roles"] = new SelectList(roleManager.Roles, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(AdminCreateUserModel model)
        {
            User user;
            if (ModelState.IsValid)
            {
                user = new User
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var role = await roleManager.FindByIdAsync(model.RoleName);
                    if (role == null)
                    {
                        ViewBag.ErrorMessage = $"Role with Id = {model.RoleName} cannot be found";
                        return View("NotFound");
                    }
                     result = await userManager.AddToRoleAsync(user, role.Name);
                    if (result.Succeeded)
                        return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }
            return View();
        }
    }
}