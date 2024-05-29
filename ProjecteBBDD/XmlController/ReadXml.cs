using MySql.Data.MySqlClient;
using ProjecteBBDD.DAO;
using ProjecteBBDD.Models;
using ProjecteBBDD.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ProjecteBBDD.XmlController
{
    public class ReadXml
    {

        public List<Usuario> ObtenerUsuariosDesdeXml(string xmlFilePath)
        {
            List<Usuario> usuarios = new List<Usuario>();

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(xmlFilePath);

                XmlNodeList usuarioNodes = xmlDocument.SelectNodes("//usuario");
                foreach (XmlNode usuarioNode in usuarioNodes)
                {
                    Usuario usuario = new Usuario();
                    usuario.Id_usuario = int.Parse(usuarioNode.SelectSingleNode("id_usuario").InnerText);
                    usuario.Nombre = usuarioNode.SelectSingleNode("nombre").InnerText;
                    usuario.Apellido = usuarioNode.SelectSingleNode("apellido")?.InnerText ?? string.Empty;
                    usuario.Correo = usuarioNode.SelectSingleNode("correo")?.InnerText ?? string.Empty;
                    usuario.Contraseña = usuarioNode.SelectSingleNode("contrasena")?.InnerText ?? string.Empty;

                    string fechaNacimientoString = usuarioNode.SelectSingleNode("fecha_nacimiento").InnerText;
                    usuario.Fecha_nacimiento = DateTime.Parse(fechaNacimientoString);

                    string tipoUsuarioString = usuarioNode.SelectSingleNode("tipo_usuario").InnerText;
                    usuario.Tipo_usuario = (Enums.TipoUsuarioEnum)Enum.Parse(typeof(Enums.TipoUsuarioEnum), tipoUsuarioString);
                    Console.WriteLine("Mensaje de debug del enumerado en metodo obtenerUsuarioDesdeXML");
                    Console.WriteLine(usuario.Tipo_usuario);
                    usuarios.Add(usuario);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener los usuarios desde el archivo XML: " + ex.Message);
            }

            return usuarios;
        }

        public List<Pelicula> ObtenerPeliculasDesdeXml(string xmlFilePath)
        {
            List<Pelicula> peliculas = new List<Pelicula>();

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(xmlFilePath);

                XmlNodeList peliculaNodes = xmlDocument.SelectNodes("//pelicula");
                foreach (XmlNode peliculaNode in peliculaNodes)
                {
                    Pelicula pelicula = new Pelicula();
                    pelicula.Id_pelicula = int.Parse(peliculaNode.SelectSingleNode("id_pelicula").InnerText);
                    pelicula.Portada = peliculaNode.SelectSingleNode("portada").InnerText;
                    pelicula.Titulo = peliculaNode.SelectSingleNode("titulo").InnerText;
                    string generoPeliculaString = peliculaNode.SelectSingleNode("genero").InnerText;
                    pelicula.Genero = (Enums.GeneroEnum)Enum.Parse(typeof(Enums.GeneroEnum), generoPeliculaString);
                    pelicula.Duracion = int.Parse(peliculaNode.SelectSingleNode("duracion").InnerText);
                    pelicula.Descripcion = peliculaNode.SelectSingleNode("descripcion").InnerText;
                    pelicula.Precio = double.Parse(peliculaNode.SelectSingleNode("precio").InnerText);

                    peliculas.Add(pelicula);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener las películas desde el archivo XML: " + ex.Message);
            }

            return peliculas;
        }

        public List<Cliente> ObtenerClientesDesdeXml(string xmlFilePath)
        {
            List<Cliente> clientes = new List<Cliente>();

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(xmlFilePath);

                XmlNodeList clienteNodes = xmlDocument.SelectNodes("//cliente");
                foreach (XmlNode clienteNode in clienteNodes)
                {
                    Cliente cliente = new Cliente();
                    cliente.Codi_cliente = int.Parse(clienteNode.SelectSingleNode("codi_cliente").InnerText);
                    cliente.Id_usuario_c = int.Parse(clienteNode.SelectSingleNode("id_usuario_c").InnerText);
                    cliente.Telefono = clienteNode.SelectSingleNode("telefono").InnerText;
                    cliente.Tarjeta_vinculada = int.Parse(clienteNode.SelectSingleNode("tarjeta_vinculada").InnerText);
                    cliente.Comentario_pref = clienteNode.SelectSingleNode("comentario_pref").InnerText;
                    cliente.Foto_perfil_cliente = clienteNode.SelectSingleNode("foto_perfil_cliente").InnerText;

                    List<Transaccion> transacciones = new List<Transaccion>();

                    XmlNodeList transaccionNodes = clienteNode.SelectNodes("transacciones/transaccion");
                    foreach (XmlNode transaccionNode in transaccionNodes)
                    {
                        Transaccion transaccion = new Transaccion();
                        transaccion.Id_transaccion = int.Parse(transaccionNode.SelectSingleNode("id_transaccion").InnerText);
                        transaccion.Codi_cliente_t = int.Parse(transaccionNode.SelectSingleNode("codi_cliente_t").InnerText);
                        string tipoTransaccionString = transaccionNode.SelectSingleNode("tipo_transaccion").InnerText;
                        transaccion.Tipo_transaccion = (Enums.TipoTransaccionEnum)Enum.Parse(typeof(Enums.TipoTransaccionEnum), tipoTransaccionString);
                        string fechaTransaccionString = transaccionNode.SelectSingleNode("fecha_transaccion").InnerText;
                        transaccion.Fecha_transaccion = DateTime.Parse(fechaTransaccionString);
                        transaccion.Total_t = double.Parse(transaccionNode.SelectSingleNode("total_t").InnerText);

                        List<LineaTransaccion> lineasTransaccion = new List<LineaTransaccion>();

                        XmlNodeList lineaTransaccionNodes = transaccionNode.SelectNodes("lineas_transaccion/linea_transaccion");
                        foreach (XmlNode lineaTransaccionNode in lineaTransaccionNodes)
                        {
                            LineaTransaccion lineaTransaccion = new LineaTransaccion();
                            lineaTransaccion.Id_linea_transaccion = int.Parse(lineaTransaccionNode.SelectSingleNode("id_linea_transaccion").InnerText);
                            lineaTransaccion.Id_pelicula_lt = int.Parse(lineaTransaccionNode.SelectSingleNode("id_pelicula_lt").InnerText);
                            lineaTransaccion.Precio_pelicula = double.Parse(lineaTransaccionNode.SelectSingleNode("precio_pelicula").InnerText);
                            lineaTransaccion.Cantidad = int.Parse(lineaTransaccionNode.SelectSingleNode("cantidad").InnerText);
                            lineaTransaccion.Total_lt = double.Parse(lineaTransaccionNode.SelectSingleNode("total_lt").InnerText);

                            lineasTransaccion.Add(lineaTransaccion);
                        }

                        transaccion.Lineas_transaccion = lineasTransaccion;

                        transacciones.Add(transaccion);
                    }

                    cliente.Transacciones = transacciones;
                    clientes.Add(cliente);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener los clientes desde el archivo XML: " + ex.Message);
            }

            return clientes;
        }


        public void EnviarDatos(List<Usuario> usuarios, List<Pelicula> peliculas, List<Cliente> clientes)
        {
            try
            {
                UsuarioDAO usuarioDAO = new UsuarioDAO(DataBaseManager.getConnection());
                PeliculaDAO peliculaDAO = new PeliculaDAO(DataBaseManager.getConnection());
                ClienteDAO clienteDAO = new ClienteDAO(DataBaseManager.getConnection());
                TransaccionDAO transaccionDAO = new TransaccionDAO(DataBaseManager.getConnection());
                LineaTransaccionDAO lineaTransaccionDAO = new LineaTransaccionDAO(DataBaseManager.getConnection());

                foreach (Usuario usuario in usuarios)
                {
                    int idUsuario = usuarioDAO.InsertReturnID(usuario);
                    usuario.Id_usuario = idUsuario;
                }

                foreach (Pelicula pelicula in peliculas)
                {
                    int idPelicula = peliculaDAO.InsertReturnID(pelicula);
                    pelicula.Id_pelicula = idPelicula;
                }

                foreach (Cliente cliente in clientes)
                {
                    int idCliente = clienteDAO.InsertReturnID(cliente);
                    cliente.Codi_cliente = idCliente;

                    foreach (Transaccion transaccion in cliente.Transacciones)
                    {
                        int idTransaccion = transaccionDAO.InsertReturnID(transaccion);
                        transaccion.Id_transaccion = idTransaccion;

                        foreach (LineaTransaccion lineaTransaccion in transaccion.Lineas_transaccion)
                        {
                            lineaTransaccionDAO.Insert(lineaTransaccion, idTransaccion);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar los datos a la base de datos: " + ex.Message);
            }
        }
    }
}
