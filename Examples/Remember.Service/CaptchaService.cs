using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Remember.Service
{
    public class CaptchaService:ICaptchaService
    {


        public bool IsValid(string captcha)
        {
            if (captcha == "4")
            {
                return true;
            }
            return false;
        }

        
    }
}
