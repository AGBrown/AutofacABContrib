using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Remember.Web.Models;
using Remember.Web.Service;

namespace Remember.Web.Controllers
{
    [CustomAuthorize]
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
                //  This simple sample uses the built in System.Web.Security.FormsAuthentication 
                //      rather than simplemembership or Identity
                //  Sample page flow:
                //      - Account/LoginSuccess (which is protected by CustomAuthorize)
                //          - CustomAuthorize takes an injected ILogger (see global.asax.cs)
                //      - redirects to Account/Login
                //      - On submission to this action
                //          - The model binder will only validate the form if the "captcha" is correct
                //          - we then only call SetAuthCookie if the email and password are correct
                //      - And finally we redirect to LoginSuccess
                if (model.EmailAddress == "test@test.com" && model.Password == "test")
                {
                    FormsAuthentication.SetAuthCookie(model.EmailAddress, false);
                    var redirectUrl = Request.QueryString["ReturnUrl"];
                    if (String.IsNullOrEmpty(redirectUrl))
                        return RedirectToAction("LoginSuccess");
                    else
                        return Redirect(redirectUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid credentials (so says the Login action)");
                }
            }

            //  If we got this far, something went wrong so display the view with the ModelState errors
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        
        public ActionResult LoginSuccess()
        {
            return View();
        }
    }
}