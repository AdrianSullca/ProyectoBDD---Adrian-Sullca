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
        //CONSOLA
        [DllImport("kernel32.dll")]
        static extern bool AllocConsole();
        //CONSOLA
        MySqlConnection connection = DataBaseManager.getConnection();
        ReadXml xmlReader = new ReadXml();
        public Form1()
        {
            InitializeComponent();
            AllocConsole();
            this.Text = "Cargar fichero XML";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void accionCargarBoton(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos XML|*.xml";
            openFileDialog.Title = "Seleccionar archivo XML";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string xmlFilePath = openFileDialog.FileName;

                textBox1.Text = xmlFilePath;

                bool xmlValidoConDtd = DTDValidator.Validate(xmlFilePath);

                if (xmlValidoConDtd)
                {
                    MessageBox.Show("El archivo XML es válido con el DTD.", "Validación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    List<Usuario> usuarios = xmlReader.ObtenerUsuariosDesdeXml(xmlFilePath);
                    List<Pelicula> peliculas = xmlReader.ObtenerPeliculasDesdeXml(xmlFilePath);
                    List<Cliente> clientes = xmlReader.ObtenerClientesDesdeXml(xmlFilePath);

                    xmlReader.EnviarDatos(usuarios, peliculas, clientes);
                }
                else
                {
                    MessageBox.Show("El archivo XML no es válido con el DTD.", "Validación Fallida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void accionCrearXmlBoton(object sender, EventArgs e)
        {
            try
            {

                string basePath = AppDomain.CurrentDomain.BaseDirectory;

                string xmlFilePath = Path.Combine(basePath, "..", "..", "..", "Backups", "backup.xml");

                BDXml xmlController = new BDXml(connection);

                xmlController.GetDatosYCrearXml(xmlFilePath);

                MessageBox.Show("Archivo XML creado correctamente", "Creación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear el archivo XML: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void accionResetBD(object sender, EventArgs e)
        {
            try
            {
                DataBaseManager.EjecutarScript(DataBaseManager.scriptCinema);
                MessageBox.Show("Base de datos reseteada correctamente.", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al resetear la base de datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void accionStoredProcedure(object sender, EventArgs e)
        {
            try
            {
                DataBaseManager.EjecutarScript(DataBaseManager.scriptSPGetClienteCMT);
                string xmlResult = getDades("GetClienteConMasTransaccionesXml");
                Console.WriteLine(xmlResult);
                MessageBox.Show(xmlResult, "Resultado del stored procedure", MessageBoxButtons.OK, MessageBoxIcon.Information);

                string basePath = AppDomain.CurrentDomain.BaseDirectory;

                string xmlFilePath = Path.Combine(basePath, "..", "..", "..", "Backups", "procedureResultado.xml");

                string folderPath = Path.GetDirectoryName(xmlFilePath);
                File.WriteAllText(xmlFilePath, xmlResult);

                XDocument xmlDocument = XDocument.Parse(xmlResult);

                xmlDocument.Save(xmlFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al enviar el stored procedure a la base de datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void accionSPBackupBoton(object sender, EventArgs e)
        {
            try
            {
                DataBaseManager.EjecutarScript(DataBaseManager.scriptSPGetAllDataBDD);
                string xmlResult = getDades("GetAllDataBDD");
                Console.WriteLine(xmlResult);
                MessageBox.Show("Stored Procedure", "Archivo creado en", MessageBoxButtons.OK, MessageBoxIcon.Information);
                string basePath = AppDomain.CurrentDomain.BaseDirectory;

                string xmlFilePath = Path.Combine(basePath, "..", "..", "..", "Backups", "resultProcedureGetAllDataBdd.xml");

                string folderPath = Path.GetDirectoryName(xmlFilePath);
                File.WriteAllText(xmlFilePath, xmlResult);

                XDocument xmlDocument = XDocument.Parse(xmlResult);

                xmlDocument.Save(xmlFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al enviar el stored procedure a la base de datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void accionTriggerBoton(object sender, EventArgs e)
        {
            try
            {
                DataBaseManager.EjecutarScript(DataBaseManager.scriptDeleteTriggers);
                DataBaseManager.EjecutarScript(DataBaseManager.scriptTriggerInsert);
                DataBaseManager.EjecutarScript(DataBaseManager.scriptTriggerUpdate);
                DataBaseManager.EjecutarScript(DataBaseManager.scriptTriggerDelete);

                MessageBox.Show("Triggers creados correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear los triggers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static string getDades(string nombreStoredProcedure)
        {
            string resultVariable = string.Empty;
            try
            {
                MySqlConnection connection = DataBaseManager.getConnection();
                connection.Open();
                using (MySqlCommand cmd = new MySqlCommand(nombreStoredProcedure, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (!reader.IsDBNull(reader.GetOrdinal("XML_Result")))
                            {
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
                Console.WriteLine("Error al obtener datos: " + ex.Message);
                resultVariable = "Error al obtener datos: " + ex.Message;
            }
            finally
            {
                DataBaseManager.CerrarConexion();
            }
            return resultVariable;
        }
    }
}
