
namespace AutomataCSharp
{
    partial class frmIDE
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnAbrir = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.tbxCodigo = new System.Windows.Forms.RichTextBox();
            this.OpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.SaveDialog = new System.Windows.Forms.SaveFileDialog();
            this.dgvToken = new System.Windows.Forms.DataGridView();
            this.Tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Palabra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Valor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Linea = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblError = new System.Windows.Forms.Label();
            this.lblNumLineas = new System.Windows.Forms.Label();
            this.dgvSintactico = new System.Windows.Forms.DataGridView();
            this.lblErrorSintaxis = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvToken)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSintactico)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAbrir
            // 
            this.btnAbrir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(56)))), ((int)(((byte)(130)))));
            this.btnAbrir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAbrir.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(255)))), ((int)(((byte)(245)))));
            this.btnAbrir.Location = new System.Drawing.Point(93, 12);
            this.btnAbrir.Name = "btnAbrir";
            this.btnAbrir.Size = new System.Drawing.Size(75, 23);
            this.btnAbrir.TabIndex = 0;
            this.btnAbrir.Text = "Abir .cs";
            this.btnAbrir.UseVisualStyleBackColor = false;
            this.btnAbrir.Click += new System.EventHandler(this.btnAbrir_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.SaddleBrown;
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.ForeColor = System.Drawing.Color.Yellow;
            this.btnNuevo.Location = new System.Drawing.Point(12, 12);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(75, 23);
            this.btnNuevo.TabIndex = 1;
            this.btnNuevo.Text = "Nuevo .cs";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(97)))), ((int)(((byte)(42)))));
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.ForeColor = System.Drawing.Color.Lime;
            this.btnGuardar.Location = new System.Drawing.Point(174, 12);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 2;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnRun
            // 
            this.btnRun.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(8)))), ((int)(((byte)(130)))));
            this.btnRun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRun.ForeColor = System.Drawing.Color.HotPink;
            this.btnRun.Location = new System.Drawing.Point(255, 12);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(102, 23);
            this.btnRun.TabIndex = 3;
            this.btnRun.Text = "Ejecutar Análisis";
            this.btnRun.UseVisualStyleBackColor = false;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // tbxCodigo
            // 
            this.tbxCodigo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(41)))), ((int)(((byte)(35)))));
            this.tbxCodigo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbxCodigo.Font = new System.Drawing.Font("Consolas", 10F);
            this.tbxCodigo.ForeColor = System.Drawing.SystemColors.MenuBar;
            this.tbxCodigo.HideSelection = false;
            this.tbxCodigo.Location = new System.Drawing.Point(12, 41);
            this.tbxCodigo.Name = "tbxCodigo";
            this.tbxCodigo.Size = new System.Drawing.Size(597, 535);
            this.tbxCodigo.TabIndex = 4;
            this.tbxCodigo.Text = "";
            this.tbxCodigo.TextChanged += new System.EventHandler(this.tbxCodigo_TextChanged);
            // 
            // OpenDialog
            // 
            this.OpenDialog.FileName = "OpenDialog";
            // 
            // dgvToken
            // 
            this.dgvToken.AllowUserToAddRows = false;
            this.dgvToken.AllowUserToDeleteRows = false;
            this.dgvToken.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(41)))), ((int)(((byte)(35)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            this.dgvToken.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvToken.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvToken.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvToken.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(41)))), ((int)(((byte)(35)))));
            this.dgvToken.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvToken.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvToken.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Aqua;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvToken.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvToken.ColumnHeadersHeight = 20;
            this.dgvToken.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvToken.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Tipo,
            this.Palabra,
            this.Valor,
            this.Linea});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(41)))), ((int)(((byte)(35)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvToken.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvToken.EnableHeadersVisualStyles = false;
            this.dgvToken.GridColor = System.Drawing.Color.SlateGray;
            this.dgvToken.Location = new System.Drawing.Point(619, 41);
            this.dgvToken.Name = "dgvToken";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvToken.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvToken.RowHeadersVisible = false;
            this.dgvToken.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvToken.Size = new System.Drawing.Size(350, 245);
            this.dgvToken.TabIndex = 4;
            // 
            // Tipo
            // 
            this.Tipo.Frozen = true;
            this.Tipo.HeaderText = "Tipo";
            this.Tipo.Name = "Tipo";
            this.Tipo.Width = 51;
            // 
            // Palabra
            // 
            this.Palabra.Frozen = true;
            this.Palabra.HeaderText = "Palabra";
            this.Palabra.Name = "Palabra";
            this.Palabra.Width = 66;
            // 
            // Valor
            // 
            this.Valor.Frozen = true;
            this.Valor.HeaderText = "Token";
            this.Valor.Name = "Valor";
            this.Valor.Width = 61;
            // 
            // Linea
            // 
            this.Linea.HeaderText = "Linea";
            this.Linea.Name = "Linea";
            this.Linea.Width = 56;
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblError.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblError.Location = new System.Drawing.Point(616, 15);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(104, 16);
            this.lblError.TabIndex = 6;
            this.lblError.Text = "Errores Léxicos:";
            this.lblError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblError.Visible = false;
            // 
            // lblNumLineas
            // 
            this.lblNumLineas.AutoSize = true;
            this.lblNumLineas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumLineas.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblNumLineas.Location = new System.Drawing.Point(12, 588);
            this.lblNumLineas.Name = "lblNumLineas";
            this.lblNumLineas.Size = new System.Drawing.Size(51, 16);
            this.lblNumLineas.TabIndex = 7;
            this.lblNumLineas.Text = "Lineas:";
            this.lblNumLineas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvSintactico
            // 
            this.dgvSintactico.AllowUserToAddRows = false;
            this.dgvSintactico.AllowUserToDeleteRows = false;
            this.dgvSintactico.AllowUserToOrderColumns = true;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(41)))), ((int)(((byte)(35)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            this.dgvSintactico.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvSintactico.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvSintactico.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvSintactico.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(41)))), ((int)(((byte)(35)))));
            this.dgvSintactico.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSintactico.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvSintactico.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Aqua;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSintactico.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvSintactico.ColumnHeadersHeight = 20;
            this.dgvSintactico.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvSintactico.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn4});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(41)))), ((int)(((byte)(35)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSintactico.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgvSintactico.EnableHeadersVisualStyles = false;
            this.dgvSintactico.GridColor = System.Drawing.Color.SlateGray;
            this.dgvSintactico.Location = new System.Drawing.Point(619, 331);
            this.dgvSintactico.Name = "dgvSintactico";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSintactico.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvSintactico.RowHeadersVisible = false;
            this.dgvSintactico.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvSintactico.Size = new System.Drawing.Size(350, 245);
            this.dgvSintactico.TabIndex = 8;
            this.dgvSintactico.Visible = false;
            // 
            // lblErrorSintaxis
            // 
            this.lblErrorSintaxis.AutoSize = true;
            this.lblErrorSintaxis.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblErrorSintaxis.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblErrorSintaxis.Location = new System.Drawing.Point(616, 304);
            this.lblErrorSintaxis.Name = "lblErrorSintaxis";
            this.lblErrorSintaxis.Size = new System.Drawing.Size(123, 16);
            this.lblErrorSintaxis.TabIndex = 9;
            this.lblErrorSintaxis.Text = "Errores de Sintaxis:";
            this.lblErrorSintaxis.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblErrorSintaxis.Visible = false;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.Frozen = true;
            this.dataGridViewTextBoxColumn3.HeaderText = "Código";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 63;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.Frozen = true;
            this.dataGridViewTextBoxColumn1.HeaderText = "Descripción";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 86;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Linea";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 56;
            // 
            // frmIDE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(73)))), ((int)(((byte)(68)))));
            this.ClientSize = new System.Drawing.Size(984, 616);
            this.Controls.Add(this.lblErrorSintaxis);
            this.Controls.Add(this.dgvSintactico);
            this.Controls.Add(this.lblNumLineas);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.dgvToken);
            this.Controls.Add(this.tbxCodigo);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.btnAbrir);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "frmIDE";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "C# IDE";
            ((System.ComponentModel.ISupportInitialize)(this.dgvToken)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSintactico)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAbrir;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.RichTextBox tbxCodigo;
        private System.Windows.Forms.OpenFileDialog OpenDialog;
        private System.Windows.Forms.SaveFileDialog SaveDialog;
        private System.Windows.Forms.DataGridView dgvToken;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Label lblNumLineas;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Palabra;
        private System.Windows.Forms.DataGridViewTextBoxColumn Valor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Linea;
        private System.Windows.Forms.DataGridView dgvSintactico;
        private System.Windows.Forms.Label lblErrorSintaxis;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    }
}

