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
        Lexico lex;
        Sintaxis syn;
        int errorlexico;
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
                dgvToken.Rows.Clear();
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            errorlexico = 0;
            dgvToken.Rows.Clear(); 
            dgvSintactico.Rows.Clear();
            lblError.Text = "Errores Léxicos: ";
            
            syn = new Sintaxis();

            syn.Inicializar();

            lblError.Visible = true;
            syn.AnalisisLexico(tbxCodigo.Text + " ");
            syn.StartSyntax();

            ImprimirTablaTokens();
            lblError.Text = "Errores Léxicos: " + errorlexico.ToString();

            //Inicia análisis sintáctico
            if (syn.listaErrores.Count == 0)
            {
                syn.AnalizadorSintactico();
                ImprimirTablaSintactico();
            }
            else
            {
                lblErrorSintaxis.Text = "ERROR: Resuelva los problemas léxicos";
            }

        }
        private void ImprimirTablaTokens()
        {
            foreach (Tokens token in syn.listaErrores)
            {
                dgvToken.Rows.Add(token.Tipo, token.Lexema, token.Valor, token.Linea);
                errorlexico++;
            }
            foreach (Tokens token in syn.tokensGenerados)
            {
                dgvToken.Rows.Add(token.Tipo, token.Lexema, token.Valor, token.Linea);
            }
        }

        private void ImprimirTablaSintactico()
        {
            lblErrorSintaxis.Visible = true;

            if (syn.listasyntaxErrores.Count == 0)
            {
                lblErrorSintaxis.Text = "Felicidades! No hay errores de sintaxis :)";
            }
            else
            {
                lblErrorSintaxis.Text = "Errores de Sintaxis: " + syn.listasyntaxErrores.Count.ToString();
                dgvSintactico.Visible = true;

                foreach (Tokens synE in syn.listasyntaxErrores)
                {
                    dgvSintactico.Rows.Add(synE.Tipo, synE.Lexema, synE.Valor, synE.Linea);
                }
            }
        }
    }
}
