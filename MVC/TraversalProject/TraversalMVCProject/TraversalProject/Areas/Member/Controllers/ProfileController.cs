﻿using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TraversalProject.Areas.Member.Models;

namespace TraversalProject.Areas.Member.Controllers
{
    [Area("Member")]
    [Route("Member/[controller]/[action]")]
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        public ProfileController(UserManager<AppUser> userManager)
        {
                _userManager = userManager;
        }
        public async Task <IActionResult> Index()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            UserEditViewModel userEditViewModel = new UserEditViewModel();
            userEditViewModel.Name = values.Name;
            userEditViewModel.Surname = values.Surname;
            userEditViewModel.Phonenumber = values.PhoneNumber;
            userEditViewModel.Mail = values.Email;
            return View(userEditViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserEditViewModel p)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (p.Image != null)
            {
                var resource = Directory.GetCurrentDirectory();
                var extension=Path.GetExtension(p.Image.FileName);
                var imagename = Guid.NewGuid() + extension;
                var saveliacation = resource + "/wwwroot/userimages/" + imagename;
                var stream=new FileStream(saveliacation, FileMode.Create);
                await p.Image.CopyToAsync(stream);
                user.ImageUrl = imagename;
            }
              user.Name = p.Name;
              user.Surname=p.Surname;
              user.PasswordHash =_userManager.PasswordHasher.HashPassword(user, p.Password);
              var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded) 
            {
                return RedirectToAction("SignIn", "Login");
            }
            return View();
        }
    }
}
