using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OSTaskSimulator
{
    public partial class BCP : Form
    {
        public BCP()
        {
            InitializeComponent();
            tablaProcesos.Columns[1].Width = 120;
        }

        private void BCP_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 ventanaPrincipal = Owner as Form1;
            ventanaPrincipal.timer1.Start();
        }

        public void mostrarTablaProcesos(List<Proceso> bcp)
        {
            foreach (var proceso in bcp)
            {
                int n = tablaProcesos.Rows.Add();
                tablaProcesos.Rows[n].Cells[0].Value = proceso.Id;
                tablaProcesos.Rows[n].Cells[1].Value = proceso.Estado;
                tablaProcesos.Rows[n].Cells[3].Value = proceso.Operacion;
                tablaProcesos.Rows[n].Cells[10].Value = proceso.TiempoRestante;
                tablaProcesos.Rows[n].Cells[11].Value = proceso.TiempoEstimado;
                if(proceso.Estado == "Bloqueado")
                {
                    tablaProcesos.Rows[n].Cells[2].Value = proceso.TiempoBloqueado;
                }
                if (proceso.Estado != "Nuevo")
                {
                    if(proceso.Estado == "Terminado" | proceso.Estado == "Terminado (Error)")
                    {
                        tablaProcesos.Rows[n].Cells[5].Value = proceso.TiempoFinalizacion;
                        tablaProcesos.Rows[n].Cells[6].Value = proceso.TiempoRetorno;
                    }
                    tablaProcesos.Rows[n].Cells[4].Value = proceso.TiempoLlegada;
                    if(proceso.TiempoEspera == -1)
                    {
                        tablaProcesos.Rows[n].Cells[7].Value = 0;
                        tablaProcesos.Rows[n].Cells[8].Value = 0;
                    }
                    else
                    {
                        tablaProcesos.Rows[n].Cells[7].Value = proceso.TiempoRespuesta;
                        tablaProcesos.Rows[n].Cells[8].Value = proceso.TiempoEspera;
                    }
                    tablaProcesos.Rows[n].Cells[9].Value = proceso.TiempoServicio;
                }
            }
        }

        private void BCP_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.C)
            {
                this.Close();
            }
        }
    }
}
