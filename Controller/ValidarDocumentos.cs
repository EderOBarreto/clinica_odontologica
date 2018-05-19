using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public static class ValidarDocumentos
    {
        public static bool ValidaCpf(string cpf)
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
