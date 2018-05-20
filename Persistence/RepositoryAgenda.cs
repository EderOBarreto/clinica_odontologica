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
                //data agendamento não faz muito sentido aqui, acho que a data de consulta já iria servir
                cmdConsulta.Parameters.AddWithValue("data_consulta", consulta.Data_consulta);
                cmdConsulta.Parameters.AddWithValue("hora_inicio", consulta.Hora_inicio);
                cmdConsulta.Parameters.AddWithValue("hora_termino", consulta.Hora_final);
                cmdConsulta.Parameters.AddWithValue("preco_consulta", consulta.Data_consulta);
                //os exames serao arquivos em pdf
                cmdConsulta.Parameters.AddWithValue("exames", consulta.Exames);
                //o dianóstico talvez seja um pdf também, mas antes é preciso pesquisar um pouco sobre.
                cmdConsulta.Parameters.AddWithValue("diagnostico", consulta.Diagnostico);
                //poderia criar uma variavel para saber se a consulta está finaliza, em processo ou iniciada.
                

                conConsulta.Open();

                consulta.Id_consulta = Convert.ToInt32(cmdConsulta.ExecuteScalar());
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
                cmdConsulta.Parameters.AddWithValue("preco_consulta", consulta.Data_consulta);
                //os exames serao arquivos em pdf
                cmdConsulta.Parameters.AddWithValue("exames", consulta.Exames);
                //o dianóstico talvez seja um pdf também, mas antes é preciso pesquisar um pouco sobre.
                cmdConsulta.Parameters.AddWithValue("diagnostico", consulta.Diagnostico);
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

        public ListaAgenda ListagemAgendas(string filtro)
        {
            try
            {
                //criar procedure selecionar_agendas ou consultas
                ListaAgenda objListaConsultas = new ListaAgenda();
                conConsulta.ConnectionString = Dados.strConexao;
                cmdConsulta.Connection = conConsulta;
                cmdConsulta.CommandType = CommandType.StoredProcedure;
                cmdConsulta.CommandText = "selecionar_consulta";
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


                        consulta.Id_consulta = int.Parse(dr["agd_id_consulta"].ToString());
                        consulta.Id_paciente = int.Parse(dr["agd_id_paciente"].ToString());
                        consulta.Id_funcionario = int.Parse(dr["agd_id_funcionario"].ToString());
                        consulta.Data_consulta = DateTime.Parse(dr["agd_data_consulta"].ToString()); // 
                        consulta.Hora_inicio = DateTime.Parse(dr["agd_hora_consulta"].ToString()); //formato errado
                        consulta.Hora_final = DateTime.Parse(dr["agd_hora_consulta"].ToString());
                        consulta.Preco = float.Parse(dr["agd_preco_consulta"].ToString());
                        consulta.Exames = (byte[])dr["agd_exames"];
                        consulta.Diagnostico = (byte[])dr["agd_diagnostico"];

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
