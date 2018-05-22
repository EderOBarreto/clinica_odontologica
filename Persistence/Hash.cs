using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
        class Hash
        {
            private HashAlgorithm _algoritmo;

            public Hash(HashAlgorithm algoritmo)
            {
                _algoritmo = algoritmo;
            }

            public string CriptografarSenha(string senha)
            {
                var encodedValue = Encoding.UTF8.GetBytes(senha);
                var encryptedPassword = _algoritmo.ComputeHash(encodedValue);

                var sb = new StringBuilder();
                foreach (var caracter in encryptedPassword)
                {
                    sb.Append(caracter.ToString("X2"));
                }

                return sb.ToString();
            }

            public bool VerificarSenha(string senhaDigitada, string senhaCadastrada)
            {
            /*if (string.IsNullOrEmpty(senhaCadastrada))
                throw new NullReferenceException("Cadastre uma senha.");*/

            //return CriptografarSenha(senhaDigitada) == senhaCadastrada;
            return senhaDigitada == senhaCadastrada;
            }

            private static string CreateSalt(int size)
            {
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                byte[] buff = new byte[size];
                rng.GetBytes(buff);
                return Convert.ToBase64String(buff);
            }
        }
}
