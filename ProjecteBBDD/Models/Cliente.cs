using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjecteBBDD.Models
{
    public class Cliente
    {

        private int codi_cliente;
        private int id_usuario_c;
        private string telefono;
        private int? tarjeta_vinculada;
        private string? comentario_pref;
        private string foto_perfil_cliente;
        private List<Transaccion> transacciones;

        public int Codi_cliente { get => codi_cliente; set => codi_cliente = value; }
        public int Id_usuario_c { get => id_usuario_c; set => id_usuario_c = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public int? Tarjeta_vinculada { get => tarjeta_vinculada; set => tarjeta_vinculada = value; }
        public string Comentario_pref { get => comentario_pref; set => comentario_pref = value; }
        public string Foto_perfil_cliente { get => foto_perfil_cliente; set => foto_perfil_cliente = value; }
        public List<Transaccion> Transacciones { get => transacciones; set => transacciones = value; }
    }
}
