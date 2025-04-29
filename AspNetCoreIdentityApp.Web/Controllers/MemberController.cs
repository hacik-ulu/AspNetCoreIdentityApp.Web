using AspNetCoreIdentityApp.Web.Extensions;
using AspNetCoreIdentityApp.Web.Models.Identity;
using AspNetCoreIdentityApp.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreIdentityApp.Web.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public MemberController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity!.Name!);

            var userViewModel = new UserInfoViewModel()
            {
                Email = currentUser!.Email!,
                UserName = currentUser.UserName,
                PhoneNumber = currentUser.PhoneNumber
            };

            return View(userViewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Default");
        }

        public IActionResult PasswordChange()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PasswordChange(PasswordChangeViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var currentUser = await _userManager.FindByNameAsync(User.Identity!.Name!);

            // Eski password kontrolü
            var checkOldPassword = await _userManager.CheckPasswordAsync(currentUser, request.PasswordOld);

            if (!checkOldPassword)
            {
                ModelState.AddModelError(string.Empty, "Eski şifreniz yanlış");
                return View();
            }

            var resultChangePassword = await _userManager.ChangePasswordAsync(currentUser, request.PasswordOld, request.PasswordNew);

            if (!resultChangePassword.Succeeded)
            {

                ModelState.AddModelErrorList(resultChangePassword.Errors.Select(x => x.Description).ToList());
            }


            // Şifre değiştiğinden security stamp değerini güncelleyerek başka bir sekmede de açık kalma ihtimalinden ötürü bilgileri güncelliyoruz.
            await _userManager.UpdateSecurityStampAsync(currentUser);

            // Şifre değiştiğinden signout yapıp sonra tekrardan signIn yapacağız.
            await _signInManager.SignOutAsync();
            await _signInManager.PasswordSignInAsync(currentUser, request.PasswordNew, true, false);

            TempData["SuccessMessage"] = "Şifreniz başarıyla gerçekleşmiştir.";

            return View();
        }
    }
}
