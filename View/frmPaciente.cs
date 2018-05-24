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
        Paciente pacientes = new Paciente();
        Convenio convenio = new Convenio();
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
                dgvPacientes.DataSource = ctrlPacientes.ListagemPacientes("");
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
                dgvPacientes.Columns[1].HeaderText = "ID Convenio";
                dgvPacientes.Columns[1].Visible = false;
                dgvPacientes.Columns[2].HeaderText = "Nome";
                dgvPacientes.Columns[3].HeaderText = "Sexo";
                dgvPacientes.Columns[4].HeaderText = "CPF";
                dgvPacientes.Columns[5].HeaderText = "Nascimento";
                dgvPacientes.Columns[6].HeaderText = "Celular";
                dgvPacientes.Columns[7].HeaderText = "Email";
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
            formatarDgv();
            preencherDgv();
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

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                pacientes.Pid = int.Parse(lblIdPaciente.Text);
                ctrlPacientes.Excluir(pacientes);

                limpar();

                preencherDgv();
                formatarDgv();

                MessageBox.Show("Paciente excluido com sucesso.", "Excluido", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Parece que algo de errado aconteceu...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            try
            {
                /*
                 * TODO:
                 *      validar email igual o Eder
                 **/
                preencherPaciente();
                ctrlPacientes.Inserir(pacientes);
                limpar();
                preencherDgv();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Algo de errado aconteceu...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void preencherPaciente()
        {
            try
            {
                pacientes.Nome = txtNome.Text;
                pacientes.Sexo = cboSexo.SelectedText;
                pacientes.Email = txtEmail.Text;
                pacientes.Celular = mskCelular.Text;
                pacientes.Cpf = mskCpf.Text.Replace(".", "").Replace("-","").Replace(",", "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnConvenio_Click(object sender, EventArgs e)
        {
            /*
             *  VER COMO PEGAR POSX e POSY
             
            frmListaConvenios lstConv = new frmListaConvenios(convenio);
            lstConv.ShowDialog();
            */
        }
    }
}

/*
 * TODO:
 *      REVER RELAÇÃO DO ID_CONV
 *      CRIAR TELA PARA SELECIONAR CONVENIO?
 **/
