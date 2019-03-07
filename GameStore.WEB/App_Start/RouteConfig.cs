using System.Web.Mvc;
using System.Web.Routing;

namespace GameStore.WEB
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "CreateGame",
                url: "games/new",
                defaults: new { controller = "Game", action = "CreateGame" }
            );

            routes.MapRoute(
                name: "EditGame",
                url: "games/update",
                defaults: new { controller = "Game", action = "UpdateGame" }
            );

            routes.MapRoute(
                name: "GetAllGames",
                url: "games",
                defaults: new { controller = "Game", action = "GetAllGames" }
            );

            routes.MapRoute(
                name: "RemoveGame",
                url: "games/remove",
                defaults: new { controller = "Game", action = "RemoveGame" }
            );

            routes.MapRoute(
                name: "AddPublisher",
                url: "publisher/new",
                defaults: new { controller = "Publisher", action = "AddPublisher" }
                );

            routes.MapRoute(
                name: "PublisherDetails",
                url: "publisher/{CompanyName}",
                defaults: new { controller = "Publisher", action = "GetPublisherByCompanyName" }
                );


            routes.MapRoute(
                name: "GetGameByKey",
                url: "game/{key}",
                defaults: new { controller = "Game", action = "GetGameByKey" }
            );

            routes.MapRoute(
                name: "LeaveComment",
                url: "game/{gamekey}/newcomment",
                defaults: new { controller = "Comment", action = "LeaveComment" }
            );
            routes.MapRoute(
                name: "AddToBasket",
                url: "game/{gamekey}/buy",
                defaults: new { controller = "Order", action = "AddToBasket" }
            );
            routes.MapRoute(
                name: "AddReplay",
                url: "game/{gameKey}/{commentkey}/newComment",
                defaults: new { controller = "Comment", action = "AddReplay" }
            );

            routes.MapRoute(
                name: "GetAllCommentsByGame",
                url: "game/{gamekey}/comments",
                defaults: new { controller = "Comment", action = "GetAllCommentsByGame" }
            );

            routes.MapRoute(
                name: "DownloadGame",
                url: "game/{gamekey}/download",
                defaults: new { controller = "Game", action = "DownloadGame" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Game", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
