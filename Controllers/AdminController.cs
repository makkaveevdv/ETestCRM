using ETestCRM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ETestCRM.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private UserManager<AppUser> userManager;
        private IUserValidator<AppUser> userValidator;
        private IPasswordValidator<AppUser> passwordValidator;
        private IPasswordHasher<AppUser> passwordHasher;
        public AdminController(UserManager<AppUser> usrMgr, IUserValidator<AppUser> userValid, 
            IPasswordValidator<AppUser> passwordValid, IPasswordHasher<AppUser> passwordHash)
        {
            userManager = usrMgr;
            userValidator = userValid;
            passwordValidator = passwordValid;
            passwordHasher = passwordHash;
        }
        [Authorize(Roles = "Superadmin")]
        public ViewResult Index() => View(userManager.Users);

        [Authorize(Roles = "Superadmin")]
        public ViewResult Create()
        {
            ViewBag.AllUsers = new SelectList(userManager.Users.ToList(), "Id");
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Superadmin")]
        public async Task<IActionResult> Create(CreateModel model)
        {
            if(ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    UserName = model.Name,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    ManagerId = model.Manager != null ? (userManager.Users.FirstOrDefault(c => c.UserName == model.Manager)).Id : null,
                    ManagerFullName = model.Manager != null ? (userManager.Users.FirstOrDefault(c => c.UserName == model.Manager)).FullName : null
                };
                IdentityResult result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                } else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Superadmin")]
        public async Task<IActionResult> Delete(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                } else
                {
                    AddErrorsFromResult(result);
                }
            } else
            {
                ModelState.AddModelError("", "Пользователь не найден");
            }
            return View("Index", userManager.Users);
        }

        [Authorize(Roles = "Superadmin")]
        public async Task<IActionResult> Edit(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null) 
            {
                ViewBag.AllUsers = new SelectList(userManager.Users.ToList(), "Id");
                return View(user); 
            } else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Superadmin")]
        public async Task<IActionResult> Edit(string id, string firstname, string middlename, string lastname, string manager, string email, string password)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.FirstName = firstname;
                user.MiddleName = middlename;
                user.LastName = lastname;
                user.Email = email;
                if (manager != null)
                {
                    user.ManagerId = (userManager.Users.FirstOrDefault(c => c.UserName == manager)).Id;
                    user.ManagerFullName = (userManager.Users.FirstOrDefault(c => c.UserName == manager)).FullName;
                }

                IdentityResult validEmail = await userValidator.ValidateAsync(userManager, user);
                if (!validEmail.Succeeded)
                {
                    AddErrorsFromResult(validEmail);
                }
                IdentityResult validPass = null;
                if (!string.IsNullOrEmpty(password))
                {
                    validPass = await passwordValidator.ValidateAsync(userManager, user, password);
                } else
                {
                    return View(user);
                }
                if ((validEmail.Succeeded && validPass == null) || (validEmail.Succeeded && password != string.Empty && validPass.Succeeded))
                {
                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        AddErrorsFromResult(result);
                    }
                }
            } else
            {
                ModelState.AddModelError("", "User Not Found");
            }
            return View(user);
        }
        
        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}
