namespace OSTaskSimulator
{
    partial class BCP
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tablaProcesos = new System.Windows.Forms.DataGridView();
            this.idTerminado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tiempoBloqueado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.operacionTerminado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tiempoLlegada = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tiempoFinalizacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tiempoRetorno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tiempoRespuesta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tiempoEspera = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tiempoServicio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tiempoRestante = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tiempoEstimado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.tablaProcesos)).BeginInit();
            this.SuspendLayout();
            // 
            // tablaProcesos
            // 
            this.tablaProcesos.AllowUserToAddRows = false;
            this.tablaProcesos.AllowUserToDeleteRows = false;
            this.tablaProcesos.AllowUserToResizeColumns = false;
            this.tablaProcesos.AllowUserToResizeRows = false;
            this.tablaProcesos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tablaProcesos.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tablaProcesos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.tablaProcesos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tablaProcesos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idTerminado,
            this.estado,
            this.tiempoBloqueado,
            this.operacionTerminado,
            this.tiempoLlegada,
            this.tiempoFinalizacion,
            this.tiempoRetorno,
            this.tiempoRespuesta,
            this.tiempoEspera,
            this.tiempoServicio,
            this.tiempoRestante,
            this.tiempoEstimado});
            this.tablaProcesos.Location = new System.Drawing.Point(12, 12);
            this.tablaProcesos.Name = "tablaProcesos";
            this.tablaProcesos.ReadOnly = true;
            this.tablaProcesos.RowHeadersVisible = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.tablaProcesos.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.tablaProcesos.RowTemplate.Height = 24;
            this.tablaProcesos.Size = new System.Drawing.Size(1049, 372);
            this.tablaProcesos.TabIndex = 1;
            // 
            // idTerminado
            // 
            this.idTerminado.FillWeight = 78.23604F;
            this.idTerminado.HeaderText = "ID";
            this.idTerminado.Name = "idTerminado";
            this.idTerminado.ReadOnly = true;
            // 
            // estado
            // 
            this.estado.FillWeight = 274.1117F;
            this.estado.HeaderText = "Estado";
            this.estado.Name = "estado";
            this.estado.ReadOnly = true;
            // 
            // tiempoBloqueado
            // 
            this.tiempoBloqueado.HeaderText = "Tiempo Bloqueado";
            this.tiempoBloqueado.Name = "tiempoBloqueado";
            this.tiempoBloqueado.ReadOnly = true;
            // 
            // operacionTerminado
            // 
            this.operacionTerminado.FillWeight = 78.23604F;
            this.operacionTerminado.HeaderText = "Operación";
            this.operacionTerminado.Name = "operacionTerminado";
            this.operacionTerminado.ReadOnly = true;
            // 
            // tiempoLlegada
            // 
            this.tiempoLlegada.FillWeight = 78.23604F;
            this.tiempoLlegada.HeaderText = "Tiempo de llegada";
            this.tiempoLlegada.Name = "tiempoLlegada";
            this.tiempoLlegada.ReadOnly = true;
            // 
            // tiempoFinalizacion
            // 
            this.tiempoFinalizacion.FillWeight = 78.23604F;
            this.tiempoFinalizacion.HeaderText = "Tiempo de finalización";
            this.tiempoFinalizacion.Name = "tiempoFinalizacion";
            this.tiempoFinalizacion.ReadOnly = true;
            // 
            // tiempoRetorno
            // 
            this.tiempoRetorno.FillWeight = 78.23604F;
            this.tiempoRetorno.HeaderText = "Tiempo de retorno";
            this.tiempoRetorno.Name = "tiempoRetorno";
            this.tiempoRetorno.ReadOnly = true;
            // 
            // tiempoRespuesta
            // 
            this.tiempoRespuesta.FillWeight = 78.23604F;
            this.tiempoRespuesta.HeaderText = "Tiempo de respuesta";
            this.tiempoRespuesta.Name = "tiempoRespuesta";
            this.tiempoRespuesta.ReadOnly = true;
            // 
            // tiempoEspera
            // 
            this.tiempoEspera.FillWeight = 78.23604F;
            this.tiempoEspera.HeaderText = "Tiempo de espera";
            this.tiempoEspera.Name = "tiempoEspera";
            this.tiempoEspera.ReadOnly = true;
            // 
            // tiempoServicio
            // 
            this.tiempoServicio.FillWeight = 78.23604F;
            this.tiempoServicio.HeaderText = "Tiempo de servicio";
            this.tiempoServicio.Name = "tiempoServicio";
            this.tiempoServicio.ReadOnly = true;
            // 
            // tiempoRestante
            // 
            this.tiempoRestante.HeaderText = "Tiempo Restante";
            this.tiempoRestante.Name = "tiempoRestante";
            this.tiempoRestante.ReadOnly = true;
            // 
            // tiempoEstimado
            // 
            this.tiempoEstimado.HeaderText = "Tiempo Máximo Estimado";
            this.tiempoEstimado.Name = "tiempoEstimado";
            this.tiempoEstimado.ReadOnly = true;
            // 
            // BCP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1073, 397);
            this.Controls.Add(this.tablaProcesos);
            this.KeyPreview = true;
            this.Name = "BCP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BCP";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BCP_FormClosed);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BCP_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.tablaProcesos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView tablaProcesos;
        private System.Windows.Forms.DataGridViewTextBoxColumn idTerminado;
        private System.Windows.Forms.DataGridViewTextBoxColumn estado;
        private System.Windows.Forms.DataGridViewTextBoxColumn tiempoBloqueado;
        private System.Windows.Forms.DataGridViewTextBoxColumn operacionTerminado;
        private System.Windows.Forms.DataGridViewTextBoxColumn tiempoLlegada;
        private System.Windows.Forms.DataGridViewTextBoxColumn tiempoFinalizacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn tiempoRetorno;
        private System.Windows.Forms.DataGridViewTextBoxColumn tiempoRespuesta;
        private System.Windows.Forms.DataGridViewTextBoxColumn tiempoEspera;
        private System.Windows.Forms.DataGridViewTextBoxColumn tiempoServicio;
        private System.Windows.Forms.DataGridViewTextBoxColumn tiempoRestante;
        private System.Windows.Forms.DataGridViewTextBoxColumn tiempoEstimado;
    }
}