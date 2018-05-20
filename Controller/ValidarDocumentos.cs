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
            string[] cpfsInvalidos = {
                "111,111,111-11","222,222,222-22","333,333,333-33",
                "444,444,444-44","555,555,555-55",
                "666,666,666-66","777,777,777-77",
                "888,888,888-88","999,999,999-99"};

            foreach ( string c in cpfsInvalidos)
            {
                if (c == cpf)
                    return false;
            }

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

        public static bool ValidaCnpj(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            int soma;
            int resto;

            string digito;
            string tempCnpj;

            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            if (cnpj.Length != 14)
                return false;

            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            resto = (soma % 11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;

            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cnpj.EndsWith(digito);
        }
    }
}
