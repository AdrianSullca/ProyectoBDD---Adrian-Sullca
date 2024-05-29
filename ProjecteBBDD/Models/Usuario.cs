using ProjecteBBDD.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjecteBBDD.Models
{
    public class Usuario
    {
        private int id_usuario;
        private String nombre;
        private String apellido;
        private String correo;
        private String contraseña;
        private DateTime fecha_nacimiento;
        private TipoUsuarioEnum tipo_usuario;

        public int Id_usuario { get => id_usuario; set => id_usuario = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido { get => apellido; set => apellido = value; }
        public string Correo { get => correo; set => correo = value; }
        public string Contraseña { get => contraseña; set => contraseña = value; }
        public DateTime Fecha_nacimiento { get => fecha_nacimiento; set => fecha_nacimiento = value; }
        public TipoUsuarioEnum Tipo_usuario { get => tipo_usuario; set => tipo_usuario = value; }

    }
}
