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

        ConsultaLlamadasEntities db = new ConsultaLlamadasEntities();
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]  //Aquí se utilizó Session para mantener la sesión del usuario durante la ejecucion del programa
        public ActionResult Authorize(ConsultaLlamadas.Models.CatalogoAcceso userModel)
        {
            using (ConsultaLlamadasEntities db = new ConsultaLlamadasEntities())
            {

                //Verifica que los datos ingresados sean correctos
                var userDetails = db.CatalogoAcceso.Where(x => x.UserName == userModel.UserName && x.Password == userModel.Password).FirstOrDefault();
                if(userDetails == null)
                {
                    userModel.ErrorMessage = "Usuario y/o contraseña son incorrectos.";
                    return View("Index", userModel);
                }
                else //Si son correctos, guardará estos datos en la sesión y los mostrará en la siguiente pantalla
                {
                    Session["personId"] = userDetails.PersonId;
                    Session["personName"] = userDetails.PersonName;
                    Session["userType"] = userDetails.UserType;
                    Session["rol"] = userDetails.Rol;
                    return RedirectToAction("Index", "Home");
                }
            }
             
        }

        public ActionResult LogOut()  //Este bloque de codigo cierra la sesión
        {
            //int personId = (int)Session["personId"];
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public ActionResult AddOrEdit(int Id=0)  //Este bloque de codigo trae el modelo de la tabla de usuarios
        {
            CatalogoAcceso userModel = new CatalogoAcceso();
            return View(userModel);
        }

        [HttpPost]
        public ActionResult AddOrEdit(CatalogoAcceso userModel)  //Guarda nuevo usuario en la base de datos
        {
            db.CatalogoAcceso.Add(userModel);
            db.SaveChanges();

            ModelState.Clear();
            ViewBag.SuccessMessage = "El usuario se guardó con exito";

            return View("AddOrEdit", new CatalogoAcceso());
        }
    }
}