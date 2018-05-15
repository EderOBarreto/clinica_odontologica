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
    class RepositoryFuncionarios
    {
            MySqlConnection conFuncionario = new MySqlConnection();

            MySqlCommand cmdFuncionario = new MySqlCommand();

            public string Mensagem { get; set; }

            public void Inserir(Funcionario funcionario)
            {
                //verificar se já existe algum funcionario com o mesmo nome

                try
                {
                        conFuncionario.ConnectionString = Dados.strConexao;

                        cmdFuncionario.CommandType = CommandType.StoredProcedure;

                        cmdFuncionario.CommandText = "`inserir_funcionario`";

                        cmdFuncionario.Connection = conFuncionario;

                        cmdFuncionario.Parameters.AddWithValue("nome", funcionario.Nome);
                        cmdFuncionario.Parameters.AddWithValue("cpf", funcionario.Cpf);
                        cmdFuncionario.Parameters.AddWithValue("senha", funcionario.Senha);
                        cmdFuncionario.Parameters.AddWithValue("usuario", funcionario.Usuario);
                        cmdFuncionario.Parameters.AddWithValue("tipo", funcionario.Tipo);
                        cmdFuncionario.Parameters.AddWithValue("email", funcionario.Email);
                        cmdFuncionario.Parameters.AddWithValue("celular", funcionario.Celular);
                        cmdFuncionario.Parameters.AddWithValue("especialidade", funcionario.Especialidade);

                        conFuncionario.Open();

                        funcionario.Id = Convert.ToInt32(cmdFuncionario.ExecuteScalar());          
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    conFuncionario.Close();
                }
            }

            public void Alterar(Funcionario funcionario)
            {
                try
                {
                    conFuncionario.ConnectionString = Dados.strConexao;
                    cmdFuncionario.CommandType = CommandType.StoredProcedure;
                    cmdFuncionario.CommandText = "alterar_funcionario";
                    cmdFuncionario.Connection = conFuncionario;

                    cmdFuncionario.Parameters.AddWithValue("id", funcionario.Id);
                    cmdFuncionario.Parameters.AddWithValue("nome", funcionario.Nome);
                    cmdFuncionario.Parameters.AddWithValue("cpf", funcionario.Cpf);
                    cmdFuncionario.Parameters.AddWithValue("senha", funcionario.Senha);
                    cmdFuncionario.Parameters.AddWithValue("usuario", funcionario.Usuario);
                    cmdFuncionario.Parameters.AddWithValue("tipo", funcionario.Tipo);
                    cmdFuncionario.Parameters.AddWithValue("email", funcionario.Email);
                    cmdFuncionario.Parameters.AddWithValue("celular", funcionario.Celular);
                    cmdFuncionario.Parameters.AddWithValue("especialidade", funcionario.Especialidade);

                    conFuncionario.Open();
                    cmdFuncionario.ExecuteNonQuery();
            }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    conFuncionario.Close();
                }
            }

            public bool Excluir(Funcionario funcionario)
            {

                int resultado = 0;
                bool resposta = false;

                try
                {
                    conFuncionario.ConnectionString = Dados.strConexao;
                    cmdFuncionario.Connection = conFuncionario;
                    cmdFuncionario.CommandType = CommandType.StoredProcedure;
                    cmdFuncionario.CommandText = "excluir_funcionario";
                    cmdFuncionario.Parameters.AddWithValue("id_funcionario", funcionario.Id);
                    conFuncionario.Open();
                    resultado = cmdFuncionario.ExecuteNonQuery();

                    if (resultado != 1)
                    {
                        throw new Exception("Não foi possível excluir o funcionario.");
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
                    conFuncionario.Close();
                }
            }

            public ListaFuncionario ListagemFuncionarios(string filtro)
            {
                try
                {
                //criar procedure selecionar_funcionario
                    ListaFuncionario objListaClientes = new ListaFuncionario();
                    conFuncionario.ConnectionString = Dados.strConexao;
                    cmdFuncionario.Connection = conFuncionario;
                    cmdFuncionario.CommandType = CommandType.StoredProcedure;
                    cmdFuncionario.CommandText = "selecionar_funcionario";
                    cmdFuncionario.Parameters.AddWithValue("filtro", filtro);
                    conFuncionario.Open();

                    MySqlDataReader dr = cmdFuncionario.ExecuteReader();
                    cmdFuncionario.Parameters.Clear();                           
                    if (dr.HasRows == true)
                    {
                        while (dr.Read())
                        {
                            // Cria uma instâncoa para o objeto cliente
                            Funcionario funcionario = new Funcionario();

                        
                            funcionario.Id = int.Parse(dr["fun_id"].ToString());
                            funcionario.Nome = dr["fun_nome"].ToString();
                            funcionario.Cpf = dr["fun_cpf"].ToString();
                            funcionario.Senha = dr["fun_senha"].ToString(); // 
                            funcionario.Usuario = dr["fun_usuario"].ToString();
                            funcionario.Tipo = dr["fun_tipo"].ToString();
                            funcionario.Email = dr["fun_email"].ToString();
                            funcionario.Celular = dr["fun_celular"].ToString();
                            funcionario.Especialidade = dr["fun_especialidade"].ToString();

                        objListaClientes.Add(funcionario);
                        
                        }
                    }
                    return objListaClientes;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    conFuncionario.Close();
                }
            }
        

    }
}
