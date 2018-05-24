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

        byte[] tempFile;


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
                if (ValidarCampos())
                {
                    agenda.Id_funcionario = Convert.ToInt32(cboFuncionario.SelectedValue);
                    agenda.Id_paciente = Convert.ToInt32(cboPacientes.SelectedValue);
                    agenda.Data_consulta = dtpDataConsulta.Value.Date;
                    agenda.Hora_inicio = dtpHoraInicio.Value;
                    agenda.Hora_final = dtpHoraTermino.Value;
                    agenda.Diagnostico = rtbDiagnostico.Text;
                    if (txtPreco.Text != "")
                        agenda.Preco = float.Parse(txtPreco.Text);
                    else
                        agenda.Preco = 0;
                    if (txtExame.Text != "")
                        agenda.Exames = LerEConverterArquivos(txtExame.Text);

                    //verifica se foi inserido com sucesso
                    if (!objAgendaBll.Inserir(agenda))
                        MessageBox.Show(objAgendaBll.Mensagem, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        //limpa e atualiza caso tenha funcionado corretamente
                        LimparForm();
                        AtualizaGrid();
                    }

                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
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
            dtpHoraInicio.Value = DateTime.Parse("12:00");
            dtpHoraTermino.Value = DateTime.Parse("12:00");
            txtExame.Text = "";
            rtbDiagnostico.Text = "";
            txtPreco.Text = "";
            err1.Clear();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparForm();
            AtualizaGrid();
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
                FormataGrid();
            }
        }

        private void btnSair_Click(object sender, EventArgs e) => Close();

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            try
            {

                if (lblIdConsulta.Text != "")
                {
                    agenda.Id_consulta = int.Parse(lblIdConsulta.Text);
                    agenda.Id_funcionario = Convert.ToInt32(cboFuncionario.SelectedValue);
                    agenda.Id_paciente = Convert.ToInt32(cboPacientes.SelectedValue);
                    agenda.Data_consulta = dtpDataConsulta.Value.Date;
                    agenda.Hora_inicio = dtpHoraInicio.Value;
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
        }

        private void dgvConsultas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lblIdConsulta.Text = dgvConsultas[0, dgvConsultas.CurrentRow.Index].Value.ToString();
            cboPacientes.SelectedValue = dgvConsultas[1, dgvConsultas.CurrentRow.Index].Value.ToString();
            cboFuncionario.SelectedValue = dgvConsultas[2, dgvConsultas.CurrentRow.Index].Value.ToString();
            dtpDataConsulta.Value = DateTime.Parse(dgvConsultas[3, dgvConsultas.CurrentRow.Index].Value.ToString());
            txtPreco.Text = dgvConsultas[4, dgvConsultas.CurrentRow.Index].Value.ToString();
            dtpHoraInicio.Value = DateTime.Parse(dgvConsultas[6, dgvConsultas.CurrentRow.Index].Value.ToString());
            dtpHoraTermino.Value = DateTime.Parse(dgvConsultas[7, dgvConsultas.CurrentRow.Index].Value.ToString());
            rtbDiagnostico.Text = dgvConsultas[8, dgvConsultas.CurrentRow.Index].Value.ToString();
            tempFile = Encoding.UTF8.GetBytes(dgvConsultas[8, dgvConsultas.CurrentRow.Index].Value.ToString());

            err1.Clear();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblIdConsulta.Text != "")
                {
                    agenda.Id_consulta = int.Parse(lblIdConsulta.Text);
                    objAgendaBll.Excluir(agenda);
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

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            AtualizaGrid(txtPesquisar.Text);
        }

        private void VerificarCampoVazio(Control campo, string mensagem)
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

        private void cboFuncionario_Validating(object sender, CancelEventArgs e)
        {
            VerificarCampoVazio(cboFuncionario, "Selecione o funcionário.");
        }

        private void cboPacientes_Validating(object sender, CancelEventArgs e)
        {
            VerificarCampoVazio(cboPacientes, "Selecione o paciente.");
        }

        private bool ValidarCampos()
        {

            //verifica se a hora de início é menor do que a de término
            if (dtpHoraInicio.Value >= dtpHoraTermino.Value)
            {
                err1.SetError(dtpHoraInicio, "A hora de inicio deve ser menor do que término");
                return false;
            }
            else err1.SetError(dtpHoraInicio, "");

            //da focus nos controles para ativar o evento de validação
            foreach (Control c in Controls)
            {
                if (c is ComboBox || c is DateTimePicker)
                    c.Focus();
            }
            //verifica se alguma validação retorna erro
            foreach (Control c in Controls)
            {
                if (c is ComboBox || c is DateTimePicker)
                    if (err1.GetError(c) != "")
                        return false;
            }
            return true;
        }

        private void txtPreco_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == 46);
        }

        private void FormataGrid()
        {
            try
            {
                dgvConsultas.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvConsultas.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvConsultas.Columns["Id_consulta"].DisplayIndex = 0;
                dgvConsultas.Columns["Id_consulta"].HeaderText = "ID Consulta";
                dgvConsultas.Columns["Id_consulta"].Width = 60;
                dgvConsultas.Columns["Id_paciente"].DisplayIndex = 1;
                dgvConsultas.Columns["Id_paciente"].HeaderText = "ID Paciente";
                dgvConsultas.Columns["Id_paciente"].Width = 60;
                dgvConsultas.Columns["Id_funcionario"].DisplayIndex = 2;
                dgvConsultas.Columns["Id_funcionario"].HeaderText = "ID Funcionário";
                dgvConsultas.Columns["Id_funcionario"].Width = 60;
                dgvConsultas.Columns["Data_consulta"].DisplayIndex = 3;
                dgvConsultas.Columns["Data_consulta"].HeaderText = "Data da consulta";
                dgvConsultas.Columns["Data_consulta"].Width = 80;
                //Convert.ToDateTime(txtCampoTextoSaida.Text).ToString("hh:mm");
                dgvConsultas.Columns["Hora_inicio"].DisplayIndex = 4;
                dgvConsultas.Columns["Hora_inicio"].DefaultCellStyle.Format = "HH:mm";
                dgvConsultas.Columns["Hora_inicio"].HeaderText = "Início";
                dgvConsultas.Columns["Hora_inicio"].Width = 60;
                dgvConsultas.Columns["Hora_final"].DisplayIndex = 5;
                dgvConsultas.Columns["Hora_final"].DefaultCellStyle.Format = "HH:mm";
                dgvConsultas.Columns["Hora_final"].HeaderText = "Término";
                dgvConsultas.Columns["Hora_final"].Width = 60;
                dgvConsultas.Columns["Preco"].DisplayIndex = 6;
                dgvConsultas.Columns["Preco"].DefaultCellStyle.Format = "C2";
                dgvConsultas.Columns["Preco"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgvConsultas.Columns["Preco"].HeaderText = "Preço";
                dgvConsultas.Columns["Preco"].Width = 100;
                dgvConsultas.Columns["Diagnostico"].DisplayIndex = 7;
                dgvConsultas.Columns["Diagnostico"].HeaderText = "Diagnóstico";
                dgvConsultas.Columns["Diagnostico"].Width = 120;
                dgvConsultas.Columns["Exames"].Visible = false;
                dgvConsultas.Columns["Exames"].DisplayIndex = 8;
                //dgvConsultas.Columns["Exames"].HeaderText = "Exames";
                //dgvConsultas.Columns["Exames"].Width = 50;

            }
            catch
            {
                LimparForm();
            }
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            if (agenda.Exames != null)
            {
                string path;
                path = Conversor.ConvertToPDF(agenda.Exames);
                System.Diagnostics.Process.Start(path);
            }
        }
    }
}
