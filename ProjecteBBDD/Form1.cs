using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using ProjecteBBDD.DAO;
using ProjecteBBDD.Models;
using ProjecteBBDD.Validators;
using ProjecteBBDD.XmlController;
using System.Data;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace ProjecteBBDD
{
    public partial class Form1 : Form
    {
        // CONSOLA: Importamos una función del sistema para mostrar la consola.
        [DllImport("kernel32.dll")]
        static extern bool AllocConsole();

        // Establecemos la conexión a la base de datos.
        MySqlConnection connection = DataBaseManager.getConnection();

        // Instanciamos la clase que lee archivos XML.
        ReadXml xmlReader = new ReadXml();

        // Constructor de la clase Form1.
        public Form1()
        {
            InitializeComponent();  // Inicializa los componentes de la interfaz gráfica.
            AllocConsole();  // Muestra la consola.
            this.Text = "Cargar fichero XML";  // Cambia el título de la ventana.
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Método que se ejecuta al cargar el formulario.
        }

        private void accionCargarBoton(object sender, EventArgs e)
        {
            // Creamos un cuadro de diálogo para seleccionar un archivo.
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos XML|*.xml";  // Filtramos solo archivos XML.
            openFileDialog.Title = "Seleccionar archivo XML";  // Título del cuadro de diálogo.

            // Si el usuario selecciona un archivo y presiona OK.
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Obtenemos la ruta del archivo seleccionado.
                string xmlFilePath = openFileDialog.FileName;

                // Mostramos la ruta en un TextBox.
                textBox1.Text = xmlFilePath;

                // Validamos el archivo XML contra su DTD.
                bool xmlValidoConDtd = DTDValidator.Validate(xmlFilePath);

                if (xmlValidoConDtd)
                {
                    // Si el archivo es válido, mostramos un mensaje de éxito.
                    MessageBox.Show("El archivo XML es válido con el DTD.", "Validación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Obtenemos los datos de los usuarios, películas y clientes desde el XML.
                    List<Usuario> usuarios = xmlReader.ObtenerUsuariosDesdeXml(xmlFilePath);
                    List<Pelicula> peliculas = xmlReader.ObtenerPeliculasDesdeXml(xmlFilePath);
                    List<Cliente> clientes = xmlReader.ObtenerClientesDesdeXml(xmlFilePath);

                    // Enviamos los datos a la base de datos.
                    xmlReader.EnviarDatos(usuarios, peliculas, clientes);
                }
                else
                {
                    // Si el archivo no es válido, mostramos un mensaje de error.
                    MessageBox.Show("El archivo XML no es válido con el DTD.", "Validación Fallida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void accionCrearXmlBoton(object sender, EventArgs e)
        {
            try
            {
                // Obtenemos la ruta base de la aplicación.
                string basePath = AppDomain.CurrentDomain.BaseDirectory;

                // Construimos la ruta completa donde se guardará el archivo XML.
                string xmlFilePath = Path.Combine(basePath, "..", "..", "..", "Backups", "backupCrearXml.xml");

                // Instanciamos la clase que crea el archivo XML.
                BDXml xmlController = new BDXml(connection);

                // Generamos el archivo XML con los datos de la base de datos.
                xmlController.GetDatosYCrearXml(xmlFilePath);

                // Mostramos un mensaje indicando que el archivo se creó correctamente.
                MessageBox.Show("Archivo XML creado correctamente", "Creación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Mostramos un mensaje de error si ocurre algún problema al crear el archivo XML.
                MessageBox.Show("Error al crear el archivo XML: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void accionResetBD(object sender, EventArgs e)
        {
            try
            {
                // Ejecutamos el script para resetear la base de datos.
                DataBaseManager.EjecutarScript(DataBaseManager.scriptCinema);

                // Mostramos un mensaje indicando que la base de datos se reseteó correctamente.
                MessageBox.Show("Base de datos reseteada correctamente.", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Mostramos un mensaje de error si ocurre algún problema al resetear la base de datos.
                MessageBox.Show("Error al resetear la base de datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void accionStoredProcedure(object sender, EventArgs e)
        {
            try
            {
                // Ejecutamos el script para crear el stored procedure.
                DataBaseManager.EjecutarScript(DataBaseManager.scriptSPGetClienteCMT);

                // Obtenemos el resultado del stored procedure en formato XML.
                string xmlResult = getDades("GetClienteConMasTransaccionesXml");
                Console.WriteLine(xmlResult);

                // Mostramos el resultado en un mensaje.
                MessageBox.Show(xmlResult, "Resultado del stored procedure", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Obtenemos la ruta base de la aplicación.
                string basePath = AppDomain.CurrentDomain.BaseDirectory;

                // Construimos la ruta completa donde se guardará el archivo XML.
                string xmlFilePath = Path.Combine(basePath, "..", "..", "..", "Backups", "spResultado.xml");

                // Guardamos el resultado XML en un archivo.
                File.WriteAllText(xmlFilePath, xmlResult);

                // Cargamos el archivo XML y lo guardamos en el archivo especificado.
                XDocument xmlDocument = XDocument.Parse(xmlResult);
                xmlDocument.Save(xmlFilePath);
            }
            catch (Exception ex)
            {
                // Mostramos un mensaje de error si ocurre algún problema al ejecutar el stored procedure.
                MessageBox.Show("Error al enviar el stored procedure a la base de datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void accionSPBackupBoton(object sender, EventArgs e)
        {
            try
            {
                // Ejecutamos el script para crear el stored procedure.
                DataBaseManager.EjecutarScript(DataBaseManager.scriptSPGetAllDataBDD);

                // Obtenemos el resultado del stored procedure en formato XML.
                string xmlResult = getDades("GetAllDataBDD");
                Console.WriteLine(xmlResult);

                // Mostramos un mensaje indicando que el archivo se creó.
                MessageBox.Show("Stored Procedure", "Archivo creado en", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Obtenemos la ruta base de la aplicación.
                string basePath = AppDomain.CurrentDomain.BaseDirectory;

                // Construimos la ruta completa donde se guardará el archivo XML.
                string xmlFilePath = Path.Combine(basePath, "..", "..", "..", "Backups", "spResultadoGetAllDataBdd.xml");

                // Guardamos el resultado XML en un archivo.
                File.WriteAllText(xmlFilePath, xmlResult);

                // Cargamos el archivo XML y lo guardamos en el archivo especificado.
                XDocument xmlDocument = XDocument.Parse(xmlResult);
                xmlDocument.Save(xmlFilePath);
            }
            catch (Exception ex)
            {
                // Mostramos un mensaje de error si ocurre algún problema al ejecutar el stored procedure.
                MessageBox.Show("Error al enviar el stored procedure a la base de datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void accionTriggerBoton(object sender, EventArgs e)
        {
            try
            {
                // Ejecutamos los scripts para crear los triggers.
                DataBaseManager.EjecutarScript(DataBaseManager.scriptDeleteTriggers);
                DataBaseManager.EjecutarScript(DataBaseManager.scriptTriggerInsert);
                DataBaseManager.EjecutarScript(DataBaseManager.scriptTriggerUpdate);
                DataBaseManager.EjecutarScript(DataBaseManager.scriptTriggerDelete);

                // Mostramos un mensaje indicando que los triggers se crearon correctamente.
                MessageBox.Show("Triggers creados correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Mostramos un mensaje de error si ocurre algún problema al crear los triggers.
                MessageBox.Show($"Error al crear los triggers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static string getDades(string nombreStoredProcedure)
        {
            string resultVariable = string.Empty;
            try
            {
                // Obtenemos la conexión a la base de datos.
                MySqlConnection connection = DataBaseManager.getConnection();
                connection.Open();

                // Creamos un comando para ejecutar el stored procedure.
                using (MySqlCommand cmd = new MySqlCommand(nombreStoredProcedure, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Ejecutamos el stored procedure y leemos los resultados.
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (!reader.IsDBNull(reader.GetOrdinal("XML_Result")))
                            {
                                // Si hay un resultado XML, lo guardamos en la variable.
                                resultVariable = reader.GetString("XML_Result");
                            }
                            else
                            {
                                resultVariable = "No se encontraron datos.";
                            }
                        }
                        else
                        {
                            resultVariable = "No se encontraron datos.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Si ocurre un error, lo mostramos en la consola y lo guardamos en la variable de resultado.
                Console.WriteLine("Error al obtener datos: " + ex.Message);
                resultVariable = "Error al obtener datos: " + ex.Message;
            }
            finally
            {
                // Cerramos la conexión a la base de datos.
                DataBaseManager.CerrarConexion();
            }
            return resultVariable;  // Devolvemos el resultado.
        }
    }
}
