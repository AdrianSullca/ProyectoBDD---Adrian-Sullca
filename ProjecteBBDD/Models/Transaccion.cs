using ProjecteBBDD.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjecteBBDD.Models
{
    public class Transaccion
    {

        private int id_transaccion;
        private int codi_cliente_t;
        private TipoTransaccionEnum tipo_transaccion;
        private DateTime fecha_transaccion;
        private double total_t;
        private List<LineaTransaccion> lineas_transaccion;

        public int Id_transaccion { get => id_transaccion; set => id_transaccion = value; }
        public int Codi_cliente_t { get => codi_cliente_t; set => codi_cliente_t = value; }
        public TipoTransaccionEnum Tipo_transaccion { get => tipo_transaccion; set => tipo_transaccion = value; }
        public DateTime Fecha_transaccion { get => fecha_transaccion; set => fecha_transaccion = value; }
        public double Total_t { get => total_t; set => total_t = value; }
        public List<LineaTransaccion> Lineas_transaccion { get => lineas_transaccion; set => lineas_transaccion = value; }

    }
}
