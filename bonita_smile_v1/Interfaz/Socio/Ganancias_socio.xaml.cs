﻿using bonita_smile_v1.Modelos;
using bonita_smile_v1.Servicios;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace bonita_smile_v1.Interfaz.Socio
{
    /// <summary>
    /// Lógica de interacción para Ganancias_Clinica.xaml
    /// </summary>
    public partial class Ganancias_socio : Page
    {
        ObservableCollection<Ganancias> Gganancias;
        private MySqlDataReader reader = null;
        private string query;
        private MySqlConnection conexionBD;
        Conexion obj = new Conexion();
        string valor = "";
        string fecha = "";
        string fecha2 = "";
        public Ganancias_socio(string alias,List<string>lista)
        {
            this.conexionBD = obj.conexion(false);
            InitializeComponent();
            llenar_Combo(alias);
            llena_listview(lista);
        }

        private void llena_listview(List<string> lista)
        {

            var ganancias = new ObservableCollection<Ganancias>(new Servicios.Abonos(false).Mostrar_Ganancias_Socio(lista));

            lv_Gannacias.ItemsSource = ganancias;
            Gganancias = ganancias;

        }

        public void llenar_Combo(string alias)
        {
            query = "select clinica.id_clinica,clinica.nombre_sucursal from usuario left join permisos on usuario.id_usuario=permisos.id_usuario inner join clinica on clinica.id_clinica=permisos.id_clinica where usuario.alias='"+alias+"'";

            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    // ColoresModel coloresModel = new ColoresModel();

                    //coloresModel.id_color = int.Parse(reader[0].ToString());
                    //coloresModel.descripcion = reader[1].ToString();

                    string clinica = reader[1].ToString();
                    cmbClinica.Items.Add(clinica);

                }
            }
            catch (MySqlException ex)
            {
                System.Windows.Forms.MessageBox.Show("Se ha producido un error  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conexionBD.Close();
        }


        private void cmbClinica_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }

        public string obtener_id_clinica(string nombre_sucursal)
        {
            string id = "";
            query = "SELECT id_clinica FROM clinica where nombre_sucursal='" + nombre_sucursal + "'";

            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    id = reader[0].ToString();
                }
            }
            catch (MySqlException ex)
            {
                System.Windows.Forms.MessageBox.Show("Se ha producido un error  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
            conexionBD.Close();

            return id;
        }





        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {


        }

        private void calendario2_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string id_clinica = "";
            try
            {
                
                valor = cmbClinica.SelectedItem.ToString();
                fecha = calendario.SelectedDate.ToString();
                fecha2 = calendario2.SelectedDate.ToString();

                if (fecha2.Equals("") && fecha.Equals("") && !valor.Equals(""))
                {

                    id_clinica = obtener_id_clinica(valor);
                    var ganancias = new ObservableCollection<Ganancias>(new Servicios.Abonos(false).Ganacioas_c_clinica(id_clinica));

                    lv_Gannacias.ItemsSource = ganancias;
                    Gganancias = ganancias;
                    valor = "";
                    fecha2 = "";
                    fecha = "";
                }
                else
                    if (fecha2.Equals("") && !fecha.Equals("") && !valor.Equals(null))
                {

                     id_clinica = obtener_id_clinica(valor);
                    fecha = fecha.Substring(0, fecha.Length - 8);

                    var ganancias = new ObservableCollection<Ganancias>(new Servicios.Abonos(false).Ganacioas_c_clinica_fecha(id_clinica, fecha));

                    lv_Gannacias.ItemsSource = ganancias;
                    Gganancias = ganancias;
                    valor = "";
                    fecha2 = "";
                    fecha = "";
                }
                else
                    if (!fecha2.Equals("") && !fecha.Equals("") && !valor.Equals(null))
                {
                    id_clinica = obtener_id_clinica(valor);
                    fecha = fecha.Substring(0, fecha.Length - 8);
                    fecha2 = fecha2.Substring(0, fecha2.Length - 8);
                    var ganancias = new ObservableCollection<Ganancias>(new Servicios.Abonos(false).Ganacioas_c_clinica_fecha2(id_clinica, fecha, fecha2));

                    lv_Gannacias.ItemsSource = ganancias;
                    Gganancias = ganancias;
                    valor = "";
                    fecha2 = "";
                    fecha = "";
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show("el id de la clinica es :" + id_clinica);
                fecha = calendario.SelectedDate.ToString();
                fecha2 = calendario2.SelectedDate.ToString();

                if (!fecha2.Equals("") && !fecha.Equals("") && id_clinica.Equals(""))
                {
                    System.Windows.Forms.MessageBox.Show("Debe de elegir un id de la clinica ", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    valor = "";
                    fecha2 = "";
                    fecha = "";
                }
                else
                    if (!fecha2.Equals("") && fecha.Equals("") && !id_clinica.Equals(""))
                {
                    System.Windows.Forms.MessageBox.Show("Porfavor elegir la fecha inicial elegir una clinica ", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);

                  
                    valor = "";
                    fecha2 = "";
                    fecha = "";
                }
                else
                    if (!fecha2.Equals("") && fecha.Equals("") && id_clinica.Equals(""))
                {
                    System.Windows.Forms.MessageBox.Show("Porfavor elegir la fecha inicial con el id de la clinica ", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    
                    valor = "";
                    fecha2 = "";
                    fecha = "";
                }
            }




        }
    }
}
