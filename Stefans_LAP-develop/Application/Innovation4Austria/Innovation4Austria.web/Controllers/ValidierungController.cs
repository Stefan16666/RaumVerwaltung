using Innovation4Austria.logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Innovation4Austria.web.Controllers
{
    public class ValidierungController : Controller
    {
        public JsonResult EmailFrei(string Emailadresse)
        {
            bool benutzerExistiert = false;

            benutzerExistiert = Tools.EmailVorhanden(Emailadresse);

            if (benutzerExistiert)
                return Json(false, JsonRequestBehavior.AllowGet);
            else
                return Json(true, JsonRequestBehavior.AllowGet);
        }

    }
}