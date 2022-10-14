using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Productos
    {
        [Key]
        public int IdProducto { get; set; }
        public string Nombre { get; set; }

        public int IdRol { get; set; }
        [ForeignKey("IdRol")]
        public Roles Roles { get; set; }
    }
}
