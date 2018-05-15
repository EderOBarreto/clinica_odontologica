using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Agenda
    {
        public int id_consulta { get; set; }
        public int id_paciente { get; set; }
        //pode ser mais de 1 funcionario?
        public int id_funcionario { get; set; }
        public DateTime data_agendamento { get; set; }
        public DateTime data_consulta { get; set; }
        public DateTime data_retorno { get; set; }
        public DateTime hora { get; set; }
        public float preco {get; set;}
        //ainda a serem definidos
        public String exames { get; set; }
        public String diagnostico { get; set; }
       
    }
}
