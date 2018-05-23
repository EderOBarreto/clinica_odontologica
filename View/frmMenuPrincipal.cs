using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace View
{
    public partial class frmMenuPrincipal : Form
    {
        public frmMenuPrincipal()
        {
            InitializeComponent();
        }
        private void opcClientes_Click(object sender, EventArgs e)
        {
            /*
            frmClientes objFrmClientes = new frmClientes();
            objFrmClientes.ShowDialog();*/
        }

        private void opcConsultas_Click(object sender, EventArgs e)
        {

            frmAgenda objFrmAgenda = new frmAgenda();
            objFrmAgenda.ShowDialog();
        }

        private void opcConvenios_Click(object sender, EventArgs e)
        {
            frmConvenio objFrmConvenio = new frmConvenio();
            objFrmConvenio.ShowDialog();
        }

        private void opcPacientes_Click(object sender, EventArgs e)
        {
            frmPaciente objFrmPaciente = new frmPaciente();
            objFrmPaciente.ShowDialog();
        }

        private void opcFuncionarios_Click(object sender, EventArgs e)
        {
            frmFuncionario objFRMFuncionario = new frmFuncionario();
            objFRMFuncionario.ShowDialog();
        }

        private void frmMenuPrincipal_Load(object sender, EventArgs e)
        {
            // Estabelecendo e exibindo a data do sistema
            lblData.Text = DateTime.Now.ToShortDateString();

            // Exibindo o nome do equipamento no sistema
            lblComputer.Text = Environment.MachineName;

            lblUsuario.Text = Globais.strFuncionario;

            if (Globais.strTipo == "Administrador")
            {
                lblTipoAcesso.Text = "Administrador: ";
                opcFuncionarios.Enabled = true;
                //btnFuncionarios.Enabled = true;
            }
            else
            {
                lblTipoAcesso.Text = "Usuario: ";
                opcFuncionarios.Enabled = false;
                // btnFuncionarios.Enabled = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToString("HH:mm:ss");
        }
    }
}
