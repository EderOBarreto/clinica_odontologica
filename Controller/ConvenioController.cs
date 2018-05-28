using System;

using Model;
using Persistence;
using System.Text.RegularExpressions;
using System.Data;

namespace Controller
{
    public class ConvenioController
    {
        RepositoryConvenio objConvenio = new RepositoryConvenio();

        public void Inserir(Convenio convenio)
        {
            try
            {
                ValidarDados(convenio);
                objConvenio.Inserir(convenio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Excluir(Convenio convenio)
        {
            try
            {
                return objConvenio.Excluir(convenio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Alterar(Convenio convenio)
        {
            try
            {
                ValidarDados(convenio);

                objConvenio.Alterar(convenio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ListaConvenios ListarConvenios(string filtro)
        {
            try
            {
                filtro = SqlIFilter(filtro);

                return objConvenio.ListagemConvenio(filtro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListarConvenios()
        {
            try
            {
                return objConvenio.ListagemConvenio();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string SqlIFilter(string to_validate)
        {
            string[] trash = {"select", "'", ";",
                                "\"", "--", "insert", "=",
                                "update", "delete"};

            foreach (string item in trash)
            {
                to_validate = to_validate.Replace(item, "");
            }

            return to_validate.Trim();
        }

        private void ValidarDados(Convenio convenio)
        {
            try
            {
                convenio.Cnpj = convenio.Cnpj.Replace(",", "").Replace(".", "").Replace("/", "").Replace("-", "");
                if (!ValidarDocumentos.ValidaCnpj(convenio.Cnpj))
                    throw new Exception("CNPJ inválido.");

                convenio.NomeConvenio = SqlIFilter(convenio.NomeConvenio.ToLower());
                convenio.NomeConvenio = convenio.NomeConvenio.ToLower();
                if (convenio.NomeConvenio.Length < 3)
                    throw new Exception("Nome do convenio inválido.");

                convenio.Contato = convenio.Contato.ToLower();
                convenio.Contato = SqlIFilter(convenio.Contato.ToLower());
                if (convenio.Contato.Length > 3)
                    throw new Exception("Contato inválido.");

                if (!validarTelefone(convenio.Telefone))
                    throw new Exception("Telefone inválido.");

               convenio.Telefone = convenio.Telefone.Replace("(", "")
                                                                    .Replace(")", "")
                                                                    .Replace(".", "")
                                                                    .Replace(",", "")
                                                                    .Replace("-", "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool validarTelefone(string telefone)
        {
            Match matchCelular = Regex.Match(telefone, "^[1-9]{2}9[1-9][0-9]{7}$");
            if (matchCelular.Success)
                return true;

            Match matchTelefone = Regex.Match(telefone, "^[0-9]{8}$");
            if (matchTelefone.Success)
                return true;

            return false;
        }
    }
}
