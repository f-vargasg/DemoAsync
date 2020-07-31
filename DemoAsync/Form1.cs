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
        }

        float OperacionLarga(int parametro)
        {
            System.Threading.Thread.Sleep(1000);
            throw new Exception("Excepcion");
            return 25 + parametro;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            UseWaitCursor = true;
            label1.Text = "Ejecutando Operacion...";
            int par = 50;
            float result = 0;
            try
            {
                result = await Task.Run<float>(() => { return OperacionLarga(par); });
                result += await Task.Run<float>(() => { return OperacionLarga(par); });
                result += await Task.Run<float>(() => { return OperacionLarga(par); });
                result += await Task.Run<float>(() => { return OperacionLarga(par); });
                label1.Text = "Finalizado " + result.ToString();
            }
            catch (Exception ex)
            {
                label1.Text = "Error " + ex.Message;
            }


            UseWaitCursor = false;
        }
    }
}
