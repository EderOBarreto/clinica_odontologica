using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Exames
    {
        private int id_exame;
        private string nome;
        private byte[] arquivo;

        public string Nome { get => nome; set => nome = value; }
        public byte[] Arquivo { get => arquivo; set => arquivo = value; }
        public int Id_exame { get => id_exame; set => id_exame = value; }
    }
}
