using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class Paciente
    {
        public int Pid { get; set; }
        public int Pid_conv { get; set; }
        public string Nome { get; set; }
        public string Sexo { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
    }
}
