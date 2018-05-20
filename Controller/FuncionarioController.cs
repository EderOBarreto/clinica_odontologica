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

        // Mensagem de erros.

        private string mensagem;
        public string Mensagem {
            get { return mensagem; }
            set { mensagem = value; }
        }

        // Receberá o resultado da autenticação.
        private bool resposta = false;

        // Resultado da criptografia.
        private bool verifica;

        // Verifica a quantidade de caracteres do nome, login e senha.
        private bool valida;

        //Fazer método para login ->

        //validar campos ->



        public void Inserir(Funcionario funcionario)
        {
            try
            {
                    
                    objFuncionariosDal.Inserir(funcionario);
                    if (objFuncionariosDal.Mensagem == "")
                    {
                        mensagem = "Cliente incluído com sucesso";
                    }
                    else
                    {
                        mensagem = "O Cliente não foi incluído!";
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
                //valida = validaFuncionario(funcionario);
                
                    //Chama do método GeraSenhaMD5 para gerar uma nova senha em caso de alteração de dados.
                    //funcionario.Funsenha = GeraSenhaMD5(funcionario.Funsenha);
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
    }
}
