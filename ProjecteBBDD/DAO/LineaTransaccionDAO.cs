using MySql.Data.MySqlClient;
using ProjecteBBDD.Models;
using System;
using System.Collections.Generic;

namespace ProjecteBBDD.DAO
{

    public class LineaTransaccionDAO : IDAO<LineaTransaccion, int>
    {
        private MySqlConnection connection;

        public LineaTransaccionDAO(MySqlConnection connection)
        {
            this.connection = connection;
        }

        public void Delete(LineaTransaccion lineaTransaccion)
        {
            throw new NotImplementedException();
        }

        public void Insert(LineaTransaccion lineaTransaccion, int idTransaccion)
        {
            try
            {
                string query = "INSERT INTO LINEA_TRANSACCION (id_transaccion_lt, id_pelicula_lt, precio_pelicula, cantidad, total_lt) " +
                               "VALUES (@id_transaccion_lt, @id_pelicula_lt, @precio_pelicula, @cantidad, @total_lt);";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id_transaccion_lt", idTransaccion);
                    command.Parameters.AddWithValue("@id_pelicula_lt", lineaTransaccion.Id_pelicula_lt);
                    command.Parameters.AddWithValue("@precio_pelicula", lineaTransaccion.Precio_pelicula);
                    command.Parameters.AddWithValue("@cantidad", lineaTransaccion.Cantidad);
                    command.Parameters.AddWithValue("@total_lt", lineaTransaccion.Total_lt);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar la línea de transacción: " + ex.Message);
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public void Insert(LineaTransaccion lineaTransaccion)
        {
            throw new NotImplementedException();
        }

        public int InsertReturnID(LineaTransaccion lineaTransaccion)
        {
            throw new NotImplementedException();
        }

        public List<LineaTransaccion> SelectAll()
        {
            List<LineaTransaccion> LineasTransacciones = new List<LineaTransaccion>();
            try
            {
                string query = "SELECT * FROM LINEA_TRANSACCION;";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LineaTransaccion lineaTransaccion = new LineaTransaccion();

                            lineaTransaccion.Id_transaccion_lt = reader.GetInt32("id_transaccion_lt");
                            lineaTransaccion.Id_linea_transaccion = reader.GetInt32("id_linea_transaccion");
                            lineaTransaccion.Id_pelicula_lt = reader.GetInt32("id_pelicula_lt");
                            lineaTransaccion.Precio_pelicula = reader.GetDouble("precio_pelicula");
                            lineaTransaccion.Cantidad = reader.GetInt32("cantidad");
                            lineaTransaccion.Total_lt = reader.GetDouble("total_lt");

                            LineasTransacciones.Add(lineaTransaccion);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener todas las lineas de transacciones: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return LineasTransacciones;
        }

        public LineaTransaccion SelectById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(LineaTransaccion lineaTransaccion)
        {
            throw new NotImplementedException();
        }
    }
}
