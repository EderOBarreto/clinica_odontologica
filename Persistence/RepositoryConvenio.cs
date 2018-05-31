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
    public class RepositoryConvenio
    {
        MySqlConnection conConvenio = new MySqlConnection(Dados.strConexao);
        MySqlCommand cmdConvenio = new MySqlCommand();

        public void Inserir(Convenio novo_convenio)
        {
            try
            {
                if (exists(novo_convenio))
                    throw new Exception("Convenio já existe.");

                cmdConvenio.CommandType = CommandType.StoredProcedure;
                cmdConvenio.CommandText = "inserir_convenio";
                cmdConvenio.Connection = conConvenio;

                cmdConvenio.Parameters.AddWithValue("convenio", novo_convenio.NomeConvenio);
                cmdConvenio.Parameters.AddWithValue("cnpj", novo_convenio.Cnpj);
                cmdConvenio.Parameters.AddWithValue("contato", novo_convenio.Contato);
                cmdConvenio.Parameters.AddWithValue("telefone", novo_convenio.Telefone);
                cmdConvenio.Parameters.AddWithValue("email", novo_convenio.Email);

                conConvenio.Open();

                cmdConvenio.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmdConvenio.Parameters.Clear();
                conConvenio.Close();
            }
        }

        public bool Excluir(Convenio convenio)
        {
            try
            {
                cmdConvenio.CommandType = CommandType.StoredProcedure;
                cmdConvenio.CommandText = "excluir_convenio";
                cmdConvenio.Connection = conConvenio;

                cmdConvenio.Parameters.AddWithValue("id_convenio", convenio.Cid);

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
                cmdConvenio.Parameters.AddWithValue("email", convenio.Email);

                conConvenio.Open();

                cmdConvenio.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmdConvenio.Parameters.Clear();
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

                cmdConvenio.Parameters.AddWithValue("filtro", filtro);

                conConvenio.Open();

                MySqlDataReader dr = cmdConvenio.ExecuteReader();

                if (!dr.HasRows)
                    return lisc;

                while(dr.Read())
                { 
                    Convenio con = new Convenio();
                    con.Cid = int.Parse(dr["con_id"].ToString());
                    con.NomeConvenio = dr["con_convenio"].ToString();
                    con.Cnpj = dr["con_cnpj"].ToString();
                    con.Contato = dr["con_contato"].ToString();
                    con.Telefone = dr["con_telefone"].ToString();
                    con.Email = dr["con_email"].ToString();

                    lisc.Add(con);
                }

                return lisc;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmdConvenio.Parameters.Clear();
                conConvenio.Close();
            }
        }

        private bool exists(Convenio convenio)
        {
            try
            {
                cmdConvenio.CommandType = CommandType.StoredProcedure;
                cmdConvenio.CommandText = "existe_convenio";
                cmdConvenio.Connection = conConvenio;

                cmdConvenio.Parameters.AddWithValue("pcnpj", convenio.Cnpj);

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
                cmdConvenio.Parameters.Clear();
                conConvenio.Close();
            }
        }

        public string ListaById(int id)
        {
            try
            {
                cmdConvenio.CommandType = CommandType.StoredProcedure;
                cmdConvenio.CommandText = "selecionar_convenio_id";
                cmdConvenio.Connection = conConvenio;

                cmdConvenio.Parameters.AddWithValue("cid", id);

                conConvenio.Open();

                // Procedure select só no con_convenio
                string con_convenio_nome = cmdConvenio.ExecuteScalar().ToString();

                return con_convenio_nome;
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

        public DataTable ListagemConvenio()
        {
            try
            {
                cmdConvenio.Connection = conConvenio;
                cmdConvenio.CommandType = CommandType.StoredProcedure;
                cmdConvenio.CommandText = "buscar_convenios_combo";

                conConvenio.Open();

                MySqlDataReader dr = cmdConvenio.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);

                return dt;
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
