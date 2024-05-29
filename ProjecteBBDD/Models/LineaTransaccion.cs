using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjecteBBDD.Models
{
    public class LineaTransaccion
    {
        private int id_linea_transaccion;
        private int id_transaccion_lt;
        private int id_pelicula_lt;
        private double precio_pelicula;
        private int cantidad;
        private double total_lt;

        public int Id_linea_transaccion { get => id_linea_transaccion; set => id_linea_transaccion = value; }
        public int Id_transaccion_lt { get => id_transaccion_lt; set => id_transaccion_lt = value; }
        public int Id_pelicula_lt { get => id_pelicula_lt; set => id_pelicula_lt = value; }
        public double Precio_pelicula { get => precio_pelicula; set => precio_pelicula = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }
        public double Total_lt { get => total_lt; set => total_lt = value; }

    }
}
