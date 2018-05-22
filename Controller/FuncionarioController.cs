using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Persistence;

namespace Controller
{
    public class FuncionarioController
    {
        RepositoryFuncionarios objFuncionariosDal = new RepositoryFuncionarios();

        private bool autentica = false;

        public bool getAutentica()
        {
            return autentica;
        }

        private string mensagem;
        public string Mensagem {
            get { return mensagem; }
            set { mensagem = value; }
        }

        // Receberá o resultado da autenticação.
        private bool resposta = false;
        //Fazer método para login ->

        public void Inserir(Funcionario funcionario)
        {
            try
            {       
                    objFuncionariosDal.Inserir(funcionario);
                    if (objFuncionariosDal.Mensagem == "")
                    {
                        mensagem = "Funcionário incluído com sucesso";
                    }
                    else
                    {
                        mensagem = "O funcionário não foi incluído!";
                    }              
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public void Alterar(Funcionario funcionario)
        {
            try
            {
                    objFuncionariosDal.Alterar(funcionario);
                    Mensagem = "Funcionário alterado com sucesso!";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Excluir(Funcionario funcionario)
        {
            try
            {
                resposta = false;
               
                resposta = objFuncionariosDal.Excluir(funcionario);
                if (resposta == false)
                {
                    //essa mensagem deve ser exibida em caso de falha ao salvar
                    //porem deve ser repensada
                    Mensagem = objFuncionariosDal.Mensagem;
                }
                else
                {
                    Mensagem = "Funcionário excluído com sucesso!";
                }
                
                return resposta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ListaFuncionario ListagemFuncionarios(string filtro)
        {
            try
            {
                return objFuncionariosDal.ListagemFuncionarios(filtro);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async void RealizarLogin(string usuario, string senha,string tipo_acesso)
        {
            try
            {
               autentica =  await objFuncionariosDal.VerificarUsuario(usuario, senha, tipo_acesso);
            }
            catch
            {
                autentica = false;
            }
        }
    }
}
