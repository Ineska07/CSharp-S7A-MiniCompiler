using System.Collections.Generic;
using ExcelDataReader;
using System;
using System.IO;
using System.Windows.Forms;

namespace AutomataCSharp
{
    public partial class frmIDE : Form
    {
        bool saved;
        string template = Properties.Resources.template;
        Syntax Analizador;
        int linecount;

        public frmIDE()
        {
            InitializeComponent();
            btnRun.Enabled = false;
            btnRun.Visible = false;
            btnGuardar.Visible = false;
            btnGuardar.Enabled = false;
            saved = false;
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Deseas Guardar el archivo actual?", "Salir", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
            {
                OpenFileDialog OpenFile = new OpenFileDialog();
                OpenFile.Filter = "C# files (*.cs)|*.cs";
                OpenFile.Title = "Abir archivo de Código C#";

                if (OpenFile.ShowDialog() == DialogResult.OK)
                {
                    string txt = File.ReadAllText(OpenFile.FileName);
                    tbxCodigo.Text = txt;
                    HabilitarGuardar();
                }
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (!tbxCodigo.Text.Equals(""))
            {
                if (MessageBox.Show("¿Deseas Guardar el archivo actual?", "Salir", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    btnGuardar_Click(sender, e);
                    saved = true;
                }
                else
                {
                    dgvErrores.Rows.Clear();
                    tbxCodigo.Text = template;
                    lblNumLineas.Text = "Lineas: " + tbxCodigo.Lines.Length.ToString();
                    HabilitarGuardar();
                    saved = false;
                }
            }
            else
            {
                dgvErrores.Rows.Clear();
                tbxCodigo.Text = template;
                lblNumLineas.Text = "Lineas: " + tbxCodigo.Lines.Length.ToString();
                HabilitarGuardar();
                saved = false;
            }
        }

        private void HabilitarGuardar()
        {
            btnGuardar.Visible = true;
            btnGuardar.Enabled = true;

            btnRun.Enabled = true;
            btnRun.Visible = true;
        }

        private void tbxCodigo_TextChanged(object sender, EventArgs e)
        {
            HabilitarGuardar();
            btnGuardar.Text = "Guardar*";
            linecount = tbxCodigo.Lines.Length;
            lblNumLineas.Text = "Lineas: " + linecount.ToString();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            SaveFileDialog SaveDialog = new SaveFileDialog();
            SaveDialog.Filter = "C# file (*.cs)|*.cs|Text file (*.txt)|*.txt|All files (*.*)|*.*";
            SaveDialog.Title = "Guardar Archivo";

            SaveDialog.RestoreDirectory = true;

            if (SaveDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(SaveDialog.FileName, tbxCodigo.Text);
                saved = true;
                btnGuardar.Text = "Guardar";
                dgvErrores.Rows.Clear();
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            dgvErrores.Rows.Clear();
            dgvPolish.Rows.Clear();

            lblError.Text = "Errores: ";
            
            Analizador = new Syntax();
            lblError.Visible = true;
            Analizador.AnalisisLexico(tbxCodigo.Text + " ");

            //Inicia análisis sintáctico
            if (Analizador.ErrorL.Count == 0) Analizador.AnalizadorSintactico();

            int TotalErrores = Analizador.ErrorL.Count + Analizador.ErrorS.Count;
            ImprimirTablaErrores(TotalErrores);

            if (TotalErrores == 0)
            {
                Intermedio code = new Intermedio(Analizador.tokensGenerados);
                code.CreatePolish();

                foreach(string polishline in code.Polish)
                {
                    dgvPolish.Rows.Add(polishline);
                }
            }
        }

        private void ImprimirTablaErrores(int ErrorCount)
        {
            lblError.Visible = true;

            if (ErrorCount == 0)
            {
                lblError.Text = "Felicidades! No hay errores :)";
                dgvPolish.Visible = true;
            }
            else
            {
                lblError.Text = "Errores: " + ErrorCount.ToString();
                dgvErrores.Visible = true;

                foreach (string error in Analizador.ErrorL) dgvErrores.Rows.Add(error);
                foreach (string error in Analizador.ErrorS) dgvErrores.Rows.Add(error);

                MessageBox.Show("ERROR: No se puede generar el código Intermedio");
            }
        }
    }
}
