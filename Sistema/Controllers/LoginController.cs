using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sistema.Models;

namespace Sistema.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {

            return View();
            
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Index(Login model)
        {
            if (ModelState.IsValid)
            {
               using(Sistema_EscolarContext db = new Sistema_EscolarContext())
                {
                    var a = db.Alumnos.Where(x => x.Nombre == model.Nombre && x.Pass == model.Pass).FirstOrDefault();
                    var p = db.Profesores.Where(s => s.Nombre == model.Nombre && s.Pass == model.Pass).FirstOrDefault();
                   

                    if (a !=null)
                    {
                        return RedirectToAction("Create", "Alumnos");
                       
                    }
                 
                     if(p != null)
                    {
                        return RedirectToAction("Create", "Profesores");
                    }

                    if(model.Nombre == "Admin" && model.Pass == "123")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return View("Index");
                    }


                }


            }
            return View();
        }
    }
}