using MySql.Data.MySqlClient;
using ProjecteBBDD.DAO;
using ProjecteBBDD.Models;
using System.Xml;

namespace ProjecteBBDD.XmlController
{
    public class BDXml
    {
        private MySqlConnection connection;

        public BDXml(MySqlConnection connection)
        {
            this.connection = connection;
        }

        public void GetDatosYCrearXml(string xmlFilePath)
        {
            try
            {
                UsuarioDAO usuarioDAO = new UsuarioDAO(connection);
                List<Usuario> usuarios = usuarioDAO.SelectAll();

                ClienteDAO clienteDAO = new ClienteDAO(connection);
                TransaccionDAO transaccionDAO = new TransaccionDAO(connection);
                LineaTransaccionDAO lineaTransaccionDAO = new LineaTransaccionDAO(connection);

                List<Cliente> clientes = clienteDAO.SelectAll();
                List<Transaccion> todasTransacciones = transaccionDAO.SelectAll();
                List<LineaTransaccion> todasLineasTransacciones = lineaTransaccionDAO.SelectAll();

                PeliculaDAO peliculaDAO = new PeliculaDAO(connection);
                List<Pelicula> peliculas = peliculaDAO.SelectAll();

                XmlDocument xmlDocument = new XmlDocument();

                XmlElement cinemaElement = xmlDocument.CreateElement("cinema");
                xmlDocument.AppendChild(cinemaElement);

                // Añadir usuarios al XML
                XmlElement usuariosElement = xmlDocument.CreateElement("usuarios");
                foreach (var usuario in usuarios)
                {
                    XmlElement usuarioElement = xmlDocument.CreateElement("usuario");

                    usuarioElement.AppendChild(CreateElement(xmlDocument, "id_usuario", usuario.Id_usuario.ToString()));
                    usuarioElement.AppendChild(CreateElement(xmlDocument, "nombre", usuario.Nombre));
                    usuarioElement.AppendChild(CreateElement(xmlDocument, "apellido", usuario.Apellido));
                    usuarioElement.AppendChild(CreateElement(xmlDocument, "correo", usuario.Correo));
                    usuarioElement.AppendChild(CreateElement(xmlDocument, "fecha_nacimiento", usuario.Fecha_nacimiento.ToString("yyyy-MM-dd")));
                    usuarioElement.AppendChild(CreateElement(xmlDocument, "tipo_usuario", usuario.Tipo_usuario.ToString()));

                    usuariosElement.AppendChild(usuarioElement);
                }
                cinemaElement.AppendChild(usuariosElement);

                // Añadir clientes al XML
                XmlElement clientesElement = xmlDocument.CreateElement("clientes");
                foreach (var cliente in clientes)
                {
                    XmlElement clienteElement = xmlDocument.CreateElement("cliente");

                    clienteElement.AppendChild(CreateElement(xmlDocument, "codi_cliente", cliente.Codi_cliente.ToString()));
                    clienteElement.AppendChild(CreateElement(xmlDocument, "id_usuario_c", cliente.Id_usuario_c.ToString()));
                    clienteElement.AppendChild(CreateElement(xmlDocument, "telefono", cliente.Telefono));
                    clienteElement.AppendChild(CreateElement(xmlDocument, "tarjeta_vinculada", cliente.Tarjeta_vinculada.ToString()));
                    clienteElement.AppendChild(CreateElement(xmlDocument, "comentario_pref", cliente.Comentario_pref));
                    clienteElement.AppendChild(CreateElement(xmlDocument, "foto_perfil_cliente", cliente.Foto_perfil_cliente));

                    var transaccionesCliente = todasTransacciones.Where(t => t.Codi_cliente_t == cliente.Codi_cliente).ToList();

                    XmlElement transaccionesElement = xmlDocument.CreateElement("transacciones");
                    foreach (var transaccion in transaccionesCliente)
                    {
                        XmlElement transaccionElement = xmlDocument.CreateElement("transaccion");

                        transaccionElement.AppendChild(CreateElement(xmlDocument, "id_transaccion", transaccion.Id_transaccion.ToString()));
                        transaccionElement.AppendChild(CreateElement(xmlDocument, "codi_cliente_t", transaccion.Codi_cliente_t.ToString()));
                        transaccionElement.AppendChild(CreateElement(xmlDocument, "tipo_transaccion", transaccion.Tipo_transaccion.ToString()));
                        transaccionElement.AppendChild(CreateElement(xmlDocument, "fecha_transaccion", transaccion.Fecha_transaccion.ToString("yyyy-MM-dd")));
                        transaccionElement.AppendChild(CreateElement(xmlDocument, "total_t", transaccion.Total_t.ToString()));

                        var lineasTransaccion = todasLineasTransacciones.Where(lt => lt.Id_transaccion_lt == transaccion.Id_transaccion).ToList();

                        XmlElement lineasTransaccionElement = xmlDocument.CreateElement("lineas_transaccion");
                        foreach (var lineaTransaccion in lineasTransaccion)
                        {
                            XmlElement lineaTransaccionElement = xmlDocument.CreateElement("linea_transaccion");

                            lineaTransaccionElement.AppendChild(CreateElement(xmlDocument, "id_linea_transaccion", lineaTransaccion.Id_linea_transaccion.ToString()));
                            lineaTransaccionElement.AppendChild(CreateElement(xmlDocument, "id_transaccion_lt", lineaTransaccion.Id_transaccion_lt.ToString()));
                            lineaTransaccionElement.AppendChild(CreateElement(xmlDocument, "id_pelicula_lt", lineaTransaccion.Id_pelicula_lt.ToString()));
                            lineaTransaccionElement.AppendChild(CreateElement(xmlDocument, "precio_pelicula", lineaTransaccion.Precio_pelicula.ToString()));
                            lineaTransaccionElement.AppendChild(CreateElement(xmlDocument, "cantidad", lineaTransaccion.Cantidad.ToString()));
                            lineaTransaccionElement.AppendChild(CreateElement(xmlDocument, "total_lt", lineaTransaccion.Total_lt.ToString()));

                            lineasTransaccionElement.AppendChild(lineaTransaccionElement);
                        }
                        transaccionElement.AppendChild(lineasTransaccionElement);

                        transaccionesElement.AppendChild(transaccionElement);
                    }
                    clienteElement.AppendChild(transaccionesElement);

                    clientesElement.AppendChild(clienteElement);
                }
                cinemaElement.AppendChild(clientesElement);

                // Añadir peliculas al XML
                XmlElement peliculasElement = xmlDocument.CreateElement("peliculas");
                foreach (var pelicula in peliculas)
                {
                    XmlElement peliculaElement = xmlDocument.CreateElement("pelicula");

                    peliculaElement.AppendChild(CreateElement(xmlDocument, "id_pelicula", pelicula.Id_pelicula.ToString()));
                    peliculaElement.AppendChild(CreateElement(xmlDocument, "portada", pelicula.Portada));
                    peliculaElement.AppendChild(CreateElement(xmlDocument, "titulo", pelicula.Titulo));
                    peliculaElement.AppendChild(CreateElement(xmlDocument, "genero", pelicula.Genero.ToString()));
                    peliculaElement.AppendChild(CreateElement(xmlDocument, "duracion", pelicula.Duracion.ToString()));
                    peliculaElement.AppendChild(CreateElement(xmlDocument, "descripcion", pelicula.Descripcion));
                    peliculaElement.AppendChild(CreateElement(xmlDocument, "precio", pelicula.Precio.ToString()));

                    peliculasElement.AppendChild(peliculaElement);
                }
                cinemaElement.AppendChild(peliculasElement);

                xmlDocument.Save(xmlFilePath);
                Console.WriteLine("Archivo XML creado correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener datos y crear el archivo XML: " + ex.Message);
            }
        }

        private XmlElement CreateElement(XmlDocument xmlDocument, string name, string value)
        {
            XmlElement element = xmlDocument.CreateElement(name);
            element.InnerText = value;
            return element;
        }
    }
}
