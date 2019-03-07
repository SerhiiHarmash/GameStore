using System.Diagnostics;
using GameStore.WEB.Infrastructure.Logger;
using System.Web.Mvc;

namespace GameStore.WEB.Infrastructure.Attributes
{
    public class LogHttpRequest : ActionFilterAttribute
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _stopwatch.Reset();
            _stopwatch.Start();
            GameStoreLogger.logger.Debug($"ActionExecuting: {filterContext.HttpContext.Request.Url}");
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _stopwatch.Stop();
            GameStoreLogger.logger.Debug($"ActionExecuted: {filterContext.HttpContext.Request.Url}; time of ActionExecuted:{_stopwatch.ElapsedMilliseconds}");        
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            _stopwatch.Reset();
            _stopwatch.Start();
            GameStoreLogger.logger.Debug($"ResultExecuting: {filterContext.HttpContext.Request.Url}");
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            _stopwatch.Stop();
            GameStoreLogger.logger.Debug($"ResultExecuted {filterContext.HttpContext.Request.Url}; time of ResultExecuted {_stopwatch.ElapsedMilliseconds}");
        }       
    }
}