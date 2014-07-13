using System;
using System.Web.Mvc;
using Autofac.Integration.Mvc;
using Remember.Web.Models;
using Remember.Service;

namespace Remember.Web.Binders
{
    [ModelBinderType(typeof(LoginForm))]
    public class LoginFormBinder: IModelBinder
    {
        private readonly ICaptchaService _authService;

        public LoginFormBinder(ICaptchaService authService)
        {
            _authService = authService;
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {

            var model = new LoginForm();

            try
            {

                // do the bind

                model.EmailAddress = bindingContext.ValueProvider.GetValue("Email").AttemptedValue;
                model.Password = bindingContext.ValueProvider.GetValue("Password").AttemptedValue;
                model.Captcha = bindingContext.ValueProvider.GetValue("Captcha").AttemptedValue;

                // validate

                if (!_authService.IsValid(model.Captcha))
                {
                    bindingContext.ModelState.AddModelError("", "Invalid captcha (so says the injected LoginFormBinder)");
                }
            }
            catch (Exception ex)
            {
                bindingContext.ModelState.AddModelError("", ex);

            }

            return model;


        }

    }
}