using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSTaskSimulator
{
    public class Marco
    {
        private int numeroMarco;
        private int columnaInicial;
        private int columnaActual;
        private int filaActual;
        private int espacio;
        private bool disponible;
        private int idProceso;

        public Marco(int numeroMarco, int columnaInicial, int filaActual)
        {
            this.numeroMarco = numeroMarco;
            this.columnaInicial = columnaInicial;
            columnaActual = columnaInicial;
            this.filaActual = filaActual;
            espacio = 0;
            disponible = true;
        }

        public int NumeroMarco { get => numeroMarco; set => numeroMarco = value; }
        public int ColumnaInicial { get => columnaInicial; set => columnaInicial = value; }
        public int ColumnaActual { get => columnaActual + espacio; set => columnaActual = value; }
        public int Espacio { get => espacio; set => espacio = value; }
        public int FilaActual { get => filaActual; set => filaActual = value; }
        public bool Disponible { get => disponible; set => disponible = value; }
        public int IdProceso { get => idProceso; set => idProceso = value; }

        public bool lleno()
        {
            if(espacio == 5)
            {
                return true;
            }
            return false;
        }
    }
}
