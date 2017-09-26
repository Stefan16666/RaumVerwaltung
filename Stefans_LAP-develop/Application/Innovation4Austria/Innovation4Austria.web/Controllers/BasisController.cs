
using Innovation4Austria.logic;
using System.Web.Mvc;
using Verwaltung;

public abstract class BasisController : Controller
{
    protected override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        if (Request.IsAuthenticated)
        {
            string benutzerName = User?.Identity?.Name;
            Benutzer aktBenutzer = BenutzerVerwaltung.SucheFirmaVonBenutzer(benutzerName);
            log4net.LogicalThreadContext.Properties["benutzerid"] = aktBenutzer.Id;
        }
        else
        {
            log4net.LogicalThreadContext.Properties["benutzerid"] = null;
        }

        base.OnActionExecuting(filterContext);
    }
}