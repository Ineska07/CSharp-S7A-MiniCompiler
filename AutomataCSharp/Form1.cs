﻿using ExcelDataReader;
using System;
using System.IO;
using System.Windows.Forms;

namespace AutomataCSharp
{
    public partial class frmIDE : Form
    {
        bool saved;
        string template = Properties.Resources.template;
        string archivo = "C:\\Users\\Ines B\\Desktop/Matriz.xlsx";
        Lexico analizador;

        public frmIDE()
        {
            InitializeComponent();
            btnRun.Enabled = false;
            btnRun.Visible = false;
            btnGuardar.Visible = false;
            btnGuardar.Enabled = false;
            saved = false;
        }

        private void frmIDE_Load(object sender, EventArgs e)
        {
            
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
                    dgvToken.Rows.Clear();
                    tbxCodigo.Text = template;
                    lblNumLineas.Text = "Lineas: " + tbxCodigo.Lines.Length.ToString();
                    HabilitarGuardar();
                    saved = false;
                }
            }
            else
            {
                dgvToken.Rows.Clear();
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
            lblNumLineas.Text = "Lineas: " + tbxCodigo.Lines.Length.ToString();
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
                dgvToken.Rows.Clear();
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            dgvToken.Rows.Clear();
            analizador = new Lexico();
            analizador.Inicializar();

            lblError.Visible = true;

            using (var excelread = File.Open(archivo, FileMode.Open, FileAccess.Read))
            {
                using (var read = ExcelReaderFactory.CreateReader(excelread))
                {
                    do 
                    { 
                        analizador.cargarMatriz(read); 
                    } while (read.NextResult());
                }

                analizador.analizarTexto(tbxCodigo.Text + " ");
                int numError = analizador.getErrores();
                lblError.Text = "Errores: " + numError.ToString();
                ImprimirTablaTokens();
            }
        }

        private void ImprimirTablaTokens()
        {
            foreach (Tokens token in analizador.tokensGenerados)
            {
                dgvToken.Rows.Add(token.Tipo, token.Lexema, token.Valor, token.Linea);
            }
        }
    }
}
