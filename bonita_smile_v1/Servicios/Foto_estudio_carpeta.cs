﻿using bonita_smile_v1.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace bonita_smile_v1.Servicios
{
    class Fotos_estudio_carpeta
    {
        //private string ruta2 = @"\\DESKTOP-ED8E774\bs\";
        string ruta2;
        private MySqlDataReader reader = null;
        private string query;
        private MySqlConnection conexionBD;
        Conexion obj = new Conexion();
        Test_Internet ti = new Test_Internet();
        private bool online;
        string ruta_archivo = System.IO.Path.Combine(@Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"dentista\setup\conf\configuracion.txt");
        Configuracion_Model configuracion;
        public Fotos_estudio_carpeta(bool online)
        {
            Archivo_Binario ab = new Archivo_Binario();
            Configuracion_Model configuracion = ab.Cargar(ruta_archivo);
            this.ruta2 = @configuracion.carpetas.ruta_imagenes_carpeta + "\\";
            this.configuracion = configuracion;
            this.conexionBD = obj.conexion(online);
            this.online = online;
        }

        public List<Fotos_estudio_carpetaModel> MostrarFoto_estudio_carpeta(string id_carpeta, string id_paciente,string id_motivo,string id_clinica)
        {
            List<Fotos_estudio_carpetaModel> listaFoto_estudio_carpeta = new List<Fotos_estudio_carpetaModel>();
            query = "SELECT  * FROM fotos_estudio_carpeta where id_carpeta='" + id_carpeta + "' and id_paciente='" + id_paciente + "' and id_motivo='"+id_motivo+"' and id_clinica='"+id_clinica+"'";
            Console.WriteLine(query);
            //MessageBox.Show(query);
            string foto_recortada = "";
            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Fotos_estudio_carpetaModel fotos_Estudio_CarpetaModel = new Fotos_estudio_carpetaModel();

                    fotos_Estudio_CarpetaModel.id_foto = reader[0].ToString();
                    fotos_Estudio_CarpetaModel.id_carpeta = reader[1].ToString();
                    fotos_Estudio_CarpetaModel.id_paciente = reader[2].ToString();
                    foto_recortada = reader[3].ToString();
                    foto_recortada = foto_recortada.Substring(35);
                    fotos_Estudio_CarpetaModel.foto = foto_recortada;
                    fotos_Estudio_CarpetaModel.foto_completa = reader[3].ToString();
                    fotos_Estudio_CarpetaModel.imagen = LoadImage(@configuracion.carpetas.ruta_imagenes_carpeta + "\\" + reader[3].ToString());
                    fotos_Estudio_CarpetaModel.id_motivo = reader[4].ToString();
                    fotos_Estudio_CarpetaModel.id_clinica = reader[5].ToString();
                    //MessageBox.Show(@configuracion.carpetas.ruta_imagenes_carpeta + "\\" + reader[3].ToString());
                    listaFoto_estudio_carpeta.Add(fotos_Estudio_CarpetaModel);
                }
            }
            catch (MySqlException ex)
            {
                System.Windows.Forms.MessageBox.Show("Se ha producido un error ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conexionBD.Close();
            return listaFoto_estudio_carpeta;

        }

        public bool eliminarFoto_estudio_carpeta(string id_foto,string id_carpeta,string id_paciente,string id_motivo,string id_clinica, string alias)
        {
            try
            {

                MySqlCommand cmd; ;
                if (online)
                {
                    bool internet = ti.Test();

                    if (!internet)
                    {
                        //EN CASO DE REALIZAR UNA PETICION PARA ELIMINAR EN SERVIDOR VERIFICAR SI HAY INTERNET, SI NO LO HAY, ENTONCES NO HACER NADA Y SEGUIR MANTENIENDO QUERIES EN EL ARCHIVO
                        return false;
                    }
                    else
                    {
                        //EN CASO DE REALIZAR UNA PETICION PARA ELIMINAR EN SERVIDOR VERIFICAR SI HAY INTERNET, SI LO HAY, ENTONCES INSERTAR TODOS LOS QUERIES DEL ARCHIVO

                        Sincronizar sincronizar = new Sincronizar();
                        sincronizar.insertarArchivoEnServidor(conexionBD);
                        return true;
                    }
                }
                else
                {
                    query = "DELETE FROM fotos_estudio_carpeta where id_foto='" + id_foto + "' and id_carpeta='"+id_carpeta+"' and id_paciente='"+id_paciente+"' and id_motivo='"+id_motivo+"' and id_clinica='"+id_clinica+"'";

                    conexionBD.Open();
                    cmd = new MySqlCommand(query, conexionBD);
                    cmd.ExecuteReader();
                    conexionBD.Close();

                    Escribir_Archivo ea = new Escribir_Archivo();
                    ea.escribir_imagen_eliminar(query + ";", @configuracion.carpetas.ruta_script_carpeta + "\\script_temporal_" + alias + ".txt");
                    System.Windows.Forms.MessageBox.Show("Se actualizó correctamente la  Foto: ", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                //return true;
            }
            catch (MySqlException ex)
            {
                System.Windows.Forms.MessageBox.Show("Se ha producido un error al intentar eliminar ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conexionBD.Close();
                return false;
            }
        }

        public List<string> MostrarFoto_Clinica(string id_clinica)
        {
            List<string> lista_foto_clinica = new List<string>();
            query = "(select foto from fotos_estudio_carpeta where id_paciente in (select paciente.id_paciente from paciente inner join clinica on clinica.id_clinica=paciente.id_clinica where clinica.id_clinica='" + id_clinica + "'))union (select paciente.foto from paciente inner join clinica on clinica.id_clinica=paciente.id_clinica where clinica.id_clinica='" + id_clinica + "')";
            //MessageBox.Show(query);
            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (reader[0].ToString().Equals(""))
                    {
                    }
                    else
                    {
                        lista_foto_clinica.Add(reader[0].ToString());
                    }

                }
            }
            catch (MySqlException ex)
            {
                System.Windows.Forms.MessageBox.Show("Se ha producido un error  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conexionBD.Close();
            return lista_foto_clinica;
        }

        public List<Fotos_estudio_carpetaModel> MostrarFoto_Paciente(string id_paciente,string id_clinica )
        {
            List<Fotos_estudio_carpetaModel> listaFoto_estudio_carpeta = new List<Fotos_estudio_carpetaModel>();
            query = "SELECT  * FROM fotos_estudio_carpeta where id_paciente='" + id_paciente + "' and id_clinica='"+id_clinica+"'";
            //MessageBox.Show(query);
            string foto_recortada = "";
            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Fotos_estudio_carpetaModel fotos_Estudio_CarpetaModel = new Fotos_estudio_carpetaModel();

                    fotos_Estudio_CarpetaModel.id_foto = reader[0].ToString();
                    fotos_Estudio_CarpetaModel.id_carpeta = reader[1].ToString();
                    fotos_Estudio_CarpetaModel.id_paciente = reader[2].ToString();
                    foto_recortada = reader[3].ToString();
                    foto_recortada = foto_recortada.Substring(35);
                    fotos_Estudio_CarpetaModel.foto = foto_recortada;
                    fotos_Estudio_CarpetaModel.foto_completa = reader[3].ToString();
                    fotos_Estudio_CarpetaModel.imagen = LoadImage(@configuracion.carpetas.ruta_imagenes_carpeta + "\\" + reader[3].ToString());
                    fotos_Estudio_CarpetaModel.id_motivo = reader[4].ToString();
                    fotos_Estudio_CarpetaModel.id_clinica = reader[5].ToString();
                    listaFoto_estudio_carpeta.Add(fotos_Estudio_CarpetaModel);
                }
            }
            catch (MySqlException ex)
            {
                System.Windows.Forms.MessageBox.Show("Se ha producido un error ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conexionBD.Close();
            return listaFoto_estudio_carpeta;
        }

        public bool insertarFoto_estudio_carpeta(string id_carpeta, string id_paciente, string foto,string id_motivo,string id_clinica, string alias)
        {
            string id_foto = "";
            Seguridad seguridad = new Seguridad();
            id_foto = seguridad.SHA1(id_carpeta + id_paciente + foto);
            try
            {

                MySqlCommand cmd; ;
                if (online)
                {
                    bool internet = ti.Test();

                    if (!internet)
                    {
                        //EN CASO DE REALIZAR UNA PETICION PARA INSERTAR EN SERVIDOR VERIFICAR SI HAY INTERNET, SI NO LO HAY, ENTONCES NO HACER NADA Y SEGUIR MANTENIENDO QUERIES EN EL ARCHIVO 
                        return false;
                    }
                    else
                    {
                        //EN CASO DE REALIZAR UNA PETICION PARA INSERTAR EN SERVIDOR VERIFICAR SI HAY INTERNET, SI LO HAY, ENTONCES INSERTAR TODOS LOS QUERIES DEL ARCHIVO

                        //query = "INSERT INTO usuario (id_usuario,alias,nombre,apellidos,password,id_rol) VALUES('" + auxiliar_identificador + "','" + alias + "','" + nombre + "','" + apellidos + "','" + password + "'," + id_rol + ")";
                        Sincronizar sincronizar = new Sincronizar();
                        sincronizar.insertarArchivoEnServidor(conexionBD);
                        return true;
                    }
                }
                else
                {
                    query = "INSERT INTO fotos_estudio_carpeta (id_foto,id_carpeta,id_paciente,foto,id_motivo,id_clinica) VALUES('" + id_foto + "','" + id_carpeta + "','" + id_paciente + "','" + foto + "','" + id_motivo + "','"+id_clinica+"')";
                    Console.WriteLine(query);
                    conexionBD.Open();
                    cmd = new MySqlCommand(query, conexionBD);
                    cmd.ExecuteReader();
                    conexionBD.Close();

                    Escribir_Archivo ea = new Escribir_Archivo();
                    ea.escribir_imagen_eliminar(query + ";", @configuracion.carpetas.ruta_script_carpeta + "\\script_temporal_" + alias + ".txt");
                    System.Windows.Forms.MessageBox.Show("Se insertó correctamente la Foto: ", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            }
            catch (MySqlException ex)
            {
                System.Windows.Forms.MessageBox.Show("Se ha producido un error  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conexionBD.Close();
                return false;
            }
        }


        public bool actualizarFoto_estudio_carpeta(string id_foto, string id_carpeta, string id_paciente, string foto,string id_motivo,string id_clinica ,string alias)
        {

            try
            {

                MySqlCommand cmd; ;
                if (online)
                {
                    bool internet = ti.Test();

                    if (!internet)
                    {
                        //EN CASO DE REALIZAR UNA PETICION PARA ACTUALIZAR EN SERVIDOR VERIFICAR SI HAY INTERNET, SI NO LO HAY, ENTONCES NO HACER NADA Y SEGUIR MANTENIENDO QUERIES EN EL ARCHIVO 
                        return false;
                    }
                    else
                    {
                        //EN CASO DE REALIZAR UNA PETICION PARA INSEACTUALIZARRTAR EN SERVIDOR VERIFICAR SI HAY INTERNET, SI LO HAY, ENTONCES INSERTAR TODOS LOS QUERIES DEL ARCHIVO

                        //query = "UPDATE usuario set alias = '" + alias + "',nombre = '" + nombre + "',apellidos = '" + apellidos + "',password = '" + password + "',id_rol = " + id_rol + " where id_usuario = '" + id_usuario + "'";
                        Sincronizar sincronizar = new Sincronizar();
                        sincronizar.insertarArchivoEnServidor(conexionBD);
                        return true;
                    }
                }
                else
                {
                    //string auxiliar_identificador = MostrarUsuario_Update(id_usuario);
                    query = "UPDATE fotos_estudio_carpeta set foto = '" + foto  + "' where id_foto = '" + id_foto + "' and id_carpeta='"+id_carpeta+"' and id_paciente='"+id_paciente+"' and id_motivo='"+id_motivo+"' and id_clinica='"+id_clinica+"'";

                    conexionBD.Open();
                    cmd = new MySqlCommand(query, conexionBD);
                    cmd.ExecuteReader();
                    conexionBD.Close();

                    Escribir_Archivo ea = new Escribir_Archivo();
                    ea.escribir_imagen_eliminar(query + ";", @configuracion.carpetas.ruta_script_carpeta + "\\script_temporal_" + alias + ".txt");
                    System.Windows.Forms.MessageBox.Show("Se actualizó correctamente la Foto: ", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }

            }
            catch (MySqlException ex)
            {
                System.Windows.Forms.MessageBox.Show("Se ha producido un error al intentar actualizar ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conexionBD.Close();
                return false;
            }
        }

        


        /**PASAR A OTRA CLASE***/
        public BitmapImage LoadImage(string filename)
        {
            if (File.Exists(filename))
            {
                //MessageBox.Show("Existe la foto");
                var bitmap = new BitmapImage();
                var stream = File.OpenRead(filename);
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.StreamSource = stream;
                bitmap.EndInit();
                stream.Close();
                stream.Dispose();

                return bitmap;
            }
            else
            {
               // MessageBox.Show(" No Existe la foto");
                var bitmap = new BitmapImage();
                var stream = File.OpenRead(System.IO.Path.Combine(@System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName, @"..\..\..\Assets\img1.jpg"));
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.StreamSource = stream;
                bitmap.EndInit();
                stream.Close();
                stream.Dispose();

                return bitmap;
            }
        }
        /*public void fotos(string id_carpeta, string id_paciente)
        {
            string fotito = "";
            List<string> listaFoto_estudio_carpeta = new List<string>();
            query = "SELECT  foto FROM fotos_estudio_carpeta where id_carpeta='" + id_carpeta + "' and id_paciente='" + id_paciente+"'";

            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {



                    fotito = reader[0].ToString();

                    listaFoto_estudio_carpeta.Add(fotito);
                }
                foreach (var l in listaFoto_estudio_carpeta)
                {
                    bool descargo = downloadFile("ftp://jjdeveloperswdm.com/", "bonita_smile@jjdeveloperswdm.com", "bonita_smile", l, ruta2 + l, 10);
                    if (descargo)
                    {
                        System.Windows.MessageBox.Show(":)");

                    }
                    else
                    {
                        System.Windows.MessageBox.Show(":(");
                    }
                }

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexionBD.Close();
            //return listaFoto_estudio_carpeta;

        }*/


    }
}
