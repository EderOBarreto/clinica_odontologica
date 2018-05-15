using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Funcionario
    {
        //declarando atributos
        public int Id { get; set; }
        //id poderia ser alterado para GUID
        public String Nome { get; set; }
        public String Cpf { get; set; }
        public String Senha { get; set; }
        public String Usuario { get; set; }
        public String Tipo { get; set; }
        public String Email { get; set; }
        public String Celular { get; set; }
        public String Especialidade { get; set; }
    }
}
