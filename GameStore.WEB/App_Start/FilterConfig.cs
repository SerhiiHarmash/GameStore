using System.Web.Mvc;
using GameStore.WEB.Infrastructure.Attributes;

namespace GameStore.WEB.App_Start
{
    internal class FilterConfig
    {
        public static void RegisterGloabalFilter(GlobalFilterCollection filters)
        {
            filters.Add(new CustomHandleErrorAttribute());
            filters.Add(new LogHttpRequest());
        }
    }
}