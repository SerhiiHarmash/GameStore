using GameStore.WEB.Infrastructure.Logger;
using System.Web.Mvc;

namespace GameStore.WEB.Infrastructure.Attributes
{
    public class CustomHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            GameStoreLogger.logger.Error(filterContext.Exception.ToString());                  
        }
    }
}