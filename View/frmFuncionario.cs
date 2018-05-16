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
            cboTipo.Items.Add("Dentista");
            cboTipo.Items.Add("Recepcionista");
            cboTipo.Items.Add("Implantodontista");
            cboTipo.Items.Add("Periodontista");

            AtualizaGrid();
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            try
            {
                funcionario.Nome = txtNome.Text;
                funcionario.Tipo = cboTipo.Text;
                funcionario.Especialidade = txtEspecialidade.Text;
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
        }

        public void AtualizaGrid()
        {
            filtro = txtPesquisar.Text;
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
                //FormataGrid();
            }
        }
    }
}
