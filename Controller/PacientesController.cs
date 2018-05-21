using System;

using Model;
using Persistence;

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

        public ListaPacientes ListagemPacientes(string filtro)
        {
            filtro = SqlIFilter(filtro);

            try
            {
                return objPaciente.ListagemPacientes(filtro);
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
    }
}
