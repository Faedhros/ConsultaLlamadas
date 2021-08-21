using ConsultaLlamadas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConsultaLlamadas.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Authorize(ConsultaLlamadas.Models.CatalogoAcceso userModel)
        {
            using (ConsultaLlamadasEntities db = new ConsultaLlamadasEntities())
            {
                var userDetails = db.CatalogoAcceso.Where(x => x.UserName == userModel.UserName && x.Password == userModel.Password).FirstOrDefault();
                if(userDetails == null)
                {
                    userModel.ErrorMessage = "Usuario y/o contraseña son incorrectos.";
                    return View("Index", userModel);
                }
                else
                {
                    Session["personId"] = userDetails.PersonId;
                    return RedirectToAction("Index", "Home");
                }
            }
             
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}