using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSTaskSimulator
{
    public class Proceso
    {
        private int id;
        private string operacion;
        private string estado;
        private int tamanio;
        private int tiempoEstimado;
        private int tiempoTranscurrido;
        private int tiempoRestante;
        private int tiempoBloqueado;
        private int tiempoLlegada;
        private int tiempoFinalizacion;
        private int tiempoRespuesta;
        private int tiempoEspera;
        private bool banderaBloqueado;

        public Proceso()
        {
            id = 0;
            operacion = "";
            estado = "Nuevo";
            tiempoEstimado = 0;
            tiempoTranscurrido = 0;
            tiempoRestante = 0;
            tiempoBloqueado = 0;
            tiempoLlegada = 0;
            tiempoFinalizacion = 0;
            tiempoRespuesta = -1;
            tiempoEspera = -1;
            banderaBloqueado = false;
        }

        public Proceso(int id, string operacion, int tiempoEstimado, int tamanio) : this()
        {
            this.id = id;
            this.operacion = operacion;
            this.tiempoEstimado = tiempoEstimado;
            this.tiempoRestante = tiempoEstimado;
            this.tamanio = tamanio;
        }

        public int Id { get => id; set => id = value; }
        public string Operacion { get => operacion; set => operacion = value; }
        public int TiempoEstimado { get => tiempoEstimado; set => tiempoEstimado = value; }
        public int TiempoTranscurrido { get => tiempoTranscurrido; set => tiempoTranscurrido = value; }
        public int TiempoRestante { get => tiempoRestante; set => tiempoRestante = value; }
        public int TiempoBloqueado { get => tiempoBloqueado; set => tiempoBloqueado = value; }
        public int TiempoLlegada { get => tiempoLlegada; set => tiempoLlegada = value; }
        public int TiempoFinalizacion { get => tiempoFinalizacion; set => tiempoFinalizacion = value; }
        public int TiempoRetorno { get => tiempoFinalizacion - tiempoLlegada;}
        public int TiempoServicio { get => tiempoTranscurrido;}
        public string Estado { get => estado; set => estado = value; }
        public int TiempoRespuesta { get => tiempoRespuesta; set => tiempoRespuesta = value; }
        public int TiempoEspera { get => tiempoEspera; set => tiempoEspera = value; }
        public bool BanderaBloqueado { get => banderaBloqueado; set => banderaBloqueado = value; }
        public int Tamanio { get => tamanio; set => tamanio = value; }
    }
}
