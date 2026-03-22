using System;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace ConexionSQL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //Funcion de conexion al hacer click en el boton, donde se toman los datos ingresados por el
        //usuario para establecer la conexion con la base de datos y mostrar los datos de la tabla
        //CatPersonal en el DataGridView
        private void btnConexion_Click(object sender, EventArgs e)
        {
            // Conversion de los datos ingresados a tipo String para futuras referencias
            string host = textHost.Text;
            string user = textUsuario.Text;
            string password = textContraseña.Text;
            string puerto = textPuerto.Text;
            string database = textBD.Text;

            // Sentencia para establecer conexion con la BD, pasando los atributos necesarios
            string connectionString = BuildConnectionString(host, user, password, puerto, database);

            //Funcion de conexion a la base de datos, usando el string de conexion construido
            //previamente, y mostrando los datos de la tabla CatPersonal en el DataGridView
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM CatPersonal", con);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    DgvDatos.DataSource = dt;
                    // Sentencia para lograr la visualizacion de las filas de la tabla CatPersonal
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Ha ocurrido un error: " + ex.Message);

                }
                // Mensaje de error, en caso de no poder establecer conexion y mostrar los datos en el grid.
            }
        }

        // Sentencia que establece la conexion con la base de datos, usando como parametros los datos
        // ingresados por el usuario
        private string BuildConnectionString(string host, string user, string password, string puerto, string database)
        {
            return $"SERVER={host};PORT={puerto};DATABASE={database};USER ID={user};PASSWORD={password};";
        }
        
    }
}