using MySql.Data.MySqlClient;
using ProjecteBBDD.Models;
using System;
using System.Collections.Generic;

namespace ProjecteBBDD.DAO
{
    public class PeliculaDAO : IDAO<Pelicula, int>
    {
        private MySqlConnection connection;

        public PeliculaDAO(MySqlConnection connection)
        {
            this.connection = connection;
        }

        public void Delete(Pelicula pelicula)
        {
            throw new NotImplementedException();
        }

        public void Insert(Pelicula pelicula)
        {
            throw new NotImplementedException();
        }

        public int InsertReturnID(Pelicula pelicula)
        {
            int insertedId = -1;
            try
            {
                string query = "INSERT INTO PELICULA (portada, titulo, genero, duracion, descripcion, precio) VALUES (@portada, @titulo, @genero, @duracion, @descripcion, @precio);";
                string selectQuery = "SELECT LAST_INSERT_ID();";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@portada", pelicula.Portada);
                    command.Parameters.AddWithValue("@titulo", pelicula.Titulo);
                    command.Parameters.AddWithValue("@genero", pelicula.Genero);
                    command.Parameters.AddWithValue("@duracion", pelicula.Duracion);
                    command.Parameters.AddWithValue("@descripcion", pelicula.Descripcion);
                    command.Parameters.AddWithValue("@precio", pelicula.Precio);

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

        public List<Pelicula> SelectAll()
        {
            List<Pelicula> peliculas = new List<Pelicula>();
            try
            {
                string query = "SELECT * FROM PELICULA;";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Pelicula pelicula = new Pelicula();
                            pelicula.Id_pelicula = reader.GetInt32("id_pelicula");
                            pelicula.Portada = reader.GetString("portada");
                            pelicula.Titulo = reader.GetString("titulo");
                            pelicula.Genero = (Enums.GeneroEnum)Enum.Parse(typeof(Enums.GeneroEnum), reader.GetString("genero"));
                            pelicula.Duracion = reader.GetInt32("duracion");
                            pelicula.Descripcion = reader.GetString("descripcion");
                            pelicula.Precio = reader.GetDouble("precio");

                            peliculas.Add(pelicula);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener todas las películas: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return peliculas;
        }

        public Pelicula SelectById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Pelicula pelicula)
        {
            throw new NotImplementedException();
        }
    }
}