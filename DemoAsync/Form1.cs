using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoAsync
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitMyComponents();
        }

        private void InitMyComponents()
        {
            // this.button1.Click += ClickSincrono;
            // Se ejecuta en otro hilo
            this.button1.Click += ClickAsincrono;
        }

        private void ClickSincrono(object sender, EventArgs e)
        {
            try
            {

                UseWaitCursor = true;
                label1.Text = "Ejecutando Operacion...";

                var progreso = new Progress<int>(pct => progressBar1.Value = pct);

                int par = 50;
                float result = 0;
                try
                {
                    result = OperacionLarga(par, progreso);

                    label1.Text = "Finalizado " + result.ToString();
                }
                catch (Exception ex)
                {
                    label1.Text = "Error " + ex.Message;
                }


                UseWaitCursor = false;
            }
            catch (Exception ex)
            {

                MessageBox.Show (ex.Message);
            }
        }

        private async void ClickAsincrono(object sender, EventArgs e)
        {
            UseWaitCursor = true;
            label1.Text = "Ejecutando Operacion...";

            var progreso = new Progress<int>(pct => progressBar1.Value = pct);

            int par = 50;
            float result = 0;
            try
            {
                result = await Task.Run<float>(() => { return OperacionLarga(par, progreso); });

                label1.Text = "Finalizado " + result.ToString();
            }
            catch (Exception ex)
            {
                label1.Text = "Error " + ex.Message;
            }


            UseWaitCursor = false;
        }



        /*
        private async void button1_Click(object sender, EventArgs e)
        {
            UseWaitCursor = true;
            label1.Text = "Ejecutando Operacion...";

            var progreso = new Progress<int>(pct => progressBar1.Value = pct);

            int par = 50;
            float result = 0;
            try
            {
                result = await Task.Run<float>(() => { return OperacionLarga(par, progreso); });

                label1.Text = "Finalizado " + result.ToString();
            }
            catch (Exception ex)
            {
                label1.Text = "Error " + ex.Message;
            }


            UseWaitCursor = false;
        }
        */


        float OperacionLarga(int parametro, IProgress<int> progreso)
        {
            for (int i = 0; i <= 100; i+=10)
            {
                System.Threading.Thread.Sleep( 100);
                if (progreso != null)
                {
                    progreso.Report(i);
                }
            }
            
            return 25 + parametro;
        }

        
    }
}
