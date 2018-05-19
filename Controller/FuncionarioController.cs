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
                    if (validaCpf(funcionario.Cpf))
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
                    else if (verifica == false)
                    {
                        mensagem = "CPF inválido!";
                    
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

        private bool validaCpf(string cpf)
        {
            int[] multiplicador1;
            int[] multiplicador2;

            multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            // cpf = 111.444.777-05
            cpf = cpf.Replace(",", "").Replace(".", "").Replace("-", "");
            // cpf = 11144477705
            if (cpf.Length != 11)
            {
                return false;
            }
            else
            {
                tempCpf = cpf.Substring(0, 9);
                soma = 0;
                // tempCpf = 1,1,1,4,4,4,7,7,7
                // Multiplicador1 = 10, 9, 8, 7, 6, 5, 4, 3, 2
                for (int i = 0; i < 9; i++)
                {
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
                }
                resto = soma % 11;
                if (resto < 2)
                {
                    digito = "0";
                }
                else
                {
                    digito = (11 - resto).ToString();
                }
                // tempCpf = 1114447773
                tempCpf += digito;
                soma = 0;
                for (int i = 0; i < 10; i++)
                {
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
                }
                resto = soma % 11;
                if (resto < 2)
                {
                    digito = "0";
                }
                else
                {
                    digito = (11 - resto).ToString();
                }
                tempCpf += digito;
                //return tempCpf.EndsWith(digito);
                if (tempCpf == cpf)
                    return true;
                else
                {
                    // cpf = tempCpf;
                    return false;
                }
            }
        }

    }
}
