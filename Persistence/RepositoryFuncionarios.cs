﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Model;
using MySql.Data.MySqlClient;

namespace Persistence
{
    public class RepositoryFuncionarios
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
                ListaFuncionario objListaFuncionarios = new ListaFuncionario();

                conFuncionario.ConnectionString = Dados.strConexao;
                cmdFuncionario.CommandType = CommandType.StoredProcedure;
                cmdFuncionario.CommandText = "selecionar_funcionario";
                cmdFuncionario.Parameters.AddWithValue("filtro", filtro);
                cmdFuncionario.Connection = conFuncionario;
                conFuncionario.Open();

                MySqlDataReader dr = cmdFuncionario.ExecuteReader();
                cmdFuncionario.Parameters.Clear();

                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        // Cria uma instância para o objeto funcionario
                        Funcionario funcionario = new Funcionario();

                        funcionario.Id = int.Parse(dr["fun_id"].ToString());
                        funcionario.Nome = dr["fun_nome"].ToString();
                        funcionario.Especialidade = dr["fun_especialidade"].ToString();
                        funcionario.Tipo = dr["fun_tipo"].ToString();
                        funcionario.Usuario = dr["fun_usuario"].ToString();
                        funcionario.Cpf = dr["fun_cpf"].ToString();
                        funcionario.Email = dr["fun_email"].ToString();
                        funcionario.Celular = dr["fun_celular"].ToString();

                        objListaFuncionarios.Add(funcionario);
                    }
                }
                return objListaFuncionarios;
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

        /*public async Task<bool> VerificarUsuario(string CPF, string senha, Funcionario funcionario)
        {
            var hash = new Hash(SHA512.Create());

            if (string.IsNullOrEmpty(senha) || string.IsNullOrEmpty(CPF))
            {
                throw new ArgumentNullException();
            }

            string hashTxtSenha = null;

            try
            {
                //using (var sqlConnection = CreateConnection() as SqlConnection)
                {
                    SqlTransaction transaction;
                    //await sqlConnection.OpenAsync();

                   // transaction = sqlConnection.BeginTransaction("VerificaFuncionario");
                    using (SqlCommand command = new SqlCommand("VerificaFuncionario", sqlConnection, transaction))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@CPF", CPF));

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                funcionario.Senha = reader["Senha"] as string;
                                funcionario.Nome = (reader["Nome"] as string).Trim();
                                var guid = reader["UserGuid"] as string;
                                //funcionario.NomeFuncao = reader["Funcao"] as string;
                                //funcionario.Prioridade = Convert.ToInt32(reader["ID_Prioridade"]);

                                hashTxtSenha = hash.CriptografarSenha(senha + guid);
                            }
                            else
                            {
                                return false;
                            }
                        }

                        transaction.Commit();

                        if (hash.VerificarSenha(hashTxtSenha, funcionario.Senha))
                        {
                            return true;
                        }
                        return false;
                    }
                }
            }
            catch (InvalidOperationException e)
            {
                throw new InvalidOperationException("Problemas ao se conectar com o banco.", e);
            }

            catch (SqlException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new Exception("Problemas para verificar usuário.", e);
            }
        }*/
    }
}
