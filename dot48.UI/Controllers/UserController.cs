using dot48.Application.Interfaces;
using dot48.Application.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace dot48.UI.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        public UserController(IUserService _userService)
        {
            userService = _userService;
        }

        // GET: User
        public ActionResult Index()
        {
            return View(userService.GetUsers().ToList());
        }

        public JsonResult getUser()
        {
            return Json(userService.GetUsers().ToList(), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = RolesApplication.ADMIN_OR_COORDENADOR)]
        public ActionResult Add()
        {
            ViewData["IdProfile"] = new SelectList(userService.GetProfiles(), "IdProfile", "CodeProfile");
            return View();
        }

        [Authorize(Roles = RolesApplication.ADMIN_OR_COORDENADOR)]
        [HttpPost]
        public ActionResult Add(UserViewModel userAdd)
        {           
            this.ModelState["IdUser"].Errors.Clear();
            this.ModelState["IdUserSetting"].Errors.Clear();

            if (!this.ModelState.IsValid)
            {
                ViewData["IdProfile"] = new SelectList(userService.GetProfiles(), "IdProfile", "CodeProfile");

                return View(userAdd);   
            }

            userService.SaveUser(userAdd);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = RolesApplication.ADMIN_OR_COORDENADOR)]
        public ActionResult Edit(int idUser)
        {
            var user = userService.GetUserByIdUser(idUser);

            ViewData["IdProfile"] = new SelectList(userService.GetProfiles(), "IdProfile", "CodeProfile", user.IdProfile);
            return View(user);
        }

        [Authorize(Roles = RolesApplication.ADMIN_OR_COORDENADOR)]
        [HttpPost]
        public ActionResult Edit(UserViewModel userEdit)
        {
            this.ModelState["Password"].Errors.Clear();
            if (!this.ModelState.IsValid)
            {
                ViewData["IdProfile"] = new SelectList(userService.GetProfiles(), "IdProfile", "CodeProfile", userEdit.IdProfile);

                return View(userEdit);
            }

            userService.SaveUser(userEdit);

            return RedirectToAction("Index");
        }

    }
}