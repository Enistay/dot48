using dot48.Application.Models.ViewModels;
using dot48.Models.Auth;
using System.Web.Mvc;

namespace dot48.UI.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(UserSignInViewModel userSignIn)
        {
            if (!this.ModelState.IsValid || userSignIn == null)
            {
                return this.View(userSignIn);
            }

            if (!AuthManager.Instance.SignIn(userSignIn.UserName, userSignIn.Password))
            {
                ModelState.AddModelError(string.Empty, "User not found");
                return View(userSignIn);
            }

            return RedirectToAction("Index", "User");
        }

        public ActionResult SignOut()
        {
            AuthManager.Instance.SignOut();
            return RedirectToAction("Index", "Home");

        }
    }
}