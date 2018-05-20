using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model;
using Controller;


namespace View
{
    public partial class frmAgenda : Form
    {
        Agenda agenda = new Agenda();

        AgendaController objAgendaBll = new AgendaController();


        public frmAgenda()
        {
            InitializeComponent();
        }

        private void frmAgenda_Load(object sender, EventArgs e)
        {
            cboFuncionario.DataSource = objAgendaBll.MostrarFuncionarios();
            cboFuncionario.ValueMember = "fun_id";
            cboFuncionario.DisplayMember = "fun_nome";
            cboPacientes.DataSource = objAgendaBll.MostrarPacientes();
            cboPacientes.ValueMember = "pac_id";
            cboPacientes.DisplayMember = "pac_nome";
            //cboFuncionario.Update();
            AtualizaGrid();
        }

        private void btnExame_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog odf = new OpenFileDialog())
                {
                    DialogResult res = odf.ShowDialog();
                    if (res == DialogResult.OK)
                    {
                        txtExame.Text = odf.FileName;
                    }
                }
            }
            catch
            {

            }
        }

        private byte[] LerEConverterArquivos(String nomeArquivo)
        {
            byte[] buffer = null;

            StreamReader oStreamReader = new StreamReader(nomeArquivo);

            buffer = new byte[oStreamReader.BaseStream.Length];

            oStreamReader.BaseStream.Read(buffer, 0, buffer.Length);

            oStreamReader.Close();
            oStreamReader.Dispose();

            return buffer;
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            try
            {
                if (/*ValidarCampos()*/true)
                {
                    agenda.Id_funcionario = Convert.ToInt32(cboFuncionario.SelectedValue);
                    agenda.Id_paciente = Convert.ToInt32(cboPacientes.SelectedValue);
                    agenda.Data_consulta = dtpDataConsulta.Value.Date;
                    agenda.Hora_inicio = dtpDataConsulta.Value;
                    agenda.Hora_final = dtpHoraTermino.Value;
                    agenda.Diagnostico = rtbDiagnostico.Text;
                    agenda.Preco = float.Parse(txtPreco.Text);
                    if (txtExame.Text != "")
                        agenda.Exames = LerEConverterArquivos(txtExame.Text);

                    objAgendaBll.Inserir(agenda);
                    LimparForm();
                    AtualizaGrid();
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                
            }
        }

        private void LimparForm()
        {
            lblIdConsulta.Text = "";
            cboFuncionario.SelectedIndex = -1;
            cboPacientes.SelectedIndex = -1;
            dtpDataConsulta.Value = DateTime.Now;
            dtpHoraInicio.Value = DateTime.Now;
            dtpHoraTermino.Value = DateTime.Now;
            txtExame.Text = "";
            rtbDiagnostico.Text = "";
            txtPreco.Text = "";
        }

        private bool ValidarCampos()
        {
            throw new NotImplementedException();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparForm();
        }

        public void AtualizaGrid(string filtro = "")
        {
            try
            {
                dgvConsultas.DataSource = objAgendaBll.ListagemConsultas(filtro);
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

        private void btnSair_Click(object sender, EventArgs e) => Close();

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            try
            {
                /*ValidarCampos() &&*/
                if (lblIdConsulta.Text != "")
                {
                    agenda.Id_consulta = int.Parse(lblIdConsulta.Text);
                    agenda.Id_funcionario = Convert.ToInt32(cboFuncionario.SelectedValue);
                    agenda.Id_paciente = Convert.ToInt32(cboPacientes.SelectedValue);
                    agenda.Data_consulta = dtpDataConsulta.Value.Date;
                    agenda.Hora_inicio = dtpDataConsulta.Value;
                    agenda.Hora_final = dtpHoraTermino.Value;
                    agenda.Diagnostico = rtbDiagnostico.Text;
                    agenda.Preco = float.Parse(txtPreco.Text);
                    if (txtExame.Text != "")
                        agenda.Exames = LerEConverterArquivos(txtExame.Text);

                    objAgendaBll.Alterar(agenda);
                    AtualizaGrid();
                    LimparForm();
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

            }
        }

        private void dgvConsultas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lblIdConsulta.Text = dgvConsultas[0, dgvConsultas.CurrentRow.Index].Value.ToString();
            cboPacientes.SelectedValue = dgvConsultas[1, dgvConsultas.CurrentRow.Index].Value.ToString();
            cboFuncionario.SelectedValue = dgvConsultas[2, dgvConsultas.CurrentRow.Index].Value.ToString();
            dtpDataConsulta.Value = DateTime.Parse(dgvConsultas[3, dgvConsultas.CurrentRow.Index].Value.ToString());
            txtPreco.Text = dgvConsultas[4, dgvConsultas.CurrentRow.Index].Value.ToString();
            //exame
            dtpHoraInicio.Value = DateTime.Parse(dgvConsultas[6, dgvConsultas.CurrentRow.Index].Value.ToString());
            dtpHoraTermino.Value = DateTime.Parse(dgvConsultas[7, dgvConsultas.CurrentRow.Index].Value.ToString());
            rtbDiagnostico.Text = dgvConsultas[8, dgvConsultas.CurrentRow.Index].Value.ToString();
        }

        private DateTime ConverterHora(string data)
        {
            
            data = DateTime.Parse(data).ToShortTimeString();
            return DateTime.Parse(data);
        }
    }
}
