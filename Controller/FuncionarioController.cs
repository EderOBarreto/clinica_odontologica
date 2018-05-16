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
        public string Mensagem { get; set; }

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
                //valida = validaFuncionario(funcionario);
               
                    // Chamada da funçao que irá gerar a criptografia.
                    // Fazer método para gerar senha
                    //funcionario.Funsenha = GeraSenhaMD5(funcionario.Funsenha);
                    objFuncionariosDal.Inserir(funcionario);
                    if (funcionario.Id != 0)
                    {
                        Mensagem = "Funcionário inserido com sucesso!";
                    }
                    else
                    {
                        Mensagem = "O funcionário não pôde ser inserido!";
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
                if (funcionario.Id < 1)
                {
                    Mensagem = "Selecione um funcionario antes de excluir.";
                }
                else
                {
                    resposta = objFuncionariosDal.Excluir(funcionario);
                    if (resposta == false)
                    {
                        Mensagem = objFuncionariosDal.Mensagem;
                    }
                    else
                    {
                        Mensagem = "Funcionário excluído com sucesso!";
                    }
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

        /*  private string GeraSenhaMD5(string texto)
        {
          //Cria instância da classe MD5CryptoServiceProvider
            MD5CryptoServiceProvider MD5provider = new MD5CryptoServiceProvider();

            //Gera o hash do texto. Neste caso o hash é a encriptação.
            //O objeto valorHash é um vetor do tipo byte.
            //O tipo byte lê e armazena caracteres hexadecimais.
            byte[] valorHash = MD5provider.ComputeHash(Encoding.Default.GetBytes(texto));

            //A classe StringBuilder é utilizado para fazer concatenação de strings.
            //A diferença de StringBuilder para String é a seguinte:
            //Quando concatenamos strings, um novo objeto de string é instanciado a cada 
            //concatenação tornando o consumo de memória muito alto. Já o StringBuilder, 
            //não cria instancias de objetos e permite concatenar textos simples, tornando
            //a operação de concatenação bem mais rápida e com baixo consumo de memória.
            StringBuilder str = new StringBuilder();

            //retorna o hash encriptado e o converte em formato hexadecimal.
            for (int contador = 0; contador < valorHash.Length; contador++)
            {
                //O formato "x2" exibe caracteres no formato hexadecimal para variáveis do tipo byte, convertida para string.
                str.Append(valorHash[contador].ToString("x2"));
            }
            return str.ToString();
        }*/

        /*private bool VerificaSenhaMD5(ModeloFuncionarios funcionario)
        {
            //gera criptografia para o texto da senha que retornará criptografada.
            string senha2 = GeraSenhaMD5(funcionario.Funsenha);

            //funcionario.funsenha recebe a senha criptografada para ser procurada na lista 
            funcionario.Funsenha = senha2;

            //Cria uma StringComparer e compara o hash gerado com o armazenado.
            //A classe stringComparer compara apenas variáveis, mas não compara objetos.
            //Assim, esta classe não compara funcionario.Funsenha porque funcionario é objeto.
            StringComparer strcomparer = StringComparer.OrdinalIgnoreCase;

            //A variável valorHashArmazenado receberá a senha criptografada do banco.
            string valorHashArmazenado = "";

            string filtro = "";

            ListaFuncionario listaFun = new ListaFuncionario();

            //Busca a lista de funcionarios.
            listaFun = ListagemFuncionarios(filtro);

            //Expressão Lambda que procura a senha criptografada na lista com f.Funsenha
            //e a compara com funcionario.Funsenha
            funcionario = listaFun.Find(f => f.Funsenha == funcionario.Funsenha);
            //Se o funcionario for diferente de nulo!
            if (funcionario != null)
            {
                //A variável valorHashArmazenado resgata a senha armazenada no banco.
                valorHashArmazenado = funcionario.Funsenha;
            }
            else
            {
                Mensagem = "Senha Não Localizada!";
            }

            //Se o valores dos hashs foram iguais, então retorna true, senão, false
            if (strcomparer.Compare(senha2, valorHashArmazenado).Equals(0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }*/


    }
}
