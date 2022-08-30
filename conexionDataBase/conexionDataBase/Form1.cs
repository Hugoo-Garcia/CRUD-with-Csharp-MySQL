using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace conexionDataBase
{
    public partial class Form1 : Form
    {
        MySqlConnection conex = new MySqlConnection();
        static string servidor = "localhost";
        static string bd = "ita20151687";
        static string usuario = "root";
        static string password = "12345678";
        static string port = "3306";

        string cadenaConexion = "server=" + servidor + ";" + "port=" + port + ";" + "user id=" + usuario + ";" + "password=" + password + ";" + "database=" + bd + ";";

        MySqlCommand cmd;
        MySqlDataAdapter adapt;

        //Variable usada para actualizacion y eliminacion de registros
        int ID = 0;

        public Form1()
        {
            InitializeComponent();
            DisplayData();
        }

        //Insertar datos
        private void btn_insert_Click(object sender, EventArgs e)
        {
            if (txt_name.Text != "" && txt_state.Text != "")
            {
                cmd = new MySqlCommand("insert into records(Nombre, Estado, Boleano, Fecha) values(@Nombre, @Estado, @Boleano, @Fecha)", conex);
                conex.ConnectionString = cadenaConexion;
                conex.Open();
                cmd.Parameters.AddWithValue("@Nombre", txt_name.Text);
                cmd.Parameters.AddWithValue("@Estado", txt_state.Text);
                if (checkBox1.Checked)
                {
                    cmd.Parameters.AddWithValue("@Boleano", 1);
                }else
                {
                    cmd.Parameters.AddWithValue("@Boleano", 0);
                }
                cmd.Parameters.AddWithValue("@Fecha", dateTimePicker1.Value.Year.ToString()+"-"+dateTimePicker1.Value.Month.ToString()+"-"+dateTimePicker1.Value.Day.ToString());

                cmd.ExecuteNonQuery();
                MessageBox.Show("Registros insertados correctamente!");
                conex.Close();
                DisplayData();
                ClearData();
            } else
            {
                MessageBox.Show("Los campos estan vacios");
            }
        }

        //Mostrar los datos en el DataGridView
        private void DisplayData()
        {
            conex.ConnectionString = cadenaConexion;
            conex.Open();
            DataTable dt = new DataTable();
            adapt = new MySqlDataAdapter("select * from records", conex);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            conex.Close();
        }
        //Limpiar datos
        private void ClearData()
        {
            txt_name.Text = "";
            txt_state.Text = "";
            ID = 0;
        }

        //Seleccionar fila
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            txt_name.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txt_state.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        //Actualizar registro
        private void btn_update_Click(object sender, EventArgs e)
        {
            if (txt_name.Text != "" && txt_state.Text != "")
            {
                cmd = new MySqlCommand("update records set Nombre=@Nombre, Estado=@Estado, Boleano=@Boleano, Fecha=@Fecha where ID=@ID", conex);
                conex.ConnectionString = cadenaConexion;
                conex.Open();
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@Nombre", txt_name.Text);
                cmd.Parameters.AddWithValue("@Estado", txt_state.Text);
                if (checkBox1.Checked)
                {
                    cmd.Parameters.AddWithValue("@Boleano", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Boleano", 0);
                }
                cmd.Parameters.AddWithValue("@Fecha", dateTimePicker1.Value.Year.ToString() + "-" + dateTimePicker1.Value.Month.ToString() + "-" + dateTimePicker1.Value.Day.ToString());
                cmd.ExecuteNonQuery();
                conex.Close();
                MessageBox.Show("Registro actualizado exitosamente");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Modifique la informacion");
            }

        }

        //Eliminar registro
        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (ID != 0 )
            {
                cmd = new MySqlCommand("delete from records where ID=@ID", conex);
                conex.ConnectionString = cadenaConexion;
                conex.Open();
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.ExecuteNonQuery();
                conex.Close();
                MessageBox.Show("Registro eliminado exitosamente");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Seleccione un registro para eliminar");
            }
        }


    } 
}

