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
    public partial class frmFuncionario : Form
    {

        Funcionario funcionario = new Funcionario();
        FuncionarioController objFuncionariosBll = new FuncionarioController();
        
        public frmFuncionario()
        {
            InitializeComponent();
        }

        private void frmFuncionario_Load(object sender, EventArgs e)
        {
            AtualizaGrid();
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCampos())
                {
                    funcionario.Nome = txtNome.Text;
                    funcionario.Tipo = cboTipo.Text;
                    funcionario.Especialidade = cboEspecialidade.Text;
                    funcionario.Cpf = mskCpf.Text;
                    funcionario.Celular = mskCelular.Text;
                    funcionario.Email = txtEmail.Text;
                    funcionario.Usuario = txtUsuario.Text;
                    funcionario.Senha = txtSenha.Text;
                    objFuncionariosBll.Inserir(funcionario);
                    //exibir alguma mensagem de sucesso
                    AtualizaGrid();
                    LimparForm();
                }
                else
                {
                    MessageBox.Show("Preencha todos os dados antes de inserir.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCampos() && lblIdFuncionario.Text != "")
                {
                    funcionario.Id = int.Parse(lblIdFuncionario.Text);
                    funcionario.Nome = txtNome.Text;
                    funcionario.Tipo = cboTipo.Text;
                    funcionario.Especialidade = cboEspecialidade.Text;
                    funcionario.Cpf = mskCpf.Text;
                    funcionario.Celular = mskCelular.Text;
                    funcionario.Email = txtEmail.Text;
                    funcionario.Usuario = txtUsuario.Text;
                    funcionario.Senha = txtSenha.Text;
                    objFuncionariosBll.Alterar(funcionario);
                    //exibir alguma mensagem de sucesso
                    AtualizaGrid();
                    LimparForm();
                }
                else
                {
                    MessageBox.Show("Preencha todos os dados antes de alterar.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if(lblIdFuncionario.Text != "") { 
                    funcionario.Id = int.Parse(lblIdFuncionario.Text);
                    objFuncionariosBll.Excluir(funcionario);
                    //exibir alguma mensagem de sucesso
                    AtualizaGrid();
                    LimparForm();
                }
                else
                {
                    MessageBox.Show("Selecione o funcionário antes de excluí-lo.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AtualizaGrid(string filtro = "")
        {
            
            try
            {
                dgvFuncionarios.DataSource = objFuncionariosBll.ListagemFuncionarios(filtro);
            }
            catch
            {
                MessageBox.Show("Deu ruim");
            }
            finally
            {
                FormataGrid();
            }
        }

        private void FormataGrid()
        {
            try
            {
                dgvFuncionarios.Columns[0].HeaderText = "ID";
                dgvFuncionarios.Columns[0].Width = 40;
                dgvFuncionarios.Columns[1].HeaderText = "Nome";
                dgvFuncionarios.Columns[1].Width = 130;
                dgvFuncionarios.Columns[2].HeaderText = "Especialidade";
                dgvFuncionarios.Columns[2].Width = 100;
                dgvFuncionarios.Columns[3].HeaderText = "Tipo";
                dgvFuncionarios.Columns[3].Width = 80;
                dgvFuncionarios.Columns[4].HeaderText = "UserName";
                dgvFuncionarios.Columns[4].Width = 100;
                dgvFuncionarios.Columns[5].HeaderText = "CPF";
                dgvFuncionarios.Columns[5].Width = 100;
                dgvFuncionarios.Columns[6].HeaderText = "E-mail";
                dgvFuncionarios.Columns[6].Width = 150;
                dgvFuncionarios.Columns[7].HeaderText = "Celular";
                dgvFuncionarios.Columns[7].Width = 100;
                
                dgvFuncionarios.Columns.Remove("Senha");
            }
            catch
            {
                LimparForm();
            }
        }

        private void LimparForm()
        {
            lblIdFuncionario.Text = "";
            txtNome.Text = "";
            mskCpf.Text = "";
            mskCelular.Text = "";
            txtEmail.Text = "";
            txtUsuario.Text = "";
            txtSenha.Text = "";
            txtPesquisar.Text = "";
            cboTipo.SelectedIndex = -1;
            cboEspecialidade.Text = "";
            txtNome.Focus();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {  
            LimparForm();
            AtualizaGrid();
            err1.Clear();
        }

        private void btnSair_Click(object sender, EventArgs e) => Close();

        private void txtNome_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);
        }

        private void dgvFuncionarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lblIdFuncionario.Text = dgvFuncionarios[0,dgvFuncionarios.CurrentRow.Index].Value.ToString();
            txtNome.Text = dgvFuncionarios[1, dgvFuncionarios.CurrentRow.Index].Value.ToString();
            cboEspecialidade.Text = dgvFuncionarios[2, dgvFuncionarios.CurrentRow.Index].Value.ToString();
            cboTipo.Text = dgvFuncionarios[3, dgvFuncionarios.CurrentRow.Index].Value.ToString();
            txtUsuario.Text = dgvFuncionarios[4, dgvFuncionarios.CurrentRow.Index].Value.ToString();
            mskCpf.Text = dgvFuncionarios[5, dgvFuncionarios.CurrentRow.Index].Value.ToString();
            txtEmail.Text = dgvFuncionarios[6, dgvFuncionarios.CurrentRow.Index].Value.ToString();
            mskCelular.Text = dgvFuncionarios[7, dgvFuncionarios.CurrentRow.Index].Value.ToString();
            err1.Clear();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            AtualizaGrid(txtPesquisar.Text);
            dgvFuncionarios.Rows[0].Selected = true;
        }

        private void txtEmail_Validated(object sender, EventArgs e)
        {
            try
            {
                if(txtEmail.Text == "")
                {
                    err1.SetError(txtEmail, "Informe o e-mail.");
                }
                else
                {
                    MailAddress m = new MailAddress(txtEmail.Text);
                }
            }
            catch (FormatException)
            {
                err1.SetError(txtEmail, "E-mail inválido");
            }
        }

        private void txtNome_Validating(object sender, CancelEventArgs e)
        {
            VerificarCampoVazio(txtNome,"Informe o nome do funcionário");
        }

        private void mskCpf_Validating(object sender, CancelEventArgs e)
        {
             if (mskCpf.Text.Length < 14)
            {
                err1.SetError(mskCpf, "Informe o CPF.");
            }
            else if (!ValidarDocumentos.ValidaCpf(mskCpf.Text))
            {
                err1.SetError(mskCpf, "CPF inválido.");
            }
            else
            {
                err1.SetError(mskCpf, "");
            }
        }

        private void mskCpf_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Space)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void mskCelular_Validating(object sender, CancelEventArgs e)
        {
            mskCelular.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            string celular = mskCelular.Text;

            if (celular.Length < 11)
            {
                err1.SetError(mskCelular, "Informe o celular.");
            }
            else
            {
                //verifica se o celular está dentro do padrão esperado
                Match match = Regex.Match(celular, "^[1-9]{2}9[1-9][0-9]{7}$");
                if (!match.Success)
                {
                    err1.SetError(mskCelular, "Número inválido.");
                }
                else
                {
                    err1.SetError(mskCelular, "");
                }   
            }
        }

        private void txtUsuario_Validating(object sender, CancelEventArgs e)
        {
            VerificarCampoVazio(txtUsuario, "Digite um nome de usuário.");
        }

        private void txtSenha_Validating(object sender, CancelEventArgs e)
        {
            VerificarCampoVazio(txtSenha, "Digite uma senha.\nNo mínimo 5 caracteres.");
        }

        private void cboEspecialidade_Validating(object sender, CancelEventArgs e)
        {
            VerificarCampoVazio(cboEspecialidade,"Informe a especilidade.");
        }

        private void cboTipo_Validating(object sender, CancelEventArgs e)
        {
            VerificarCampoVazio(cboTipo, "Selecione o tipo de usuário.");
        }

        private void VerificarCampoVazio(TextBox campo, string mensagem)
        {
            //verifica se o camps está vazio, caso esteja
            //retorna mensagem de erro
            //recebe o textbox do campo e a mensagem de erro a ser exibida

            if (campo.Text.Trim().Length == 0)
            {
                err1.SetError(campo, mensagem);
            }
            else
            {
                err1.SetError(campo, "");
            }
        }

        //sobrecarga 
        private void VerificarCampoVazio(ComboBox campo, string mensagem)
        {
            if (campo.Text.Trim().Length == 0)
            {
                err1.SetError(campo, mensagem);
            }
            else
            {
                err1.SetError(campo, "");
            }
        }

        private bool ValidarCampos()
        {
            //encontrar um modo mais inteligente de fazer isso
            //se possivel

            foreach(Control c in Controls)
            {
                if(c is TextBox || c is ComboBox || c is MaskedTextBox)
                    c.Focus();
            }
            foreach(Control c in Controls)
            {
                if(c is TextBox || c is ComboBox || c is MaskedTextBox)    
                    if (err1.GetError(c) != "")
                        return false;
            }

            return true;
        }
    }
}
