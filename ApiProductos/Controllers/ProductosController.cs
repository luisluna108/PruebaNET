using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DAL.Context;
using DAL.Models;

namespace ApiProductos.Controllers
{
    [Route("api/Productos")]
    public class ProductosController : ApiController
    {
        private DataDBContext db = new DataDBContext();

        // GET: api/Productos/5
        [AllowAnonymous]
        [HttpGet]
        [Route("api/Productos/ProductoxRol")]
        public IHttpActionResult ProductoxRol(int idRol)
        {
            var productos = db.Productos.Where(x => x.IdRol.Equals(idRol)).ToList();
            if (productos == null)
            {
                return NotFound();
            }

            return Ok(productos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductosExists(int id)
        {
            return db.Productos.Count(e => e.IdProducto == id) > 0;
        }
    }
}