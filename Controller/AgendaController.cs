using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence;
using Model;
using System.Data;

namespace Controller
{
    public class AgendaController
    {
        RepositoryAgenda objAgendaDal = new RepositoryAgenda();

        private string mensagem;
        public string Mensagem
        {
            get { return mensagem; }
            set { mensagem = value; }
        }

        // Receberá o resultado da autenticação.
        private bool resposta = false;


        public bool Inserir(Agenda agenda)
        {
            try
            {
                if (ValidarDadosInserir(agenda))
                {
                    objAgendaDal.Inserir(agenda);
                    if (objAgendaDal.Mensagem == "")
                    {
                        Mensagem = "Agendamento realizado com sucesso.";
                        return true;
                    }
                    else
                    {
                        Mensagem = "O agendamento não foi realizado.";
                        return false;
                    }
                }
                else
                {
                    Mensagem = objAgendaDal.Mensagem;
                    return false;
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public void Alterar(Agenda agenda)
        {
            try
            {
                objAgendaDal.Alterar(agenda);
                Mensagem = "Agendamento alterado com sucesso!";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Excluir(Agenda agenda)
        {
            try
            {
                resposta = false;

                resposta = objAgendaDal.Excluir(agenda);
                if (resposta == false)
                {
                    //essa mensagem deve ser exibida em caso de falha ao salvar
                    //porem deve ser repensada
                    Mensagem = objAgendaDal.Mensagem;
                }
                else
                {
                    Mensagem = "Agendamento excluído com sucesso!";
                }

                return resposta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ListaAgenda ListagemConsultas(string filtro)
        {
            try
            {
                return objAgendaDal.ListagemAgendas(filtro);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable MostrarFuncionarios()
            => objAgendaDal.ListarFuncionarios();

        public DataTable MostrarPacientes()
            => objAgendaDal.ListarPacientes();

        public bool ValidarDadosInserir(Agenda agenda)
        {
           bool result;
           return objAgendaDal.VerificarDisponibilidade(agenda);
        }
    }
}
