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
            if(txtUsuario.Text == "Usuário")
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
                txtSenha.Text = "Senha";
                txtSenha.ForeColor = Color.Gray;
            }
        }

        private void txtSenha_Enter(object sender, EventArgs e)
        {
            if (txtSenha.Text == "Senha")
            {
                txtSenha.Text = "";
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            /*try
            {
                // Variável autentica receberá a resposta sobre a autenticação.
                bool autentica = false;

                // Criação da instancia do objeto funcionario da classe ModeloFuncionarios
                ModeloFuncionarios funcionario = new ModeloFuncionarios();

                // Instancia do objeto da camada Bll que transporta os dados 
                // do formulario.
                FuncionariosBll objFuncionariosBll = new FuncionariosBll();

                if (txtUsuario.Text == "" || txtSenha.Text == "")
                {
                    MessageBox.Show("Campos em branco! Entre com os dados para o Login!");
                }
                else
                {
                    // Leitura e armazenamento de dados da interface gráfica no objeto funcionario.
                    funcionario.Funlogin = txtUsuario.Text;
                    funcionario.Funsenha = txtSenha.Text;
                    funcionario.Funtipo = cboTipo.Text;
                    Globais.strFuncionario = funcionario.Funlogin;
                    Globais.strTipo = funcionario.Funtipo;

                    // O método Login da camada Bll retorna valor boleano para
                    // autenticar ou não o usuário
                    autentica = objFuncionariosBll.Login(funcionario);
                    lblMensagem.Text = objFuncionariosBll.Mensagem;

                    // Se houver sucesso na autenticação
                    if (autentica == true)
                    {
                        // Oculta o formulário Login
                        this.Hide();

                        // Cria uma instância do formulário Form1
                        Form1 objForm1 = new Form1();
                        objForm1.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Login inválido, verifique os dados.");
                        // Caso não haja sucesso na autenticação
                        //lblMensagem.Text = objFuncionariosBll.Mensagem;
                        //this.Text = "Login - Tentativa " + Globais.intContador;
                        //lblMensagem.Text = "Login Inválido! Tente Novamente!";
                        //lblMensagem.Text = objFuncionariosBll.Mensagem;
                        txtUsuario.Clear();
                        txtSenha.Clear();
                        txtUsuario.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }*/
        }
    }
}
