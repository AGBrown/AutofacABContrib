namespace Remember.Web.Service
{
    /// <summary>
    ///     A logging interface, as per 
    ///     https://github.com/autofac/Autofac/wiki/Mvc-Integration#inject-properties-into-filterattributes
    /// </summary>
    public interface ILogger
    {
        void Log(string message);
    }
}