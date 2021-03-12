using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BancoEmpleado.Models.ViewModel;
using System.Data.Entity;
using BancoEmpleado.Models;
namespace BancoEmpleado.Controllers
{
    public class EmpleadoController : Controller
    {
        // GET: Empleado
        public ActionResult Index()
        {

            personalDBEntities db = new personalDBEntities();
            
               var listEmp = db.empleado.ToList().Select(e=>new ViewModelEmpleado(){ID=e.id, Nombres=e.nombres, Apellidos=e.apellidos, Identificacion=e.identificacion, TipoIdentificacion=(e.tipo_ident=="C"?"Cedula":(e.tipo_ident=="R"?"RUC":"Pasaporte")), Estado=e.estado});
            
                return View(listEmp);
        }

        // GET: Empleado/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Empleado/Create
        public ActionResult Create()
        {
            ViewModelEmpleado oEmp = new ViewModelEmpleado();
            return View(oEmp);
        }

        // POST: Empleado/Create
        [HttpPost]
        public ActionResult Create(ViewModelEmpleado modelo)
        {
            try
            {
                if (modelo.Nombres != null && modelo.TipoIdentificacion !=null)
                {
                    if (idEsValida(modelo.TipoIdentificacion.Substring(0, 1), modelo.Identificacion))
                    {
                        using (personalDBEntities db = new personalDBEntities())
                        {
                            empleado oEmpleado = new empleado();
                            oEmpleado.nombres = modelo.Nombres;
                            oEmpleado.apellidos = modelo.Apellidos;
                            oEmpleado.identificacion = modelo.Identificacion;
                            oEmpleado.tipo_ident = modelo.TipoIdentificacion.Substring(0, 1);
                            oEmpleado.estado = modelo.Estado;
                            db.empleado.Add(oEmpleado);
                            db.SaveChanges();

                        }
                        ViewBag.message = "Empleado ha sido creado correctamente.";
                        ViewBag.msgclass = "success";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        new Exception("Su identificación no es válida.");
                    }
                }
                else {
                    new Exception("Campos son obligatorios");
                }
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.message = ex.Message;
                return View();
            }

            
        }

        // GET: Empleado/Edit/5
        public ActionResult Edit(int id)
        {
            ViewModelEmpleado model = new ViewModelEmpleado();
            using (personalDBEntities db = new personalDBEntities())
            {
                empleado oEmpleado = db.empleado.Find(id);
                model.Nombres = oEmpleado.nombres;
                model.Apellidos = oEmpleado.apellidos;
                model.Estado = oEmpleado.estado;
                model.Identificacion = oEmpleado.identificacion;
                model.TipoIdentificacion = oEmpleado.tipo_ident;

            }

            return View(model);
        }

        // POST: Empleado/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ViewModelEmpleado modelo)
        {
            try
            {
                string vlTipo = modelo.TipoIdentificacion.Substring(0, 1);
                if (idEsValida(vlTipo, modelo.Identificacion))
                {
                    using (personalDBEntities db = new personalDBEntities())
                    {
                        empleado oEmpleado = db.empleado.Find(id);
                        oEmpleado.nombres = modelo.Nombres;
                        oEmpleado.apellidos = modelo.Apellidos;
                        oEmpleado.tipo_ident = vlTipo;
                        oEmpleado.identificacion = modelo.Identificacion;
                        oEmpleado.estado = modelo.Estado;

                        db.SaveChanges();
                        ViewBag.message = "Empleado actualizado exitosamente";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    throw new Exception("Su identificación no es válida.");
                }

                return View();

            }
            catch(Exception ex)
            {
                ViewBag.message = ex.Message;
                return View();
            }

        }

        
        // POST: Empleado/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                using (personalDBEntities db = new personalDBEntities())
                {
                    empleado oEmpleado = db.empleado.Find(id);

                    db.empleado.Remove(oEmpleado);
                    db.SaveChanges();


                }

                ViewBag.message = "El empleado " + id.ToString() + "ha sido eliminado";
            }
            catch
            {
                ViewBag.message = "Ha ocurrido un error al eliminar";
            }
            return RedirectToAction("Index");
        }


        public bool idEsValida(string tipo, string identificacion)
        {
            bool rtVal = false;
            int totalChars = 10;
            switch (tipo)
            {
                case "C":
                    totalChars = 10;
                    break;
                case "P":
                    totalChars = 20;
                    break;
                case "R":

                    totalChars = 13;
                    break;
                default:
                    break;
            }
            if (identificacion.Length == totalChars)
            {
                rtVal = true;
            }
            
            return rtVal;
        }
    }
}
