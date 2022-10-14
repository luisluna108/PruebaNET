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
using DAL.ViewModels;

namespace ApiUsuarios.Controllers
{
    [Route("api/Usuarios")]
    public class UsuariosController : ApiController
    {
        private DataDBContext db = new DataDBContext();


        // GET: api/Usuarios/ValidarCredenciales/usuario
        [AllowAnonymous]
        [HttpPost]
        [Route("api/Usuarios/ValidarCredenciales")]
        public IHttpActionResult ValidarCredenciales(Usuarios usuario)
        {
            Usuarios usuarios = db.Usuarios.Where(x => x.Nombre.Equals(usuario.Nombre) && x.Password.Equals(usuario.Password)).FirstOrDefault();
            if (usuarios == null)
            {
                return NotFound();
            }

            return Ok(usuarios);
        }

        // GET: api/Usuarios/ObtenerRolesUsuarios/usuario
        [AllowAnonymous]
        [HttpGet]
        [Route("api/Usuarios/ObtenerRolesUsuarios")]
        public IHttpActionResult ObtenerRolesUsuarios(string usuario)
        {
            Usuarios usuarios = db.Usuarios.Include(r => r.Roles).Where(x => x.Nombre.Equals(usuario)).FirstOrDefault();
            if (usuarios == null)
            {
                return NotFound();
            }

            var rolAsignado = new UsuarioRolViewModels()
            {
                IdRol = usuarios.IdRol,
                IdUsuario = usuarios.IdUsuario,
                NombreRol = usuarios.Roles.Nombre,
                NombreUsuario = usuarios.Nombre
            };

            return Ok(rolAsignado);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UsuariosExists(int id)
        {
            return db.Usuarios.Count(e => e.IdUsuario == id) > 0;
        }
    }
}