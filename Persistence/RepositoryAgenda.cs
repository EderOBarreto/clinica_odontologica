using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using MySql.Data.MySqlClient;

namespace Persistence
{
    public class RepositoryAgenda
    {
        MySqlConnection conConsulta = new MySqlConnection();

        MySqlCommand cmdConsulta = new MySqlCommand();


        public string Mensagem { get; set; }

        public void Inserir(Agenda consulta)
        {
            //verificar se já existe algum consulta com o mesmo nome
            try
            {
                conConsulta.ConnectionString = Dados.strConexao;

                cmdConsulta.CommandType = CommandType.StoredProcedure;

                cmdConsulta.CommandText = "inserir_agenda";

                cmdConsulta.Connection = conConsulta;

                cmdConsulta.Parameters.AddWithValue("id_paciente", consulta.Id_paciente);
                cmdConsulta.Parameters.AddWithValue("id_funcionario", consulta.Id_funcionario);
                cmdConsulta.Parameters.AddWithValue("data_consulta", consulta.Data_consulta);
                cmdConsulta.Parameters.AddWithValue("hora_inicio", consulta.Hora_inicio);
                cmdConsulta.Parameters.AddWithValue("hora_termino", consulta.Hora_final);
                cmdConsulta.Parameters.AddWithValue("preco_consulta", consulta.Data_consulta);
                cmdConsulta.Parameters.AddWithValue("diagnostico", consulta.Diagnostico);
                cmdConsulta.Parameters.AddWithValue("nome_exame", consulta.Exames.Nome);
                cmdConsulta.Parameters.AddWithValue("arquivo_exame", consulta.Exames.Arquivo);

                //poderia criar uma variavel para saber se a consulta está finaliza, em processo ou iniciada.

                conConsulta.Open();

                consulta.Id_consulta = Convert.ToInt32(cmdConsulta.ExecuteScalar());
                cmdConsulta.Parameters.Clear();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conConsulta.Close();
            }
        }

        public void Alterar(Agenda consulta)
        {
            try
            {
                conConsulta.ConnectionString = Dados.strConexao;
                cmdConsulta.CommandType = CommandType.StoredProcedure;
                cmdConsulta.CommandText = "alterar_agenda";
                cmdConsulta.Connection = conConsulta;

                cmdConsulta.Parameters.AddWithValue("id_consulta", consulta.Id_consulta);
                cmdConsulta.Parameters.AddWithValue("id_paciente", consulta.Id_paciente);
                cmdConsulta.Parameters.AddWithValue("id_funcionario", consulta.Id_funcionario);
                //data agendamento não faz muito sentido aqui, acho que a data de consulta já iria servir
                cmdConsulta.Parameters.AddWithValue("data_consulta", consulta.Data_consulta);
                cmdConsulta.Parameters.AddWithValue("hora_inicio", consulta.Hora_inicio);
                cmdConsulta.Parameters.AddWithValue("hora_termino", consulta.Hora_final);
                cmdConsulta.Parameters.AddWithValue("preco_consulta", consulta.Preco);
                cmdConsulta.Parameters.AddWithValue("diagnostico", consulta.Diagnostico);
                cmdConsulta.Parameters.AddWithValue("id_exame", consulta.Exames.Id_exame);
                cmdConsulta.Parameters.AddWithValue("nome_exame", consulta.Exames.Nome);
                cmdConsulta.Parameters.AddWithValue("arquivo_exame", consulta.Exames.Arquivo);

                conConsulta.Open();
                cmdConsulta.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conConsulta.Close();
            }
        }

        public bool Excluir(Agenda consulta)
        {

            int resultado = 0;
            bool resposta = false;

            try
            {
                conConsulta.ConnectionString = Dados.strConexao;
                cmdConsulta.Connection = conConsulta;
                cmdConsulta.CommandType = CommandType.StoredProcedure;
                cmdConsulta.CommandText = "excluir_agenda";
                cmdConsulta.Parameters.AddWithValue("id_consulta", consulta.Id_consulta);
                conConsulta.Open();
                resultado = cmdConsulta.ExecuteNonQuery();

                if (resultado != 1)
                {
                    throw new Exception("Não foi possível excluir a consulta.");
                }
                else
                {
                    resposta = true;
                }
                return resposta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conConsulta.Close();
            }
        }

        public DataTable ListagemAgendas(string filtro)
        {
            try
            {  
                ListaAgenda objListaConsultas = new ListaAgenda();
                conConsulta.ConnectionString = Dados.strConexao;
                cmdConsulta.Connection = conConsulta;
                cmdConsulta.CommandType = CommandType.StoredProcedure;
                cmdConsulta.CommandText = "selecionar_consulta";
                cmdConsulta.Parameters.AddWithValue("filtro", filtro);
                conConsulta.Open();

                MySqlDataReader dr = cmdConsulta.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                cmdConsulta.Parameters.Clear();
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conConsulta.Close();
            }
        }
        //preenche combobox funcionarios
        //cuidado codigo redundante abaixo
        //mudarei depois
        public DataTable ListarFuncionarios()
        {
            try
            {
                conConsulta.ConnectionString = Dados.strConexao;
                cmdConsulta.Connection = conConsulta;
                cmdConsulta.CommandType = CommandType.StoredProcedure;
                cmdConsulta.CommandText = "buscar_funcionarios_combo";
                conConsulta.Open();
                MySqlDataReader dr = cmdConsulta.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conConsulta.Close();
            }
        }
        //preenche combobox pacientes
        public DataTable ListarPacientes()
        {
            try
            {
                conConsulta.ConnectionString = Dados.strConexao;
                cmdConsulta.Connection = conConsulta;
                cmdConsulta.CommandType = CommandType.StoredProcedure;
                cmdConsulta.CommandText = "buscar_pacientes_combo";
                conConsulta.Open();
                MySqlDataReader dr = cmdConsulta.ExecuteReader();
                cmdConsulta.Parameters.Clear();
                DataTable dt = new DataTable();
                dt.Load(dr);

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conConsulta.Close();
            }
        }
        public bool VerificarDisponibilidade(Agenda agenda)
        {
            try
            {
                int valida;
                conConsulta.ConnectionString = Dados.strConexao;
                cmdConsulta.Connection = conConsulta;
                cmdConsulta.CommandType = CommandType.StoredProcedure;
                cmdConsulta.CommandText = "verificar_disponibilidade";
                cmdConsulta.Parameters.AddWithValue("dia", agenda.Data_consulta);
                cmdConsulta.Parameters.AddWithValue("hora_inicio", agenda.Hora_inicio);
                cmdConsulta.Parameters.AddWithValue("hora_final", agenda.Hora_final);
                cmdConsulta.Parameters.AddWithValue("id_funcionario", agenda.Id_funcionario);

                conConsulta.Open();

                valida = Convert.ToInt32(cmdConsulta.ExecuteScalar());
                cmdConsulta.Parameters.Clear();
                if (valida == 0)
                {
                    Mensagem = "";
                    return true;
                }
                Mensagem = "Data já utilizada.\nTente em outro dia ou horário.";
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conConsulta.Close();
            }
        }
    }
}
