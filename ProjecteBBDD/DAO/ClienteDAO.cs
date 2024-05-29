using MySql.Data.MySqlClient;
using ProjecteBBDD.Models;
using System;
using System.Collections.Generic;

namespace ProjecteBBDD.DAO
{
    public class ClienteDAO : IDAO<Cliente, int>
    {
        private MySqlConnection connection;

        public ClienteDAO(MySqlConnection connection)
        {
            this.connection = connection;
        }

        public void Delete(Cliente cliente)
        {
            throw new NotImplementedException();
        }

        public void Insert(Cliente cliente)
        {
            throw new NotImplementedException();
        }

        public int InsertReturnID(Cliente cliente)
        {
            int insertedId = -1;
            try
            {
                string query = "INSERT INTO CLIENTE (id_usuario_c, telefono, tarjeta_vinculada, comentario_pref, foto_perfil_cliente) VALUES (@id_usuario_c, @telefono, @tarjeta_vinculada, @comentario_pref, @foto_perfil_cliente);";
                string selectQuery = "SELECT LAST_INSERT_ID();";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id_usuario_c", cliente.Id_usuario_c);
                    command.Parameters.AddWithValue("@telefono", cliente.Telefono);
                    command.Parameters.AddWithValue("@tarjeta_vinculada", cliente.Tarjeta_vinculada);
                    command.Parameters.AddWithValue("@comentario_pref", cliente.Comentario_pref);
                    command.Parameters.AddWithValue("@foto_perfil_cliente", cliente.Foto_perfil_cliente);

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

        public List<Cliente> SelectAll()
        {
            List<Cliente> clientes = new List<Cliente>();
            try
            {
                string query = "SELECT * FROM CLIENTE;";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cliente cliente = new Cliente();
                            cliente.Codi_cliente = reader.GetInt32("codi_cliente");
                            cliente.Id_usuario_c = reader.GetInt32("id_usuario_c");
                            cliente.Telefono = reader.GetString("telefono");
                            cliente.Tarjeta_vinculada = reader.GetInt32("tarjeta_vinculada");
                            cliente.Comentario_pref = reader.GetString("comentario_pref");
                            cliente.Foto_perfil_cliente = reader.GetString("foto_perfil_cliente");

                            clientes.Add(cliente);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener todos los clientes: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return clientes;
        }

        public Cliente SelectById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Cliente cliente)
        {
            throw new NotImplementedException();
        }
    }
}