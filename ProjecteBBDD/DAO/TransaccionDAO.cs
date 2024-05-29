using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using ProjecteBBDD.Models;
using System;
using System.Collections.Generic;

namespace ProjecteBBDD.DAO
{
    public class TransaccionDAO : IDAO<Transaccion, int>
    {
        private MySqlConnection connection;

        public TransaccionDAO(MySqlConnection connection)
        {
            this.connection = connection;
        }

        public void Delete(Transaccion transaccion)
        {
            throw new NotImplementedException();
        }

        public void Insert(Transaccion transaccion)
        {
            throw new NotImplementedException();
        }

        public int InsertReturnID(Transaccion transaccion)
        {
            int insertedId = -1;
            try
            {
                string query = "INSERT INTO TRANSACCION (codi_cliente_t, tipo_transaccion, fecha_transaccion, total_t) VALUES (@codi_cliente_t, @tipo_transaccion, @fecha_transaccion, @total_t);";
                string selectQuery = "SELECT LAST_INSERT_ID();";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@codi_cliente_t", transaccion.Codi_cliente_t);
                    command.Parameters.AddWithValue("@tipo_transaccion", transaccion.Tipo_transaccion);
                    command.Parameters.AddWithValue("@fecha_transaccion", transaccion.Fecha_transaccion);
                    command.Parameters.AddWithValue("@total_t", transaccion.Total_t);

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

        public List<Transaccion> SelectAll()
        {
            List<Transaccion> transacciones = new List<Transaccion>();
            try
            {
                string query = "SELECT * FROM TRANSACCION;";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Transaccion transaccion = new Transaccion();

                            transaccion.Id_transaccion = reader.GetInt32("id_transaccion");
                            transaccion.Codi_cliente_t = reader.GetInt32("codi_cliente_t");
                            transaccion.Tipo_transaccion = (Enums.TipoTransaccionEnum)Enum.Parse(typeof(Enums.TipoTransaccionEnum), reader.GetString("tipo_transaccion"));
                            transaccion.Fecha_transaccion = reader.GetDateTime("fecha_transaccion");
                            transaccion.Total_t = reader.GetDouble("total_t");

                            transacciones.Add(transaccion);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener todas las transacciones: " + ex.Message);
            }
            finally 
            {
                connection.Close();
            }
            return transacciones;
        }

        public Transaccion SelectById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Transaccion transaccion)
        {
            throw new NotImplementedException();
        }
    }
}
