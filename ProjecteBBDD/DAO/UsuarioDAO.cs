using MySql.Data.MySqlClient;
using ProjecteBBDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjecteBBDD.DAO
{

    public class UsuarioDAO : IDAO<Usuario, int>
    {
        private MySqlConnection connection;

        public UsuarioDAO(MySqlConnection connection)
        {
            this.connection = connection;
        }

        public void Delete(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public void Insert(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public int InsertReturnID(Usuario usuario)
        {
            int insertedId = -1;
            try
            {
                string query = "INSERT INTO USUARIO (nombre, apellido, correo, contrasena, fecha_nacimiento, tipo_usuario) VALUES (@nombre, @apellido, @correo, @contrasena, @fecha_nacimiento, @tipo_usuario);";
                string selectQuery = "SELECT LAST_INSERT_ID();";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("@apellido", usuario.Apellido);
                    command.Parameters.AddWithValue("@correo", usuario.Correo);
                    command.Parameters.AddWithValue("@contrasena", usuario.Contraseña);
                    command.Parameters.AddWithValue("@fecha_nacimiento", usuario.Fecha_nacimiento);
                    command.Parameters.AddWithValue("@tipo_usuario", usuario.Tipo_usuario);

                    connection.Open();

                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine("Registros insertados: " + rowsAffected);

                    using (MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection))
                    {
                        insertedId = Convert.ToInt32(selectCommand.ExecuteScalar());
                        Console.WriteLine("Registro insertado con ID: " + insertedId);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar el registro " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return insertedId;
        }

        public List<Usuario> SelectAll()
        {
            List<Usuario> usuarios = new List<Usuario>();
            try
            {
                string query = "SELECT * FROM USUARIO;";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Usuario usuario = new Usuario();
                            usuario.Id_usuario = reader.GetInt32("id_usuario");
                            usuario.Nombre = reader.GetString("nombre");
                            usuario.Apellido = reader.GetString("apellido");
                            usuario.Correo = reader.GetString("correo");
                            usuario.Contraseña = reader.GetString("contrasena");
                            usuario.Fecha_nacimiento = reader.GetDateTime("fecha_nacimiento");
                            usuario.Tipo_usuario = (Enums.TipoUsuarioEnum)Enum.Parse(typeof(Enums.TipoUsuarioEnum), reader.GetString("tipo_usuario"));

                            usuarios.Add(usuario);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener todos los usuarios: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return usuarios;
        }

        public Usuario SelectById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Usuario usuario)
        {
            throw new NotImplementedException();
        }
    }
}
