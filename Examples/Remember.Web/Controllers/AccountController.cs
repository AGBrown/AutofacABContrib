using System.Web;
using System.Web.Mvc;
using Remember.Web.Models;
using Remember.Web.Service;

namespace Remember.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginForm model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("LoginSuccess");
            }

            
            return View(model);
        }

        
        public ActionResult LoginSuccess()
        {
            return View();
        }
    }
}