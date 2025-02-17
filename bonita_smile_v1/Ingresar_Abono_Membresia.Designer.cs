﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;

using iTextSharp.text.pdf;

using System.IO;
using System.Windows;
using SystemColors = System.Drawing.SystemColors;
using System.Drawing.Printing;
using bonita_smile_v1.Servicios;
using System.Globalization;
using bonita_smile_v1.Modelos;
using MySql.Data.MySqlClient;

namespace bonita_smile_v1
{
    partial class Ingresar_Abono_Membresia
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        string id_motivo = "";
        string id_paciente = "";
        string nombre = "";
        string motivo = "";
        double restante = 0.0;
        double abonado = 0.0;
        double total = 0.0;
        bool bandera_online_offline = false;
        PacienteModel paciente;
        CultureInfo culture = new CultureInfo("en-US");
        private MySqlDataReader reader = null, reader2 = null;
        private string query, query2;
        private MySqlConnection conexionBD, conexionBD2;
        Conexion obj = new Conexion();
        Configuracion_Model configuracion;
        string ruta = Path.Combine(@Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"dentista\setup\conf\configuracion.txt");
        string alias;
        string id_membresia;
        public Ingresar_Abono_Membresia(PacienteModel paciente,string id_membresia,double total,double restante,double abonado, string alias)
        {
            this.id_motivo = id_motivo;
            this.id_paciente = id_paciente;
            this.nombre = nombre;
            this.motivo = motivo;
            this.restante = Convert.ToDouble(restante.ToString(culture), culture);
            this.abonado = Convert.ToDouble(abonado.ToString(culture), culture);
            this.total = Convert.ToDouble(total.ToString(culture), culture);
            this.paciente = paciente;
            this.conexionBD = obj.conexion(bandera_online_offline);
            Archivo_Binario ab = new Archivo_Binario();
            Configuracion_Model configuracion = ab.Cargar(ruta);
            this.configuracion = configuracion;
            this.alias = alias;
            this.id_membresia = id_membresia;
            InitializeComponent();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblAbono = new System.Windows.Forms.Label();
            this.txtAbono = new System.Windows.Forms.TextBox();
            this.txtComentario = new System.Windows.Forms.TextBox();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelat = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_efectivo = new System.Windows.Forms.TextBox();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.SuspendLayout();
            // 
            // lblAbono
            // 
            this.lblAbono.AutoSize = true;
            this.lblAbono.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAbono.Location = new System.Drawing.Point(60, 15);
            this.lblAbono.Name = "lblAbono";
            this.lblAbono.Size = new System.Drawing.Size(83, 29);
            this.lblAbono.TabIndex = 0;
            this.lblAbono.Text = "Abono";
            this.lblAbono.Click += new System.EventHandler(this.lblAbono_Click);
            // 
            // txtAbono
            // 
            this.txtAbono.Location = new System.Drawing.Point(174, 15);
            this.txtAbono.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtAbono.Name = "txtAbono";
            this.txtAbono.Size = new System.Drawing.Size(235, 26);
            this.txtAbono.TabIndex = 1;
            this.txtAbono.TextChanged += new System.EventHandler(this.txtAbono_TextChanged);
            // 
            // txtComentario
            // 
            this.txtComentario.Location = new System.Drawing.Point(65, 70);
            this.txtComentario.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtComentario.Multiline = true;
            this.txtComentario.Name = "txtComentario";
            this.txtComentario.Size = new System.Drawing.Size(500, 183);
            this.txtComentario.TabIndex = 2;
            this.txtComentario.TextChanged += new System.EventHandler(this.txtComentario_TextChanged);
            // 
            // btnAceptar
            // 
            this.btnAceptar.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAceptar.Location = new System.Drawing.Point(65, 435);
            this.btnAceptar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(199, 61);
            this.btnAceptar.TabIndex = 3;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelat
            // 
            this.btnCancelat.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnCancelat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelat.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCancelat.Location = new System.Drawing.Point(375, 435);
            this.btnCancelat.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancelat.Name = "btnCancelat";
            this.btnCancelat.Size = new System.Drawing.Size(190, 61);
            this.btnCancelat.TabIndex = 4;
            this.btnCancelat.Text = "Cancelar";
            this.btnCancelat.UseVisualStyleBackColor = false;
            this.btnCancelat.Click += new System.EventHandler(this.btnCancelat_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(60, 304);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 29);
            this.label1.TabIndex = 5;
            this.label1.Text = "Efectivo";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // txt_efectivo
            // 
            this.txt_efectivo.Location = new System.Drawing.Point(174, 304);
            this.txt_efectivo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_efectivo.Name = "txt_efectivo";
            this.txt_efectivo.Size = new System.Drawing.Size(235, 26);
            this.txt_efectivo.TabIndex = 6;
            this.txt_efectivo.TextChanged += new System.EventHandler(this.txt_efectivo_TextChanged);
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // Ingresar_Abono_Membresia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(633, 518);
            this.Controls.Add(this.txt_efectivo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancelat);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.txtComentario);
            this.Controls.Add(this.txtAbono);
            this.Controls.Add(this.lblAbono);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Ingresar Abono Membresia";
            this.Text = "Ingresar Abono Membresia";
            this.Load += new System.EventHandler(this.MessageBoxAbono_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private void btnCancelat_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!txtAbono.Text.Equals("") && !txt_efectivo.Text.Equals("") )
            {
                if (new Seguridad().validar_numero(txtAbono.Text) && new Seguridad().validar_numero(txt_efectivo.Text))
                {

                    string comentario = txtComentario.Text;

                    DateTime fecha = DateTime.Now;
                    //double costo =  Convert.ToDouble(txt_efectivo.Text, culture);
                    double abono = Convert.ToDouble(txtAbono.Text, culture);
                    double efectivo = Convert.ToDouble(txt_efectivo.Text, culture);
                    //System.Windows.MessageBox.Show("el restante es " + restante);
                    //System.Windows.MessageBox.Show("el abono es de " + abono);
                    double cambio = efectivo - abono;

                    if (efectivo >= abono && abono > 0)
                    {

                       // System.Windows.MessageBox.Show("abono es " + abono);
                       // System.Windows.MessageBox.Show("restante es " + restante);
                        if (abono <= restante && restante > 0.0)
                        {

                            Abonos_Membresia ab = new Abonos_Membresia(bandera_online_offline);
                            bool insertarAbono = ab.InsertarAbonoMembresia(fecha.ToString("yyyy/MM/dd"), abono.ToString(culture), comentario,id_membresia, paciente.id_paciente,paciente.clinica.id_clinica,alias);
                            if (insertarAbono)
                            {
                                //ab = new Abonos(!bandera_online_offline);
                                //ab.insertarAbono(id_paciente, id_motivo, fecha.ToString("yyyy/MM/dd"), abono.ToString(culture), comentario);
                                //System.Windows.Forms.MessageBox.Show("Se registro Correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //System.Windows.Forms.MessageBox.Show("El cambio es de " + cambio, "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //System.Windows.Forms.MessageBox.Show("Se esta imprimiendo el recibo", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //imprimir_recibo(fecha.ToString("yyyy/MM/dd"), nombre, abono, motivo, restante, cambio);

                                imprimir_recibo();
                            }
                            else
                            {
                               // System.Windows.Forms.MessageBox.Show("No se pudo realizar el pago", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            this.DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("El abono excede el precio del tratamiento", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            //AGREGAR SI DESEA CONTINUAR
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Abono mayor que efectivo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        //AGREGAR SI DESEA CONTINUAR
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Solo se aceptan valores numericos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Favor de llenar los campos de abono y efectivo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




        }

        public void imprimir_recibo()
        {
            //double rest = restante - abono;

            // Document doc = new Document();
            // PdfWriter.GetInstance(doc, new FileStream(@"\\DESKTOP-ED8E774\bs\prueba.pdf", FileMode.Create));
            // doc.Open();

            // Paragraph title = new Paragraph();
            // title.Font = FontFactory.GetFont(FontFactory.TIMES, 18f, BaseColor.BLUE);
            // title.Add("Recibo de Pago de Bonita Smile");
            // doc.Add(title);


            // doc.Add(new Paragraph("Bonita Smile "));
            // doc.Add(new Paragraph("Se realiza un pago el dia-------------  " +fecha));
            // doc.Add(new Paragraph("Nombre del Paciente -------------------- " +nombre));
            // doc.Add(new Paragraph("El pago es de -------------------------- " +motivo));
            // //doc.Add(new Paragraph("El restante es de ---------------------- $ "+restante));
            // doc.Add(new Paragraph("El abono a pagar es de ----------------- $ "+abono));
            // doc.Add(new Paragraph("Su cambio es de ---------------------$" + camb));
            ///* if(rest==0.0)
            // {
            //     doc.Add(new Paragraph("Su Cuenta ya fue liquidada $ " + rest));
            // }
            // else
            // {
            //     doc.Add(new Paragraph("Lo restante que le falta de pagar es de ---- $ " + rest));
            // }*/


            // doc.Add(new Paragraph("---------------------------------------------------------"));
            // doc.Close();

            printDocument1 = new PrintDocument();
            PrinterSettings ps = new PrinterSettings();
            printDocument1.PrinterSettings = ps;
            printDocument1.PrintPage += new PrintPageEventHandler(Imprimir);

            //printDocument1.PrinterSettings.PrinterName = "HPFEF3CF (HP Officejet Pro 6830) (Red)";
            printDocument1.PrinterSettings.PrinterName = configuracion.ftp.nombre_impresora;
            //printDocument1.PrinterSettings.PrinterName = "Microsoft XPS Document Writer";
            //poner try catch
            if (printDocument1.PrinterSettings.IsValid)
            {
                printDocument1.Print();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Impresora no válida. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
            }
        }
        float centimetroAPixel(double Centimeter)
        {
            double pixel = -1;
            using (Graphics g = this.CreateGraphics())
            {
                pixel = Centimeter * g.DpiY / 2.54d;
            }
            return (int)pixel;
        }

        /*MÉTODO PARA IMPRIMIR*/

        /*MÉTODO PARA IMPRIMIR*/

        private void Imprimir(object sender, PrintPageEventArgs e)
        {
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;

            System.Drawing.Font font = new System.Drawing.Font("Courier New", 12, System.Drawing.FontStyle.Bold);
            System.Drawing.Font titulo = new System.Drawing.Font("Courier New", 12, System.Drawing.FontStyle.Bold);
            System.Drawing.Font cuerpo = new System.Drawing.Font("Courier New", 9);
            System.Drawing.Font final = new System.Drawing.Font("Courier New", 6);

            // MODIFICADORES DE FORMATO DE HOJA/
            float margen_izquierdo = 0;
            float margen_superior = 10;
            double margen_cuerpo = 0.42; //DESPUES DEL TITULO
            double tamanio_hoja_horizontal = 3.6;
            // ----------------------------------/

            double cambio = (Convert.ToDouble(this.txt_efectivo.Text)) - (Convert.ToDouble(this.txtAbono.Text));
            System.Windows.MessageBox.Show("el cambio es :" + cambio);
            //double restante = this.restante - (Int32.Parse(this.txtAbono.Text));

            string fecha_inicio = DateTime.Now.ToString("d/M/yyyy");
            string fecha_finalizacion = DateTime.Now.AddYears(1).ToString("d/M/yyyy");
            string hora = DateTime.Now.ToString("HH:mm:ss") + " hrs";
            Abonos_Membresia a = new Abonos_Membresia(bandera_online_offline);
            double restante_pagado = a.Restante(id_membresia);
            double abonado_pagado = a.Abonados(id_membresia, paciente.id_paciente, paciente.clinica.id_clinica);
            string sucursal = obtener_nombre_sucursal(paciente.clinica.id_clinica);
            int tamanio_rectangulo = 13;
            System.Drawing.Image imagen = System.Drawing.Image.FromFile(System.IO.Path.Combine(@System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName, @"..\..\..\Assets\bs_ticket_imagen.bmp"));
            System.Drawing.RectangleF rect = new System.Drawing.RectangleF(margen_izquierdo, margen_superior, centimetroAPixel(3.8), tamanio_rectangulo);//tamanio_hoja_horizontal en vez de 4
            RectangleF rImage = new RectangleF(38, margen_superior, 110, 110);

            e.Graphics.DrawImage(imagen, rImage);

            //e.Graphics.FillRectangle(Brushes.Red, rect); usarlo para cada recyangulo
            rect.Y = (cuerpo.GetHeight(e.Graphics) * 7) + margen_superior;
            rect.Height = 15;
            e.Graphics.DrawString("BONITA SMILE", titulo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.X = Convert.ToSingle(centimetroAPixel(margen_cuerpo));
            rect.Width = centimetroAPixel(tamanio_hoja_horizontal); //nuevo

            rect.Y = (cuerpo.GetHeight(e.Graphics) * 9) + margen_superior;
            rect.Height = 30;
            stringFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString("SUCURSAL: " + sucursal, cuerpo, new SolidBrush(Color.Black), rect, stringFormat);


            rect.Y = (cuerpo.GetHeight(e.Graphics) * 11) + margen_superior;
            e.Graphics.DrawString("CLIENTE: " + paciente.nombre + " " + paciente.apellidos, cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.Y = (cuerpo.GetHeight(e.Graphics) * 13) + margen_superior;
            rect.Height = tamanio_rectangulo;
            e.Graphics.DrawString("FECHA: " + fecha_inicio, cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.Y = (cuerpo.GetHeight(e.Graphics) * 14) + margen_superior;
            e.Graphics.DrawString("HORA: " + hora, cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.Y = (cuerpo.GetHeight(e.Graphics) * 16) + margen_superior;
            e.Graphics.DrawString("MOTIVO: ", cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.Height = 30;
            rect.Y = (cuerpo.GetHeight(e.Graphics) * 18) + margen_superior;
            e.Graphics.DrawString("Abono de membresia", cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.Y = (cuerpo.GetHeight(e.Graphics) * 20) + margen_superior;
            rect.Height = tamanio_rectangulo;
            e.Graphics.DrawString("PRECIO: $" + this.total, cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.Y = (cuerpo.GetHeight(e.Graphics) * 21) + margen_superior + 10;
            e.Graphics.DrawString("-------------------", cuerpo, new SolidBrush(Color.Black), rect, stringFormat);
            rect.Y = ((cuerpo.GetHeight(e.Graphics) * 21) + margen_superior + 10) + 5;
            e.Graphics.DrawString("-------------------", cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.Y = (cuerpo.GetHeight(e.Graphics) * 23) + margen_superior - 5;
            e.Graphics.DrawString("-------------------", cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.Y = ((cuerpo.GetHeight(e.Graphics) * 23) + margen_superior - 5) + 5;
            e.Graphics.DrawString("-------------------", cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.Y = ((cuerpo.GetHeight(e.Graphics) * 24) + margen_superior);
            e.Graphics.DrawString("TOTAL: $" + this.total, cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.Y = (cuerpo.GetHeight(e.Graphics) * 25) + margen_superior;
            e.Graphics.DrawString("ABONO: $" + txtAbono.Text, cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.Y = (cuerpo.GetHeight(e.Graphics) * 26) + margen_superior;
            e.Graphics.DrawString("RECIBIDO: $" + txt_efectivo.Text, cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.Y = (cuerpo.GetHeight(e.Graphics) * 27) + margen_superior;
            e.Graphics.DrawString("CAMBIO: $" + cambio, cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.Y = (cuerpo.GetHeight(e.Graphics) * 29) + margen_superior - 5;
            e.Graphics.DrawString("-------------------", cuerpo, new SolidBrush(Color.Black), rect, stringFormat);
            rect.Y = ((cuerpo.GetHeight(e.Graphics) * 29) + margen_superior - 5) + 5;
            e.Graphics.DrawString("-------------------", cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.Y = (cuerpo.GetHeight(e.Graphics) * 31) + margen_superior;
            e.Graphics.DrawString("ABONADO: " + abonado_pagado, cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.Y = (cuerpo.GetHeight(e.Graphics) * 32) + margen_superior;
            e.Graphics.DrawString("RESTANTE: " + restante_pagado, cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.X = Convert.ToSingle(centimetroAPixel(0.27));
            rect.Y = (cuerpo.GetHeight(e.Graphics) * 34) + margen_superior;
            //e.Graphics.DrawString("La fundación Doi Esperanza brinda una aportación del " + txt_Aportacion.Text + "% del total", final, new SolidBrush(Color.Black), rect, stringFormat);

            e.HasMorePages = false;
        }

        public string obtener_nombre_sucursal(string descripcion)
        {
            string id = "";
            query = "SELECT nombre_sucursal FROM clinica where id_clinica='" + descripcion + "'";

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
        #endregion

        private System.Windows.Forms.Label lblAbono;
        private System.Windows.Forms.TextBox txtAbono;
        private System.Windows.Forms.TextBox txtComentario;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelat;
        private Label label1;
        private TextBox txt_efectivo;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private PrintDialog printDialog1;
    }
}