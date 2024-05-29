using ProjecteBBDD.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjecteBBDD.Models
{
    public class Pelicula
    {
        private int id_pelicula;
        private string portada;
        private string titulo;
        private GeneroEnum genero;
        private int duracion;
        private string descripcion;
        private double precio;

        public int Id_pelicula { get => id_pelicula; set => id_pelicula = value; }
        public string Portada { get => portada; set => portada = value; }
        public string Titulo { get => titulo; set => titulo = value; }
        public GeneroEnum Genero { get => genero; set => genero = value; }
        public int Duracion { get => duracion; set => duracion = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public double Precio { get => precio; set => precio = value; }

    }
}
