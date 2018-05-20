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
    class RepositoryConvenio
    {
        MySqlConnection conConvenio = new MySqlConnection(Dados.strConexao);
        MySqlCommand cmdConvenio = new MySqlCommand();
        
        public void Inserir(Convenio novo_convenio)
        {
            try
            {
                if (exists(novo_convenio)) // TODO
                    throw new Exception("Convenio já existe.");

                cmdConvenio.CommandType = CommandType.StoredProcedure;
                cmdConvenio.CommandText = "inserir_convenio";
                cmdConvenio.Connection = conConvenio;

                cmdConvenio.Parameters.AddWithValue("convenio", novo_convenio.NomeConvenio);
                cmdConvenio.Parameters.AddWithValue("cnpj", novo_convenio.Cnpj);
                cmdConvenio.Parameters.AddWithValue("contato", novo_convenio.Contato);
                cmdConvenio.Parameters.AddWithValue("telefone", novo_convenio.Telefone);

                conConvenio.Open();

                cmdConvenio.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conConvenio.Close();
            }
        }

        public bool Excluir(Convenio convenio)
        {
            try
            {
                cmdConvenio.CommandType = CommandType.StoredProcedure;
                cmdConvenio.CommandText = "excluir_paciente";
                cmdConvenio.Connection = conConvenio;

                cmdConvenio.Parameters.AddWithValue("id_paciente", convenio.Cid);

                conConvenio.Open();

                if (cmdConvenio.ExecuteNonQuery() != 1)
                {
                    throw new Exception("Convenio não encontrado.");
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conConvenio.Close();
            }
        }

        public void Alterar(Convenio convenio)
        {
             try
            {
                cmdConvenio.CommandType = CommandType.StoredProcedure;
                cmdConvenio.CommandText = "alterar_convenio";
                cmdConvenio.Connection = conConvenio;

                cmdConvenio.Parameters.AddWithValue("id", convenio.Cid);
                cmdConvenio.Parameters.AddWithValue("convenio", convenio.NomeConvenio);
                cmdConvenio.Parameters.AddWithValue("cnpj", convenio.Cnpj);
                cmdConvenio.Parameters.AddWithValue("contato", convenio.Contato);
                cmdConvenio.Parameters.AddWithValue("telefone", convenio.Telefone);

                conConvenio.Open();

                cmdConvenio.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conConvenio.Close();
            }
        }

        public ListaConvenios ListagemConvenio(string filtro)
        {
            ListaConvenios lisc = new ListaConvenios();
            try
            {
                cmdConvenio.CommandType = CommandType.StoredProcedure;
                cmdConvenio.CommandText = "selecionar_convenio";
                cmdConvenio.Connection = conConvenio;

                cmdConvenio.Parameters.AddWithValue("pfiltro", filtro);

                conConvenio.Open();

                MySqlDataReader dr = cmdConvenio.ExecuteReader();
                cmdConvenio.Parameters.Clear();

                if (dr.HasRows)
                {
                    foreach (DataRow row in dr)
                    {
                        Convenio con = new Convenio();
                        con.Cid = int.Parse(row["con_id"].ToString());
                        con.NomeConvenio = row["con_convenio"].ToString();
                        con.Cnpj = row["con_cnpj"].ToString();
                        con.Contato = row["con_contato"].ToString();
                        con.Telefone = row["con_telefone"].ToString();

                        lisc.Add(con);
                    }
                }
                return lisc;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conConvenio.Close();
            }
        }

        private bool exists(Convenio convenio)
        {
            /*
             * TODO:
             *      Rever.
             *      Faz sentido ter isso?
             **/

            try
            {
                cmdConvenio.CommandType = CommandType.StoredProcedure;
                cmdConvenio.CommandText = "convenio_existe";
                cmdConvenio.Connection = conConvenio;

                cmdConvenio.Parameters.AddWithValue("conv_nome", convenio.NomeConvenio);

                conConvenio.Open();

                int qtdeConvenios = cmdConvenio.ExecuteNonQuery();

                return qtdeConvenios > 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conConvenio.Close();
            }
        }
    }
}
