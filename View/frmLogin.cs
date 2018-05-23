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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            txtUsuario.Text = "Usuário";
            txtUsuario.ForeColor = Color.Gray;
            txtSenha.Text = "Senha";
            txtSenha.ForeColor = Color.Gray;

            cboTipo.Items.Add("Administrador");
            cboTipo.Items.Add("Usuário");
        }

        private void txtUsuario_Enter(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "Usuário")
            {
                txtUsuario.Text = "";
            }
        }

        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "")
            {
                txtUsuario.Text = "Usuário";
                txtUsuario.ForeColor = Color.Gray;
            }
        }

        private void txtSenha_Leave(object sender, EventArgs e)
        {
            if (txtSenha.Text == "")
            {
                txtSenha.UseSystemPasswordChar = false;
                txtSenha.Text = "Senha";
                txtSenha.ForeColor = Color.Gray;
            }
        }

        private void txtSenha_Enter(object sender, EventArgs e)
        {
            if (txtSenha.Text == "Senha")
            {
                txtSenha.Text = "";
                txtSenha.UseSystemPasswordChar = true;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                bool autentica = false;

                FuncionarioController objFuncionariosBll = new FuncionarioController();

                Funcionario funcionario = new Funcionario();

                if (txtUsuario.Text == "" || txtSenha.Text == "")
                {
                    MessageBox.Show("Campos em branco! Entre com os dados para o Login!");
                }
                else
                {
                    funcionario.Usuario = txtUsuario.Text;
                    funcionario.Senha = txtSenha.Text;
                    funcionario.Tipo = cboTipo.Text;

                    objFuncionariosBll.RealizarLogin(funcionario.Usuario, funcionario.Senha, funcionario.Tipo);
                    autentica = objFuncionariosBll.getAutentica();
                    if (autentica == true)
                    {
                        Globais.strFuncionario = funcionario.Usuario;
                        Globais.strTipo = funcionario.Tipo;
                        Hide();
                        frmMenuPrincipal frmMain = new frmMenuPrincipal();
                        frmMain.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Login inválido, verifique os dados.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        // Caso não haja sucesso na autenticação
                        //lblMensagem.Text = objFuncionariosBll.Mensagem;
                        //this.Text = "Login - Tentativa " + Globais.intContador;
                        //lblMensagem.Text = "Login Inválido! Tente Novamente!";
                        //lblMensagem.Text = objFuncionariosBll.Mensagem;
                        //txtUsuario.Clear();
                        //txtSenha.Clear();
                        //txtUsuario.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
