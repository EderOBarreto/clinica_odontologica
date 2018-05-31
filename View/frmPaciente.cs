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
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace View
{
    public partial class frmPaciente : Form
    {
        Paciente pacientes = new Paciente();
        PacientesController ctrlPacientes = new PacientesController();
        ConvenioController ctrlConvenio = new ConvenioController();
        DataTable dtConvenioSource;

        public frmPaciente()
        {
            InitializeComponent();
        }

        private void frmPaciente_Load(object sender, EventArgs e)
        {
            dtpNascimento.MaxDate = DateTime.Now;

            preencherDgv();
            formatarDgv();
            preencherCombo();
        }

        private void preencherCombo()
        {
            try
            {
                dtConvenioSource = ctrlConvenio.ListarConvenios();
                cboConvenio.DataSource = dtConvenioSource;
                cboConvenio.DisplayMember = "con_convenio";
                cboConvenio.ValueMember = "con_id";
                cboConvenio.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void preencherDgv()
        {
            try
            {
                pesquisar("");
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
            cboConvenio.SelectedIndex = -1;

            erro.Clear();
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
                preencherPaciente();
                ctrlPacientes.Inserir(pacientes);

                MessageBox.Show("Paciente incluido com sucesso.", "Sucesso...", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                pacientes.Pid_conv = getIndex_dtConvenio(cboConvenio.Text);
                pacientes.Nome = txtNome.Text;
                pacientes.Sexo = cboSexo.SelectedItem.ToString();
                pacientes.Email = txtEmail.Text;
                pacientes.Celular = mskCelular.Text;
                pacientes.Cpf = mskCpf.Text.Replace(".", "").Replace("-","").Replace(",", "");
                pacientes.DataNascimento = dtpNascimento.Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void txtNome_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);
        }

        private void txtEmail_Validated(object sender, EventArgs e)
        {
            try
            {
                MailAddress mail = new MailAddress(txtEmail.Text);

                erro.SetError(txtEmail, "");
            }
            catch (Exception)
            {
                erro.SetError(txtEmail, "Email inválido.");
            }
        }

        private void txtNome_Validating(object sender, CancelEventArgs e)
        {
            if (txtNome.Text.Trim().Length <= 3)
                erro.SetError(txtNome, "Informe um nome.");
            else
                erro.SetError(txtNome, "");
        }

        private void mskCpf_Validating(object sender, CancelEventArgs e)
        {
            if (mskCpf.Text.Length < 14)
                erro.SetError(mskCpf, "Informe o CPF.");
            else if (!ValidarDocumentos.ValidaCpf(mskCpf.Text))
                erro.SetError(mskCpf, "CPF inválido.");
            else
                erro.SetError(mskCpf, "");
        }

        private void mskCpf_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Space)
                e.SuppressKeyPress = true;
        }

        private void mskCelular_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Space)
                e.SuppressKeyPress = true;
        }

        private void mskCelular_Validating(object sender, CancelEventArgs e)
        {
            mskCelular.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            string num_celular = mskCelular.Text;

            if (num_celular.Length < 11)
            {
                erro.SetError(mskCelular, "Número incompleto.");
                return;
            }

            Match celular_eh_valido = Regex.Match(mskCelular.Text, "^[1-9]{2}9[1-9][0-9]{7}$");

            if (!celular_eh_valido.Success)
                erro.SetError(mskCelular, "Número inválido.");
            else
                erro.SetError(mskCelular, "");
        }

        private void cboSexo_Validating(object sender, CancelEventArgs e)
        {
            if (cboSexo.SelectedIndex == -1)
                erro.SetError(cboSexo, "Selecione um sexo.");
            else
                erro.SetError(cboSexo, "");
        }

        private void cboConvenio_Validating(object sender, CancelEventArgs e)
        {
            if (cboConvenio.SelectedIndex == -1)
                erro.SetError(cboSexo, "Selecione um convênio.");
            else
                erro.SetError(cboSexo, "");
        }

        private void dgvPacientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                lblIdPaciente.Text = dgvPacientes[0, dgvPacientes.CurrentRow.Index].Value.ToString();

                // TODO:
                //      Ver erro bizarro no cboConvenio ao clicar varias vezes em uma celula.
                var _value = dgvPacientes[1, dgvPacientes.CurrentRow.Index].Value;
                cboConvenio.SelectedText = getConvenio_dtConvenio((int)_value);
                
                txtNome.Text = dgvPacientes[2, dgvPacientes.CurrentRow.Index].Value.ToString();
                cboSexo.Text = dgvPacientes[3, dgvPacientes.CurrentRow.Index].Value.ToString();
                mskCpf.Text = dgvPacientes[4, dgvPacientes.CurrentRow.Index].Value.ToString();
                dtpNascimento.Value = Convert.ToDateTime(dgvPacientes[5, dgvPacientes.CurrentRow.Index].Value.ToString());
                mskCelular.Text = dgvPacientes[6, dgvPacientes.CurrentRow.Index].Value.ToString();
                txtEmail.Text = dgvPacientes[7, dgvPacientes.CurrentRow.Index].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Algo de errado aconteceu...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                limpar();
                preencherDgv();
            }
        }

        private int getIndex_dtConvenio(string conv_nome)
        {
            var index = from DataRowView view in dtConvenioSource.AsDataView()
                        where view.Row.Field<string>("con_convenio") == conv_nome
                        select view.Row.Field<int>("con_id");

            return index.ToList<int>()[0];
        }

        private string getConvenio_dtConvenio(int index_convenio)
        {
            var con_name = from DataRowView view in dtConvenioSource.AsDataView()
                           where view.Row.Field<int>("con_id") == index_convenio
                           select view.Row.Field<string>("con_convenio");

            return con_name.ToList<string>()[0];
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            try
            {
                preencherPaciente();
                pacientes.Pid = int.Parse(lblIdPaciente.Text);

                ctrlPacientes.Alterar(pacientes);

                MessageBox.Show("Paciente alterado com sucesso!", "Sucesso.", MessageBoxButtons.OK, MessageBoxIcon.Information);

                limpar();
                preencherDgv();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Parece que algo estranho aconteceu...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                pesquisar(txtPesquisar.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Parece que algo estranho aconteceu...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void pesquisar(string filtro)
        {
            try
            {
                dgvPacientes.DataSource = ctrlPacientes.ListagemPacientes(filtro);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Parece que algo estranho aconteceu...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
