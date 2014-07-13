using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remember.Web.Service
{
    /// <summary>
    ///     A fake "logging" class
    /// </summary>
    public class FakeLogger : ILogger
    {
        public void Log(string message)
        {
            //   do nothing with the message
        }
    }
}
