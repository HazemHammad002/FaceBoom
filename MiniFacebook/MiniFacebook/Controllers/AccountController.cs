using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiniFacebook.Models.Entities;
using MiniFacebook.Models.ViewModels;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using MiniFacebook.Data;
using Microsoft.AspNetCore.Http;

namespace MiniFacebook.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        ApplicationDbContext db;

        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager , ApplicationDbContext _db)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            db = _db;

        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            User user; 
            if (ModelState.IsValid)
            {
                if (model.Gender == 0)
                {
                    user = new User
                    {
                        FirstName = model.FistName,
                        LastName=model.LastName,
                        UserName = model.Email,
                        Email = model.Email,
                        BirthDate = new DateTime(model.Year, model.Month, model.Day),
                        Gender = model.Gender,
                        ProfilePic = "Male_avater.jpg"
                    };
                }
                else
                {
                     user = new User
                    {
                        UserName = model.Email,
                        Email = model.Email,
                        BirthDate = new DateTime(model.Year, model.Month, model.Day),
                        Gender = model.Gender,
                        ProfilePic = "Female_avater.jpg"
                    };
                }
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = token }, Request.Scheme);
                    //Sending Comfirmation Mail To the User
                    SendingMail.Send(confirmationLink, user.Email);

                    ViewBag.WarningTitle = "Registration Successful";
                    ViewBag.WarningMessage = "Before you can Login, please confirm your email, by clicking on the confirmation link we have emailed you";
                    return View("SuccessfulRegistration"); 
                }
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "home");
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl)
        {
            LoginViewModel model = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                Externallogins=(await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            model.Externallogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if(user != null && !user.EmailConfirmed &&(await userManager.CheckPasswordAsync(user,model.Password)))
                {
                    ModelState.AddModelError(string.Empty, "Email not confirmed yet!! ");
                        return View(model);
                }
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    var id = db.Users.Where(u => u.Email == user.Email).Select(u => u.Id).ToList()[0];
                    HttpContext.Session.SetString("ID", id);

                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);
                    else
                        return RedirectToAction("index", "home");

                }   
                ModelState.AddModelError("", "Invalid Login Attempt");
            }
            return View(model);
        }
        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return Json(true);
            else
                return Json($"Email {email} is already in use");
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult ExternalLogin(string provider,string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });
            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            LoginViewModel loginViewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                Externallogins =
                        (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            if (remoteError != null)
            {
                ModelState
                    .AddModelError(string.Empty, $"Error from external provider: {remoteError}");

                return View("Login", loginViewModel);
            }

            // Get the login information about the user from the external login provider
            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState
                    .AddModelError(string.Empty, "Error loading external login information.");
                    
                return View("Login", loginViewModel);
            }
            // Get the email claim value
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            User user = null;
            if(user != null && !user.EmailConfirmed)
            {
                ModelState.AddModelError(string.Empty, "Email not confirmed yet!!");
                return View("Login", loginViewModel);
            }
            // If the user already has a login (i.e if there is a record in AspNetUserLogins
            // table) then sign-in the user with this external login provider
            var signInResult = await signInManager.ExternalLoginSignInAsync(info.LoginProvider,
                info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

            if (signInResult.Succeeded)
            {
                
                return LocalRedirect(returnUrl);
            }
            // If there is no record in AspNetUserLogins table, the user may not have
            // a local account
            else
            {

                if (email != null)
                {
                    // Create a new user without password if we do not have a user already
                    if (user == null)
                    {
                        user = new User
                        {
                            UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                            BirthDate = Convert.ToDateTime(info.Principal.FindFirstValue(ClaimTypes.DateOfBirth)),
                        };
                        
                        await userManager.CreateAsync(user);
                        var id = db.Users.Where(u => u.Email == user.Email).Select(u => u.Id).ToList()[0];
                        HttpContext.Session.SetString("ID", id);
                        user.EmailConfirmed = true;
                        //var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                        //var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = token }, Request.Scheme);
                        ////Sending Comfirmation Mail To the User
                        //SendingMail.Send(confirmationLink, user.Email);

                        //ViewBag.WarningTitle = "Registration Successful";
                        //ViewBag.WarningMessage = "Before you can Login, please confirm your email, by clicking on the confirmation link we have emailed you";
                        //return View("SuccessfulRegistration");
                    }

                    // Add a login (i.e insert a row for the user in AspNetUserLogins table)
                    await userManager.AddLoginAsync(user, info);
                    await signInManager.SignInAsync(user, isPersistent: false);

                    return LocalRedirect(returnUrl);
                }

                // If we cannot find the user email we cannot continue
                ViewBag.ErrorTitle = $"Email claim not received from: {info.LoginProvider}";
                ViewBag.ErrorMessage = "Please contact support on facebookcontactapp@gmail.com";

                return View("Error");
            }
        }
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("index", "home");
            }

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"The User ID {userId} is invalid";
                return View("NotFound");
            }

            var result = await userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return View();
            }

            ViewBag.ErrorTitle = "Email cannot be confirmed";
            return View("Error");
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}