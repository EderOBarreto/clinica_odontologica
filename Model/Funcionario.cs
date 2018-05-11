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
        private int id { get; set; }
        //id poderia ser alterado para GUID
        private String nome { get; set; }
        private String cpf { get; set; }
        private String senha { get; set; }
        private String usuario { get; set; }
        private String tipo { get; set; }
        private String email { get; set; }
        private String celular { get; set; }
        private String especialidade { get; set; }
    }
}
