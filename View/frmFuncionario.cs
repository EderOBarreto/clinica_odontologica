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

namespace View
{
    public partial class frmFuncionario : Form
    {
        String filtro;

        Funcionario funcionario = new Funcionario();
        FuncionarioController objFuncionariosBll = new FuncionarioController();
        
        public frmFuncionario()
        {
            InitializeComponent();
        }

        private void frmFuncionario_Load(object sender, EventArgs e)
        {
            cboEspecialidade.Items.Add("Dentista");
            cboEspecialidade.Items.Add("Recepcionista");
            cboEspecialidade.Items.Add("Implantodontista");
            cboEspecialidade.Items.Add("Periodontista");

            AtualizaGrid();
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            try
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

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                LimparForm();
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
        }

        private void btnSair_Click(object sender, EventArgs e) => Close();

        private void txtNome_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            try
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

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                LimparForm();
            }
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
            
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            AtualizaGrid(txtPesquisar.Text);
            dgvFuncionarios.Rows[0].Selected = true;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                funcionario.Id = int.Parse(lblIdFuncionario.Text);
                objFuncionariosBll.Excluir(funcionario);
                //exibir alguma mensagem de sucesso
                AtualizaGrid();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                LimparForm();
            }
        }

        private void txtEmail_Validated(object sender, EventArgs e)
        {
            try
            {
                if(txtEmail.Text != "")
                { 
                    MailAddress m = new MailAddress(txtEmail.Text);
                }
            }
            catch (FormatException)
            {
                //mostrar mensagem de erro por label ou tooltip
            }
        }
    }
}
