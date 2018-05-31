using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

using Model;
using Controller;
using MySql.Data.MySqlClient;

namespace View
{
    public partial class frmConvenio : Form
    {
        ConvenioController ctrlConvenio = new ConvenioController();
        Convenio convenio = new Convenio();

        public frmConvenio()
        {
            InitializeComponent();
        }

        private void frmConvenio_Load(object sender, EventArgs e)
        {
            preencherDgv();
            formatarDgv();
        }
 
        private void txtConvenio_Validating(object sender, CancelEventArgs e)
        {
            if (txtConvenio.Text.Trim().Length < 4)
            {
                erro.SetError(txtConvenio, "Por favor, coloque um nome");
                return;
            }

            erro.SetError(txtConvenio, "");
        }

        private void mskCnpj_Validating(object sender, CancelEventArgs e)
        {
            if (mskCnpj.Text.Length != 18)
            {
                erro.SetError(mskCnpj, "CNPJ incompleto.");
                return;
            }

            erro.SetError(mskCnpj, "");
        }

        private void txtConvenio_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                e.SuppressKeyPress = true;
        }

        private void mskTelefone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                e.SuppressKeyPress = true;
        }

        private void mskTelefone_Validating(object sender, CancelEventArgs e)
        {
            mskTelefone.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            string num_celular = mskTelefone.Text;

            if (num_celular.Length < 11)
            {
                erro.SetError(mskTelefone, "Número incompleto.");
                return;
            }

            Match celular_eh_valido = Regex.Match(mskTelefone.Text, "^[1-9]{2}9[1-9][0-9]{7}$");

            if (!celular_eh_valido.Success)
                erro.SetError(mskTelefone, "Número inválido.");
            else
                erro.SetError(mskTelefone, "");
        }

        private void txtContato_Validating(object sender, CancelEventArgs e)
        {
            if (txtContato.Text.Trim().Length < 3)
            {
                erro.SetError(txtContato, "Preencha o contato.");
                return;
            }

            erro.SetError(txtContato, "");
        }

        private void btnSair_Click(object sender, EventArgs e) => this.Close();

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            limpar();
            preencherDgv();
        }

        private void limpar()
        {
            txtContato.Text = "";
            txtConvenio.Text = "";
            txtEmail.Text = "";
            txtPesquisar.Text = "";
            mskCnpj.Text = "";
            mskTelefone.Text = "";
            lblId.Text = "";

            erro.Clear();
        }

        private void preencherDgv()
        {
            try
            {
                dgvConvenios.DataSource = ctrlConvenio.ListarConvenios("");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Parece que algo deu errado...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void formatarDgv()
        {
            try
            {
                dgvConvenios.Columns[0].HeaderText = "ID";
                dgvConvenios.Columns[0].Width = 30;
                dgvConvenios.Columns[1].HeaderText = "Convenio";
                dgvConvenios.Columns[2].HeaderText = "CNPJ";
                dgvConvenios.Columns[3].HeaderText = "Contato";
                dgvConvenios.Columns[4].HeaderText = "Telefone";
                dgvConvenios.Columns[5].HeaderText = "E-Mail";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Parece que algo errado aconteceu...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            try
            {
                preencherConvenio();

                ctrlConvenio.Inserir(convenio);

                preencherConvenio();

                MessageBox.Show("Convênio cadastrado com sucesso!", "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Parece que algo estranho aconteceu...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void preencherConvenio()
        {
            convenio.Cnpj = mskCnpj.Text;
            convenio.Contato = txtContato.Text;
            convenio.NomeConvenio = txtConvenio.Text;
            convenio.Telefone = mskTelefone.Text;
            convenio.Email = txtEmail.Text;
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            preencherDgv();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                convenio.Cid = int.Parse(lblId.Text);

                ctrlConvenio.Excluir(convenio);

                MessageBox.Show("Convenio excluido com sucesso!", "Sucesso.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (FormatException)
            {
                DialogResult answer = MessageBox.Show("Nenhum convênio foi selecionado para exclusão." + "\r\n\r\n" +
                                                        "Deseja limpar a tela?", "Parece que algo estranho aconteceu...",
                                                        MessageBoxButtons.YesNo,
                                                        MessageBoxIcon.Asterisk);
                if (answer == DialogResult.Yes)
                    limpar();
            }
            catch (MySqlException)
            {
                DialogResult answer = MessageBox.Show("Você não pode excluir este convênio pois existem pacientes cadastros nele." + "\r\n\r\n" +
                                                        "Deseja ver os pacientes?", "Parece que algo estranho aconteceu...",
                                                        MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                if (answer == DialogResult.Yes)
                {
                    frmPaciente frmPac = new frmPaciente();
                    frmPac.pesquisar(convenio.Cid.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Parece que algo estranho aconteceu...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            try
            {
                preencherConvenio();
                convenio.Cid = int.Parse(lblId.Text);

                ctrlConvenio.Alterar(convenio);

                MessageBox.Show("Convênio alterado com sucesso!", "Sucesso.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                limpar();
                preencherDgv();
            }
            catch (FormatException)
            {
                MessageBox.Show("Nenhum convênio selecionado para alteração.", "Selecione um convênio primeiro...", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Parece que algo de estranho aconteceu...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvConvenios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int cur_index = dgvConvenios.CurrentRow.Index;

                lblId.Text = dgvConvenios[0, cur_index].Value.ToString();
                txtConvenio.Text = dgvConvenios[1, cur_index].Value.ToString();
                mskCnpj.Text = dgvConvenios[2, cur_index].Value.ToString();
                txtContato.Text = dgvConvenios[3, cur_index].Value.ToString();
                mskTelefone.Text = dgvConvenios[4, cur_index].Value.ToString();
                txtEmail.Text = dgvConvenios[5, cur_index].Value.ToString();           
            }
            catch (NullReferenceException) { }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Parece que algo estranho aconteceu...", MessageBoxButtons.OK, MessageBoxIcon.Error);

                limpar();
                preencherDgv();
            }
        }
    }
}
