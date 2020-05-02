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
using System.IO;
using Newtonsoft.Json;

namespace OSTaskSimulator
{
    public partial class Form1 : Form
    {
        private Random numeroAleatorio;
        private const int tiempoTotalBloqueado = 10;
        private const string textoProcesos = "Número de procesos nuevos: ";
        private const string textoProcesoSiguiente = "Tamaño del próximo: ";
        private const string textoContador = "Contador: ";
        private bool pausado = false;
        public List<Proceso> bcp;
        public Queue<Proceso> procesosNuevos;
        private Queue<Proceso> procesosListos;
        private Queue<Proceso> procesosSuspendidos;
        private List<Proceso> procesosBloqueados;
        private Marco[] memoria;
        private Proceso procesoActual;
        private int contador;
        public int ultimoId;
        public int Quantum;
        private int contadorQuantum;
        public Form1()
        {
            InitializeComponent();
            numeroAleatorio = new Random();
            procesosNuevos = new Queue<Proceso>();
            procesosListos = new Queue<Proceso>();
            procesosSuspendidos = new Queue<Proceso>();
            procesosBloqueados = new List<Proceso>();
            bcp = new List<Proceso>();
            inicializarMemoria();
            inicializarTablaMemoria();
        }

        private void agregarProcesosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 ventanaProceso = new Form2();
            AddOwnedForm(ventanaProceso);
            ventanaProceso.ShowDialog();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void iniciarBtn_Click(object sender, EventArgs e)
        {
            inicializarPrograma();
        }

        #region Timer
        private void timer1_Tick(object sender, EventArgs e)
        {
            labelContador.Text = textoContador + contador;
            if (contadorQuantum == Quantum)
            {
                if (procesoActual.TiempoRestante != 0)
                {
                    agregarProcesoListo(procesoActual);
                    procesoActual = null;
                    contadorQuantum = 0;
                }
            }
            if (procesoActual == null)
            {
                cargarProceso();
            }
            if(agregarNuevoProceso())
            {
                Proceso procesoAux = procesosNuevos.Dequeue();
                procesoAux.TiempoLlegada = contador;
                agregarProcesoInicio(procesoAux);

            }
            if (procesoActual != null && procesoActual.TiempoRestante != 0)
            {
                actualizarProcesosListos();
                actualizarProcesosSuspendidos();
                procesoActual.TiempoTranscurrido++;
                procesoActual.TiempoRestante--;
                contador++;
                contadorQuantum++;
                tiempoTranscurridoTxt.Text = procesoActual.TiempoTranscurrido.ToString();
                tiempoRestanteTxt.Text = procesoActual.TiempoRestante.ToString();
                textoQuantum.Text = contadorQuantum.ToString();
            }
            else
            {
                finalizarProceso(false);
            }
            if ((procesoActual == null && procesosBloqueados.Count > 0) | procesosSuspendidos.Count > 0)
            {
                contador++;
            }
            if (procesosBloqueados.Count != 0)
            {
                actualizarProcesosBloqueados();
            }
            labelProcesos.Text = textoProcesos + procesosNuevos.Count;
            if (procesosNuevos.Count > 0)
                labelSiguienteProceso.Text = textoProcesoSiguiente + procesosNuevos.Peek().Tamanio.ToString();
            else labelSiguienteProceso.Text = textoProcesoSiguiente;
            labelSuspendidos.Text = "Número de procesos suspendidos: " + procesosSuspendidos.Count;

            if(procesosSuspendidos.Count > 0)
            {
                labelRegresoId.Text = "ID del proceso a regresar: " + procesosSuspendidos.Peek().Id;
                labelRegresoTam.Text = "Tamaño del proceso a regresar: " + procesosSuspendidos.Peek().Tamanio;
            }
            else
            {
                labelRegresoId.Text = "ID del proceso a regresar: ";
                labelRegresoTam.Text = "Tamaño del proceso a regresar: ";
            }
        }
        #endregion

        #region OperacionesAritméticas
        public string operacionAleatoria()
        {
            int numero1, numero2, operador;
            string operacion;
            char[] operadores = { '+', '-', '*', '/', '%' };

            numero1 = numeroAleatorio.Next(-500, 500);
            numero2 = numeroAleatorio.Next(-500, 500);

            if (numero2 == 0)
            {
                operador = numeroAleatorio.Next(0, 3);
            }
            else
            {
                operador = numeroAleatorio.Next(0, 5);
            }
            operacion = numero1.ToString() + operadores[operador] + numero2.ToString();
            return operacion;
        }

        private decimal realizarOperacion(string operacion)
        {
            string[] operandos = new string[2];
            char tipoOperacion = ' ';
            int indice = 0;
            bool banderaAux = true;
            decimal resultado = 0;

            for (int i = 0; i < operacion.Length; i++)
            {
                if (operacion[i] == '+' | operacion[i] == '*' | operacion[i] == '/' | operacion[i] == '%' |
                   (operacion[i] == '-' && i != 0 && banderaAux))
                {
                    tipoOperacion = operacion[i];
                    indice++;
                    banderaAux = false;
                }
                else
                {
                    operandos[indice] += operacion[i];
                }
            }

            switch (tipoOperacion)
            {
                case '+':
                    resultado = Convert.ToDecimal(operandos[0]) + Convert.ToDecimal(operandos[1]);
                    break;

                case '-':
                    resultado = Convert.ToDecimal(operandos[0]) - Convert.ToDecimal(operandos[1]);
                    break;

                case '*':
                    resultado = Convert.ToDecimal(operandos[0]) * Convert.ToDecimal(operandos[1]);
                    break;

                case '/':
                    resultado = Convert.ToDecimal(operandos[0]) / Convert.ToDecimal(operandos[1]);
                    break;

                case '%':
                    resultado = Convert.ToDecimal(operandos[0]) % Convert.ToDecimal(operandos[1]);
                    break;
            }
            return resultado;
        }
        #endregion

        #region InicializarPrograma
        private void inicializarPrograma()
        {
            contador = 0;
            contadorQuantum = 0;
            iniciarBtn.Enabled = false;
            agregarProcesosToolStripMenuItem.Enabled = false;
            labelEstado.Text = "Estado: Ejecutandose";
            tablaTerminados.Rows.Clear();
            timer1.Start();
        }

        public void inicializarMemoria()
        {
            memoria = new Marco[34];
            int fila = 1;
            for(int i = 0; i < 34; i++)
            {
                if(i % 2 == 0) memoria[i] = new Marco(i + 2, 2, fila);
                else
                {
                    memoria[i] = new Marco(i + 2, 9, fila);
                    fila++;
                }
            }
        }

        public void inicializarTablaMemoria()
        {
            for(int i = 0; i < 36; i++)
            {
                int n = tablaMemoria.Rows.Add();
                tablaMemoria.Rows[n].Cells[0].Value = i;
                tablaMemoria.Rows[n].Cells[0].Style.ForeColor = Color.Blue;
                tablaMemoria.Rows[n].Cells[7].Value = ++i;
                tablaMemoria.Rows[n].Cells[7].Style.ForeColor = Color.Blue;
            }

            tablaMemoria.Rows[0].Cells[1].Value = "SO";
            tablaMemoria.Rows[0].Cells[8].Value = "SO";
            for (int i = 2; i < 7; i++)
            {
                tablaMemoria.Rows[0].Cells[i].Style.BackColor = Color.Black;
                tablaMemoria.Rows[0].Cells[i + 7].Style.BackColor = Color.Black;
            }
        }
        #endregion

        #region operacionesMemoria
        public bool espacioDisponible(Proceso proceso)
        {
            int espacio = 0;
            for(int i = 0; i < 34; i++)
            {
                if (memoria[i].Disponible)
                {
                    espacio += 5;
                    if (espacio >= proceso.Tamanio) return true;
                }
            }
            return false;
        }

        public Queue<Marco> marcosDisponibles()
        {
            Queue<Marco> marcos = new Queue<Marco>();
            for (int i = 0; i < 34; i++)
            {
                if (memoria[i].Disponible) marcos.Enqueue(memoria[i]);
            }
            return marcos;
        }

        public void pintarCeldas(Color color, Proceso proceso, bool terminado)
        {

            for (int i = 0; i < 34; i++)
            {
                if(memoria[i].IdProceso == proceso.Id)
                {
                    Marco auxiliar = memoria[i];
                    if (terminado)
                    {
                        int fila = auxiliar.FilaActual;
                        int columnaInicial = auxiliar.ColumnaInicial - 1;
                        tablaMemoria.Rows[fila].Cells[columnaInicial].Value = "";
                    }
                    for (int j = auxiliar.ColumnaInicial; j < auxiliar.ColumnaActual; j++)
                    {
                        tablaMemoria.Rows[auxiliar.FilaActual].Cells[j].Style.BackColor = color;
                    }
                    if (terminado)
                    {
                        auxiliar.Disponible = true;
                        auxiliar.ColumnaActual = auxiliar.ColumnaInicial;
                        auxiliar.Espacio = 0;
                    }
                }
            }
        }

        public void cargarProcesoMemoria(Proceso proceso, Color color)
        {
            int espacio = 0;
            int marcosNecesarios = proceso.Tamanio / 5;
            Marco auxiliar;
            Queue<Marco> marcos = marcosDisponibles();

            if (proceso.Tamanio % 5 != 0) marcosNecesarios++;

            while(marcosNecesarios != 0)
            { 
                auxiliar = marcos.Dequeue();
                int fila = auxiliar.FilaActual;
                int columna = auxiliar.ColumnaActual;
                int columnaInicial = auxiliar.ColumnaInicial - 1;
                tablaMemoria.Rows[fila].Cells[columnaInicial].Value = proceso.Id;
                auxiliar.IdProceso = proceso.Id;
                while (!auxiliar.lleno())
                {
                    columna = auxiliar.ColumnaActual;
                    auxiliar.Disponible = false;
                    tablaMemoria.Rows[fila].Cells[columna].Style.BackColor = color;
                    auxiliar.Espacio++;
                    espacio++;
                    if (espacio == proceso.Tamanio) break;
                }
                marcosNecesarios--;
            }
        }

        #endregion

        #region procesos
        public void agregarProcesos()
        {
            tablaListos.Rows.Clear();
            tablaMemoria.Rows.Clear();
            inicializarTablaMemoria();
            while (procesosNuevos.Count != 0)
            {
                if (espacioDisponible(procesosNuevos.Peek())) agregarProcesoInicio(procesosNuevos.Dequeue());
                else break;
            }
            labelProcesos.Text = textoProcesos + procesosNuevos.Count;
            if (procesosNuevos.Count > 0)
                labelSiguienteProceso.Text = textoProcesoSiguiente + procesosNuevos.Peek().Tamanio.ToString();
            else labelSiguienteProceso.Text = textoProcesoSiguiente;
        }

        private void agregarProcesoInicio(Proceso proceso)
        {
            int n = tablaListos.Rows.Add();
            tablaListos.Rows[n].Cells[0].Value = proceso.Id;
            tablaListos.Rows[n].Cells[1].Value = proceso.TiempoEstimado;
            tablaListos.Rows[n].Cells[2].Value = proceso.TiempoRestante;
            tablaListos.Rows[n].Cells[3].Value = proceso.Tamanio;
            proceso.Estado = "Listo";
            procesosListos.Enqueue(proceso);
            cargarProcesoMemoria(proceso, Color.LightBlue);
        }

        private void agregarProcesoListo(Proceso proceso)
        {
            int n = tablaListos.Rows.Add();
            tablaListos.Rows[n].Cells[0].Value = proceso.Id;
            tablaListos.Rows[n].Cells[1].Value = proceso.TiempoEstimado;
            tablaListos.Rows[n].Cells[2].Value = proceso.TiempoRestante;
            tablaListos.Rows[n].Cells[3].Value = proceso.Tamanio;
            proceso.Estado = "Listo";
            procesosListos.Enqueue(proceso);
            pintarCeldas(Color.LightBlue, proceso, false);
        }

        private void cargarProceso()
        {
            if(procesosListos.Count > 0)
            {
                procesoActual = procesosListos.Dequeue();
                tablaListos.Rows.RemoveAt(0);
                idTxt.Text = procesoActual.Id.ToString();
                operacionTxt.Text = procesoActual.Operacion;
                tiempoEstimadoTxt.Text = procesoActual.TiempoEstimado.ToString();
                tiempoTranscurridoTxt.Text = procesoActual.TiempoTranscurrido.ToString();
                tiempoRestanteTxt.Text = procesoActual.TiempoRestante.ToString();
                procesoActual.Estado = "En ejecución";
                pintarCeldas(Color.Red, procesoActual, false);
            }
            else
            {
                idTxt.Clear();
                operacionTxt.Clear();
                tiempoEstimadoTxt.Clear();
                tiempoTranscurridoTxt.Clear();
                tiempoRestanteTxt.Clear();
                textoQuantum.Clear();
            }
        }

        private bool agregarNuevoProceso()
        {
            if(procesosNuevos.Count > 0)
            {
                return espacioDisponible(procesosNuevos.Peek());
                /*
                Proceso procesoAux = procesosNuevos.Dequeue();
                procesoAux.TiempoLlegada = contador;
                agregarProcesoListo(procesoAux);
                */
            }
            return false;
        }

        private void finalizarProceso(bool error)
        {
            if(procesoActual != null)
            {
                contadorQuantum = 0;
                procesoActual.TiempoFinalizacion = contador;
                procesoActual.TiempoEspera = procesoActual.TiempoRetorno - procesoActual.TiempoServicio;
                if (!procesoActual.BanderaBloqueado)
                {
                    procesoActual.TiempoRespuesta = procesoActual.TiempoEspera;
                }
                int n = tablaTerminados.Rows.Add();
                tablaTerminados.Rows[n].Cells[0].Value = procesoActual.Id;
                tablaTerminados.Rows[n].Cells[1].Value = procesoActual.Operacion;
                procesoActual.Estado = "Terminado";
                pintarCeldas(Color.White, procesoActual, true);
                if (error)
                {
                    procesoActual.Estado = "Terminado (Error)";
                    tablaTerminados.Rows[n].Cells[2].Value = "Error";
                }
                else
                    tablaTerminados.Rows[n].Cells[2].Value = realizarOperacion(procesoActual.Operacion).ToString("#.##");

                procesoActual = null;
                if(procesosListos.Count + procesosBloqueados.Count + procesosSuspendidos.Count == 0)
                {
                    timer1.Stop();
                    agregarProcesosToolStripMenuItem.Enabled = true;
                    procesosNuevos.Clear();
                    labelEstado.Text = "Estado: Terminado";
                    MessageBox.Show("Procesos ejecutados!", "Realizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void actualizarProcesosBloqueados()
        {
            List<Proceso> listaAuxiliar = procesosBloqueados.Where(proceso => ++proceso.TiempoBloqueado == tiempoTotalBloqueado).ToList();
            foreach (var proceso in listaAuxiliar)
            {
                proceso.TiempoBloqueado = 0;
                agregarProcesoListo(proceso);
                procesosBloqueados.Remove(proceso);
            }
            tablaBloqueados.Rows.Clear();
            foreach(var proceso in procesosBloqueados)
            {
                int n = tablaBloqueados.Rows.Add();
                tablaBloqueados.Rows[n].Cells[0].Value = proceso.Id;
                tablaBloqueados.Rows[n].Cells[1].Value = proceso.TiempoBloqueado;
            }
        }

        private void actualizarProcesosListos()
        {
            foreach(var p in procesosListos)
            {
                p.TiempoRespuesta++;
            }
        }

        private void actualizarProcesosSuspendidos()
        {
            foreach (var p in procesosSuspendidos)
            {
                p.TiempoRespuesta++;
            }
        }
        #endregion

        #region EventoDeTeclas
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.I && !pausado)
            {
                if (procesoActual != null)
                {
                    procesoActual.BanderaBloqueado = true;
                    procesosBloqueados.Add(procesoActual);
                    pintarCeldas(Color.Purple, procesoActual, false);
                    int n = tablaBloqueados.Rows.Add();
                    tablaBloqueados.Rows[n].Cells[0].Value = procesoActual.Id;
                    tablaBloqueados.Rows[n].Cells[1].Value = procesoActual.TiempoBloqueado;
                    limpiarInformacionProcesoActual();
                    procesoActual.Estado = "Bloqueado";
                    procesoActual = null;
                    contadorQuantum = 0;
                }
            }
            if (e.KeyCode == Keys.E && !pausado)
            {
                finalizarProceso(true);
                contadorQuantum = 0;
            }
            if((e.KeyCode == Keys.P | e.KeyCode == Keys.M) && !pausado)
            {
                timer1.Stop();
                labelEstado.Text = "Estado: Pausado";
                pausado = true;
            }
            if(e.KeyCode == Keys.C && pausado)
            {
                timer1.Start();
                labelEstado.Text = "Estado: Ejecutandose";
                pausado = false;
            }
            if(e.KeyCode == Keys.N && !pausado)
            {
                Proceso proceso = new Proceso(++ultimoId, operacionAleatoria(), numeroAleatorio.Next(7, 15),
                    numeroAleatorio.Next(6,30));
                procesosNuevos.Enqueue(proceso);
                bcp.Add(proceso);
            }
            if(e.KeyCode == Keys.T && !pausado)
            {
                BCP tablaProcesos = new BCP();
                timer1.Stop();
                foreach(var p in procesosListos)
                {
                    p.TiempoEspera = contador - p.TiempoLlegada - p.TiempoServicio - 1;
                }
                foreach(var p in procesosBloqueados)
                {
                    p.TiempoEspera = contador - p.TiempoLlegada - p.TiempoServicio - 1;
                }
                foreach(var p in procesosSuspendidos)
                {
                    p.TiempoEspera = contador - p.TiempoLlegada - p.TiempoServicio - 1;
                }
                AddOwnedForm(tablaProcesos);
                tablaProcesos.mostrarTablaProcesos(bcp);
                tablaProcesos.ShowDialog();
            }
            if (e.KeyCode == Keys.S && !pausado)
            {
                if(procesosBloqueados.Count > 0)
                {
                    procesosBloqueados[0].Estado = "Bloqueado y suspendido";
                    escribirArchivo();
                    tablaBloqueados.Rows.RemoveAt(0);
                }
            }
            if(e.KeyCode == Keys.R && !pausado)
            {
                if(procesosSuspendidos.Count > 0)
                {
                    if (espacioDisponible(procesosSuspendidos.Peek()))
                    {
                        Proceso procesoAux = procesosSuspendidos.Dequeue();
                        procesoAux.TiempoBloqueado = 0;
                        procesoAux.Estado = "Bloqueado";
                        procesosBloqueados.Add(procesoAux);
                        cargarProcesoMemoria(procesoAux, Color.Purple);
                        string contenido = JsonConvert.SerializeObject(procesosSuspendidos, Formatting.Indented);
                        File.WriteAllText("procesos_suspendidos.json", contenido);
                    }
                }
            }
        }

        private void limpiarInformacionProcesoActual()
        {
            idTxt.Clear();
            operacionTxt.Clear();
            tiempoEstimadoTxt.Clear();
            tiempoTranscurridoTxt.Clear();
            tiempoRestanteTxt.Clear();
            textoQuantum.Clear();
        }

        private void escribirArchivo()
        {
            procesosSuspendidos.Enqueue(procesosBloqueados[0]);
            pintarCeldas(Color.White, procesosBloqueados[0], true);
            procesosBloqueados.RemoveAt(0);

            string contenido = JsonConvert.SerializeObject(procesosSuspendidos, Formatting.Indented);

            File.WriteAllText("procesos_suspendidos.json", contenido);
        }
        #endregion
    }
}
