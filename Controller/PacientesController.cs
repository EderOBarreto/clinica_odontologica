using System;

using Model;
using Persistence;
using System.Data;

namespace Controller
{
    public class PacientesController
    {
        RepositoryPaciente objPaciente = new RepositoryPaciente();

        public void Inserir(Paciente paciente)
        {
            try
            {
                if (!ValidarDocumentos.ValidaCpf(paciente.Cpf))
                    throw new Exception("CPF inválido.");

                paciente.Nome = SqlIFilter(paciente.Nome.ToUpper());

                if (paciente.Nome.Length < 3)
                    throw new Exception("Nome do paciente inválido.");

                paciente.Email = paciente.Email.ToLower();

                objPaciente.Inserir(paciente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Alterar(Paciente paciente)
        {
            try
            {
                if (!ValidarDocumentos.ValidaCpf(paciente.Cpf))
                    throw new Exception("CPF inválido.");

                paciente.Nome = SqlIFilter(paciente.Nome);
                if (paciente.Nome.Length < 3)
                    throw new Exception("Nome do paciente inválido.");

                objPaciente.Alterar(paciente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Excluir(Paciente paciente)
        {
            try
            {
                return objPaciente.Excluir(paciente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListagemPacientes(string filtro)
        {
            filtro = SqlIFilter(filtro);

            DataTable dtResultado = new DataTable();

            try
            {
                RepositoryConvenio conv = new RepositoryConvenio();
                ListaPacientes pacs = objPaciente.ListagemPacientes(filtro);
                for (int i = 0, tam = pacs.Count; i < tam; i++)
                {
                    dtResultado.Rows[i][0] = pacs[i].Pid;
                    dtResultado.Rows[i][1] = pacs[i].Pid_conv;
                    dtResultado.Rows[i][2] = conv.ListaById(pacs[i].Pid_conv);
                    dtResultado.Rows[i][3] = pacs[i].Nome;
                    dtResultado.Rows[i][4] = pacs[i].Sexo;
                    dtResultado.Rows[i][5] = pacs[i].DataNascimento;
                    dtResultado.Rows[i][6] = pacs[i].Celular;
                    dtResultado.Rows[i][7] = pacs[i].Email;
                }
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return dtResultado;
        }

        private string SqlIFilter(string to_validate)
        {
            to_validate = to_validate.Trim();
            if (to_validate.Length == 0)
                return to_validate;

            string[] trash = {"SELECT", "'", ";",
                                "\"", "--", "INSERT", "=",
                                "UPDATE", "DELETE"};

            foreach (string item in trash)
            {
                to_validate = to_validate.Replace(item, "");
            }

            return to_validate.Trim();
        }
    }
}
