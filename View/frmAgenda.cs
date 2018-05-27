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
using System.Security;

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
                DialogResult dr = ofd1.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    txtExame.Text = ofd1.FileName;
                }
                
            }
            catch(SecurityException ex)
            {
                // O usuário  não possui permissão para ler arquivos
                MessageBox.Show("Erro de segurança Contate o administrador de segurança da rede.\n\n" +
                                            "Mensagem : " + ex.Message + "\n\n" +
                                            "Detalhes (enviar ao suporte):\n\n" + ex.StackTrace);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
                    {
                        agenda.Exames.Nome = Path.GetFileName(txtExame.Text);
                        agenda.Exames.Arquivo = System.IO.File.ReadAllBytes(txtExame.Text);
                    }
                    else
                    {
                        agenda.Exames.Nome = null;
                        agenda.Exames.Arquivo = null;
                    }

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
            btnAbrir.Enabled = false;
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
                    if (txtPreco.Text != "")
                        agenda.Preco = float.Parse(txtPreco.Text.Replace(",", "."));
                    else
                        agenda.Preco = 0;

                    if (txtExame.Text != "")
                    {
                        agenda.Exames.Id_exame = int.Parse(dgvConsultas[8, dgvConsultas.CurrentRow.Index].Value.ToString());
                        agenda.Exames.Nome = txtExame.Text;
                        agenda.Exames.Arquivo = System.IO.File.ReadAllBytes(txtExame.Text);
                    }
                    else
                    {
                        agenda.Exames.Nome = null;
                        agenda.Exames.Arquivo = null;
                    }

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
            //passa os valores do grid para os campos
            lblIdConsulta.Text = dgvConsultas[0, dgvConsultas.CurrentRow.Index].Value.ToString();
            cboPacientes.SelectedValue = dgvConsultas[1, dgvConsultas.CurrentRow.Index].Value.ToString();
            cboFuncionario.SelectedValue = dgvConsultas[2, dgvConsultas.CurrentRow.Index].Value.ToString();
            dtpDataConsulta.Value = DateTime.Parse(dgvConsultas[3, dgvConsultas.CurrentRow.Index].Value.ToString());
            dtpHoraInicio.Value = DateTime.Parse(dgvConsultas[4, dgvConsultas.CurrentRow.Index].Value.ToString());
            dtpHoraTermino.Value = DateTime.Parse(dgvConsultas[5, dgvConsultas.CurrentRow.Index].Value.ToString());
            txtPreco.Text = dgvConsultas[6, dgvConsultas.CurrentRow.Index].Value.ToString();
            rtbDiagnostico.Text = dgvConsultas[7, dgvConsultas.CurrentRow.Index].Value.ToString();
            //se o campo estiver vazio retorna null
            tempFile = dgvConsultas[10, dgvConsultas.CurrentRow.Index].Value != DBNull.Value ?
                (byte[])dgvConsultas[10, dgvConsultas.CurrentRow.Index].Value : null;
            //se o tempFile estiver vazio desabilita botão abrir
            btnAbrir.Enabled = tempFile == null ? false : true;

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
            e.Handled = !(char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == 44);
        }

        private void FormataGrid()
        {
            try
            {
                //consulta
                dgvConsultas.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvConsultas.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvConsultas.Columns["agd_id_consulta"].HeaderText = "ID Consulta";
                dgvConsultas.Columns["agd_id_consulta"].Width = 60;
                dgvConsultas.Columns["agd_id_paciente"].DisplayIndex = 1;
                dgvConsultas.Columns["agd_id_paciente"].HeaderText = "ID Paciente";
                dgvConsultas.Columns["agd_id_paciente"].Width = 60;
                dgvConsultas.Columns["agd_id_funcionario"].DisplayIndex = 2;
                dgvConsultas.Columns["agd_id_funcionario"].HeaderText = "ID Funcionário";
                dgvConsultas.Columns["agd_id_funcionario"].Width = 60;
                dgvConsultas.Columns["agd_data_consulta"].DisplayIndex = 3;
                dgvConsultas.Columns["agd_data_consulta"].HeaderText = "Data da consulta";
                dgvConsultas.Columns["agd_data_consulta"].Width = 80;
                dgvConsultas.Columns["agd_hora_inicio"].DisplayIndex = 4;
                //dgvConsultas.Columns["agd_hora_inicio"].DefaultCellStyle.Format = "HH:mm";
                dgvConsultas.Columns["agd_hora_inicio"].HeaderText = "Início";
                dgvConsultas.Columns["agd_hora_inicio"].Width = 60;
                dgvConsultas.Columns["agd_hora_termino"].DisplayIndex = 5;
                //dgvConsultas.Columns["agd_hora_termino"].DefaultCellStyle.Format = "HH:mm";
                dgvConsultas.Columns["agd_hora_termino"].HeaderText = "Término";
                dgvConsultas.Columns["agd_hora_termino"].Width = 60;
                dgvConsultas.Columns["agd_preco_consulta"].DisplayIndex = 6;
                dgvConsultas.Columns["agd_preco_consulta"].DefaultCellStyle.Format = "C2";
                dgvConsultas.Columns["agd_preco_consulta"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgvConsultas.Columns["agd_preco_consulta"].HeaderText = "Preço";
                dgvConsultas.Columns["agd_preco_consulta"].Width = 100;
                dgvConsultas.Columns["agd_diagnostico"].DisplayIndex = 7;
                dgvConsultas.Columns["agd_diagnostico"].HeaderText = "Diagnóstico";
                dgvConsultas.Columns["agd_diagnostico"].Width = 120;
                //exames
                dgvConsultas.Columns["exa_id"].DisplayIndex = 8;
                dgvConsultas.Columns["exa_id"].Visible = false;
                dgvConsultas.Columns["exa_nome"].DisplayIndex = 9;
                dgvConsultas.Columns["exa_nome"].Visible = false;
                dgvConsultas.Columns["exa_arquivo"].DisplayIndex = 10;
                dgvConsultas.Columns["exa_arquivo"].Visible = false;
                dgvConsultas.Columns["exa_id_agenda"].DisplayIndex = 11;
                dgvConsultas.Columns["exa_id_agenda"].Visible = false;
            }
            catch
            {
                LimparForm();
            }
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {

            if (tempFile != null)
            {
                //path recebe o endereço do arquivo temp criando para receber os dados
                string path = Conversor.ConvertToPDF(tempFile);
                System.Diagnostics.Process.Start(path);
            }
        }
    }
}
