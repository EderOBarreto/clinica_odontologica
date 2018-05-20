using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model;
using Persistence;
using System.Text.RegularExpressions;

namespace Controller
{
    class ConvenioController
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

        private string SqlIFilter(string to_validate)
        {
            string[] trash = {"SELECT", "'", ";",
                                "\"", "--", "INSERT", "=",
                                "UPDATE", "DELETE"};

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
                if (!ValidarDocumentos.ValidaCnpj(convenio.Cnpj))
                    throw new Exception("CNPJ inválido.");

                convenio.NomeConvenio = SqlIFilter(convenio.NomeConvenio.ToUpper());
                if (convenio.NomeConvenio.Length < 3)
                    throw new Exception("Nome do convenio inválido.");

                convenio.Contato = SqlIFilter(convenio.Contato.ToUpper());
                if (convenio.Contato.Length > 3)
                    throw new Exception("Contato inválido.");

                if (!validarTelefone(convenio.Telefone))
                    throw new Exception("Telefone inválido.");
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
