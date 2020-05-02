using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace OSTaskSimulator
{
    public partial class Form2 : Form
    {
        private Random numeroAleatorio;
        private bool ventanaCerrada;
        private int numeroProcesos;

        public Form2()
        {
            InitializeComponent();
            numeroAleatorio = new Random();
            ventanaCerrada = true;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Form1 ventanaPrincipal = Owner as Form1;
            numeroProcesos = Convert.ToInt32(numeroProcesosNumeric.Value);
            for(int i = 0; i < numeroProcesos; i++)
            {
                Proceso procesoAuxiliar = new Proceso(i + 1, ventanaPrincipal.operacionAleatoria(),
                    numeroAleatorio.Next(7, 15), numeroAleatorio.Next(6, 30));
                ventanaPrincipal.procesosNuevos.Enqueue(procesoAuxiliar);
                ventanaPrincipal.bcp.Add(procesoAuxiliar);
            }
            ventanaPrincipal.ultimoId = ventanaPrincipal.procesosNuevos.Count;
            ventanaPrincipal.Quantum = Convert.ToInt32(valorQuantum.Value);
            ventanaCerrada = false;
            Close();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!ventanaCerrada)
            {
                Form1 ventanaPrincipal = Owner as Form1;
                ventanaPrincipal.iniciarBtn.Enabled = true;
                ventanaPrincipal.agregarProcesos();
                ventanaCerrada = true;
            }
        }
    }
}
