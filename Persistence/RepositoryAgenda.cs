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
    class RepositoryAgenda
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

                cmdConsulta.CommandText = "`inserir_agenda`";

                cmdConsulta.Connection = conConsulta;

                
                cmdConsulta.Parameters.AddWithValue("id_paciente", consulta.id_paciente);
                cmdConsulta.Parameters.AddWithValue("id_funcionario", consulta.id_funcionario);
                //data agendamento não faz muito sentido aqui, acho que a data de consulta já iria servir
                cmdConsulta.Parameters.AddWithValue("data_agendamento", consulta.data_agendamento);
                cmdConsulta.Parameters.AddWithValue("data_consulta", consulta.data_consulta);
                cmdConsulta.Parameters.AddWithValue("hora_consulta", consulta.hora);
                cmdConsulta.Parameters.AddWithValue("preco_consulta", consulta.data_consulta);
                //os exames serao arquivos em pdf
                cmdConsulta.Parameters.AddWithValue("exames", consulta.exames);
                // o que exatamente eu faco com a data de retorno?
                cmdConsulta.Parameters.AddWithValue("data_retorno", consulta.data_retorno);
                //o dianóstico talvez seja um pdf também, mas antes é preciso pesquisar um pouco sobre.
                cmdConsulta.Parameters.AddWithValue("diagnostico", consulta.diagnostico);
                //poderia criar uma variavel para saber se a consulta está finaliza, em processo ou iniciada.
                

                conConsulta.Open();

                consulta.id_consulta = Convert.ToInt32(cmdConsulta.ExecuteScalar());
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

                cmdConsulta.Parameters.AddWithValue("id_consulta", consulta.id_consulta);
                cmdConsulta.Parameters.AddWithValue("id_paciente", consulta.id_paciente);
                cmdConsulta.Parameters.AddWithValue("id_funcionario", consulta.id_funcionario);
                //data agendamento não faz muito sentido aqui, acho que a data de consulta já iria servir
                cmdConsulta.Parameters.AddWithValue("data_agendamento", consulta.data_agendamento);
                cmdConsulta.Parameters.AddWithValue("data_consulta", consulta.data_consulta);
                cmdConsulta.Parameters.AddWithValue("hora_consulta", consulta.hora);
                cmdConsulta.Parameters.AddWithValue("preco_consulta", consulta.data_consulta);
                //os exames serao arquivos em pdf
                cmdConsulta.Parameters.AddWithValue("exames", consulta.exames);
                // o que exatamente eu faco com a data de retorno?
                cmdConsulta.Parameters.AddWithValue("data_retorno", consulta.data_retorno);
                //o dianóstico talvez seja um pdf também, mas antes é preciso pesquisar um pouco sobre.
                cmdConsulta.Parameters.AddWithValue("diagnostico", consulta.diagnostico);
                //poderia criar uma variavel para saber se a consulta está finaliza, em processo ou iniciada.

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
                cmdConsulta.Parameters.AddWithValue("id_consulta", consulta.id_consulta);
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

        public ListaAgenda ListagemAgendas(string filtro)
        {
            try
            {
                //criar procedure selecionar_agendas ou consultas
                ListaAgenda objListaConsultas = new ListaAgenda();
                conConsulta.ConnectionString = Dados.strConexao;
                cmdConsulta.Connection = conConsulta;
                cmdConsulta.CommandType = CommandType.StoredProcedure;
                cmdConsulta.CommandText = "selecionar_funcionario";
                cmdConsulta.Parameters.AddWithValue("filtro", filtro);
                conConsulta.Open();

                MySqlDataReader dr = cmdConsulta.ExecuteReader();
                cmdConsulta.Parameters.Clear();
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        // Cria uma instâncoa para o objeto cliente
                        Agenda consulta = new Agenda();


                        consulta.id_consulta = int.Parse(dr["agd_id_consulta"].ToString());
                        consulta.id_paciente = int.Parse(dr["agd_id_paciente"].ToString());
                        consulta.id_funcionario = int.Parse(dr["agd_id_funcionario"].ToString());
                        consulta.data_consulta = DateTime.Parse(dr["agd_data_consulta"].ToString()); // 
                        consulta.hora = DateTime.Parse(dr["agd_hora_consulta"].ToString()); //formato errado
                        consulta.preco = float.Parse(dr["agd_preco_consulta"].ToString());
                        consulta.exames = dr["agd_exames"].ToString();
                        consulta.data_retorno = DateTime.Parse(dr["agd_data_retorno"].ToString());
                        consulta.diagnostico = dr["agd_diagnostico"].ToString();
                        consulta.data_agendamento = DateTime.Parse(dr["agd_data_agendamento"].ToString());

                        objListaConsultas.Add(consulta);

                    }
                }
                return objListaConsultas;
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
