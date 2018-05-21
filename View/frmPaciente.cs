using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Model;
using Controller;

namespace View
{
    public partial class frmPaciente : Form
    {
        ListaPacientes objListaPacientes = new ListaPacientes();
        PacientesController ctrlPacientes = new PacientesController();

        public frmPaciente()
        {
            InitializeComponent();
        }

        private void frmPaciente_Load(object sender, EventArgs e)
        {
            dtpNascimento.MaxDate = DateTime.Now;

            preencherDgv();
            formatarDgv();
        }

        private void preencherDgv()
        {
            try
            {
                objListaPacientes = ctrlPacientes.ListagemPacientes("");
                dgvPacientes.DataSource = objListaPacientes;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Parece que algo estranho aconteceu...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void formatarDgv()
        {
            try
            {
                dgvPacientes.Columns[0].HeaderText = "ID";
                dgvPacientes.Columns[1].HeaderText = "Convenio";
                dgvPacientes.Columns[2].HeaderText = "Nome";
                dgvPacientes.Columns[3].HeaderText = "Sexo";
                dgvPacientes.Columns[4].HeaderText = "Nascimento";
                dgvPacientes.Columns[5].HeaderText = "Celular";
                dgvPacientes.Columns[6].HeaderText = "Email";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Parece que algo estranho aconteceu...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSair_Click(object sender, EventArgs e) => Close();

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            limpar();
        }

        private void limpar()
        {
            txtEmail.Text = "";
            txtNome.Text = "";
            txtPesquisar.Text = "";
            mskCelular.Text = "";
            mskCpf.Text = "";
            cboSexo.SelectedIndex = -1;
            lblIdPaciente.Text = "";
            lblConvenio.Text = "";
        }
    }
}
