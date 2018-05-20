using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;

using Model;

namespace Persistence
{
    public class RepositoryPaciente
    {
        MySqlConnection conPaciente = new MySqlConnection(Dados.strConexao);
        MySqlCommand cmdPaciente = new MySqlCommand();

        public void Inserir(Paciente paciente)
        {
            try
            {
                if (exists(paciente))
                    throw new Exception("Paciente já consta na base de dados.");


                cmdPaciente.CommandType = CommandType.StoredProcedure;
                cmdPaciente.CommandText = "inserir_paciente";
                cmdPaciente.Connection = conPaciente;

                cmdPaciente.Parameters.AddWithValue("id_convenio", paciente.Pid_conv);
                cmdPaciente.Parameters.AddWithValue("nome", paciente.Nome);
                cmdPaciente.Parameters.AddWithValue("sexo", paciente.Sexo);
                cmdPaciente.Parameters.AddWithValue("cpf", paciente.Cpf);
                cmdPaciente.Parameters.AddWithValue("data_nascimento", paciente.DataNascimento);
                cmdPaciente.Parameters.AddWithValue("celular", paciente.Celular);
                cmdPaciente.Parameters.AddWithValue("email", paciente.Email);

                conPaciente.Open();
                paciente.Pid = Convert.ToInt32(cmdPaciente.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conPaciente.Close();
            }
        }

        public bool Excluir(Paciente paciente)
        {
            try
            {
                cmdPaciente.CommandType = CommandType.StoredProcedure;
                cmdPaciente.CommandText = "excluir_paciente";
                cmdPaciente.Connection = conPaciente;

                cmdPaciente.Parameters.AddWithValue("id_paciente", paciente.Pid);

                conPaciente.Open();

                if (cmdPaciente.ExecuteNonQuery() != 1)
                {
                    throw new Exception("Paciente não encontrado.");
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conPaciente.Close();
            }
        }

        public ListaPacientes ListagemPacientes(string filtro)
        {
            ListaPacientes lisp = new ListaPacientes();  /* LISP é daora! Emacs Rocks! */
            try
            {
                cmdPaciente.CommandType = CommandType.StoredProcedure;
                cmdPaciente.CommandText = "selecionar_paciente";
                cmdPaciente.Connection = conPaciente;

                cmdPaciente.Parameters.AddWithValue("pfiltro", filtro);

                conPaciente.Open();

                MySqlDataReader dr = cmdPaciente.ExecuteReader();
                cmdPaciente.Parameters.Clear();

                if (dr.HasRows)
                {
                    foreach (DataRow row in dr)
                    {
                        Paciente pac = new Paciente();
                        pac.Pid = int.Parse(row["pac_id"].ToString());
                        pac.Pid_conv = int.Parse(row["pac_id_convenio"].ToString());
                        pac.Nome = row["pac_nome"].ToString();
                        pac.Sexo = row["pac_sexo"].ToString();
                        pac.Cpf = row["pac_cpf"].ToString();
                        pac.DataNascimento = Convert.ToDateTime(row["pac_data_nascimento"].ToString());
                        pac.Celular = row["pac_celular"].ToString();
                        pac.Email = row["pac_email"].ToString();

                        lisp.Add(pac);
                    }
                }
                return lisp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conPaciente.Close();
            }
        }

        public void Alterar(Paciente paciente)
        {
            try
            {
                cmdPaciente.CommandType = CommandType.StoredProcedure;
                cmdPaciente.CommandText = "alterar_paciente";
                cmdPaciente.Connection = conPaciente;

                cmdPaciente.Parameters.AddWithValue("id_paciente", paciente.Pid);
                cmdPaciente.Parameters.AddWithValue("id_convenio", paciente.Pid_conv);
                cmdPaciente.Parameters.AddWithValue("nome", paciente.Nome);
                cmdPaciente.Parameters.AddWithValue("sexo", paciente.Sexo);
                cmdPaciente.Parameters.AddWithValue("cpf", paciente.Cpf);
                cmdPaciente.Parameters.AddWithValue("data_nascimento", paciente.DataNascimento.ToString());
                cmdPaciente.Parameters.AddWithValue("celular", paciente.Celular);
                cmdPaciente.Parameters.AddWithValue("email", paciente.Email);

                conPaciente.Open();

                cmdPaciente.ExecuteNonQuery();
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conPaciente.Close();
            }
        }

        private bool exists(Paciente paciente)
        {
            try
            {
                cmdPaciente.CommandType = CommandType.StoredProcedure;
                cmdPaciente.CommandText = "paciente_existe";
                cmdPaciente.Connection = conPaciente;

                cmdPaciente.Parameters.AddWithValue("pnome", paciente.Nome);

                conPaciente.Open();

                int exist = cmdPaciente.ExecuteNonQuery();

                return exist > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conPaciente.Close();
            }
        }
    }
}
