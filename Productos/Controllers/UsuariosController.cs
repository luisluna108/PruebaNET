using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DAL.Context;
using DAL.Models;
using Newtonsoft.Json;

namespace Productos.Controllers
{
    public class UsuariosController : Controller
    {
        private DataDBContext db = new DataDBContext();

        // GET: Usuarios
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        // GET: Usuarios
        public async Task<ActionResult> Index(Usuarios usuarios)
        {
            var validUsuario = false;
            var httpClient = new HttpClient();

            var values = new List<KeyValuePair<string, string>>();
            values.Add(new KeyValuePair<string, string>("Nombre", usuarios.Nombre));
            values.Add(new KeyValuePair<string, string>("Password", usuarios.Password));
            var content = new FormUrlEncodedContent(values);

            var jsonResult = await httpClient.PostAsync("http://localhost:64592/api/Usuarios/ValidarCredenciales", content);

            return View(validUsuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            ViewBag.IdRol = new SelectList(db.Roles, "IdRol", "Nombre");
            return View();
        }

        // POST: Usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdUsuario,Nombre,Password,IdRol")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuarios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdRol = new SelectList(db.Roles, "IdRol", "Nombre", usuarios.IdRol);
            return View(usuarios);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
