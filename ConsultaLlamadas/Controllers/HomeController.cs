using ConsultaLlamadas.Models;
using ConsultaLlamadas.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
 
namespace ConsultaLlamadas.Controllers
{
    public class HomeController : Controller
    {
        private ConsultaLlamadasEntities db = new ConsultaLlamadasEntities();
        //GET: Home
        public ActionResult Index(string searchBy, string search, int page = 0)
        {

            //Aquí se hace una instancia del contexto, y se recorre cada uno de los campos. El resultado será una tabla con el listado de lineas telefonicas
            List<TablaLineaLlamadas> lst;
            using (ConsultaLlamadasEntities db = new ConsultaLlamadasEntities())
            {

                //SearchBy se usa en el buscador. Si la opción seleccionada es 'Description', va a filtrar la lista de lineas por Descripción
                if(searchBy == "Description")
                { 
                lst = (from d in db.LineaLlamadas
                       select new TablaLineaLlamadas
                       {
                           MobileLineId = d.MobileLineId,
                           MobileLine = d.MobileLine,
                           Description = d.Description
                       }).Where(x => x.Description.StartsWith(search) || search==null).ToList();
                }else  //De lo contrario, va a buscar por MobileLine
                {
                    lst = (from d in db.LineaLlamadas
                           select new TablaLineaLlamadas
                           {
                               MobileLineId = d.MobileLineId,
                               MobileLine = d.MobileLine,
                               Description = d.Description
                           }).Where(x => x.MobileLine.ToString().StartsWith(search) || search == null).ToList();
                }
            }
            //if (searchBy == "MobileLine")
            //{
            //    lst.Where(x => Convert.ToString(x.MobileLine) == search);
            //}
            //else
            //{
            //    lst.Where(x => x.Description == search);
            //}





            //*Paginador*
            //Esta pequeña logica es la que se utiliza para el paginador
            const int PageSize = 8; // Puede incrementar el numero de elementos que se muestran en la talba

            var count = lst.Count();

            var data = lst.Skip(page * PageSize).Take(PageSize).ToList();

            this.ViewBag.MaxPage = (count / PageSize) - (count % PageSize == 0 ? 1 : 0);

            this.ViewBag.Page = page;
            //*Paginador*

            
            return this.View(data);
            

        }

        public ActionResult Details( int Id = 0) //Cuando el usuario da click en detalles se hace una consulta
            //Toma el Id seleccionado y lo compara con el Id que se encuentra en el Detalle de Llamadas
            //El bloque de abajo, es similar a un inner join en un stored procedure. Devuelve una lista de lineas relacionadas a la linea seleccionada
        {
 
            var detalles = db.DetalleLlamadas.GroupJoin(db.LineaLlamadas, ln => ln.MobileLineId, dt => dt.MobileLineId
            , (ln, dt) => new { ln, dt }).Where(x => x.ln.MobileLineId == Id).ToList();


                return View(db.DetalleLlamadas.Where(x => x.MobileLineId == Id).ToList());

             
            
        }
    }
}