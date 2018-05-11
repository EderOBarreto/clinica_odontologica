using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Agenda
    {
        private int id_consulta { get; set; }
        private int id_paciente { get; set; }
        //pode ser mais de 1 funcionario?
        private int id_funcionario { get; set; }
        private DateTime data_agendamento { get; set; }
        private DateTime data_consulta { get; set; }
        private DateTime data_retorno { get; set; }
        private DateTime hora { get; set; }
        private float preco {get; set;}
        //ainda a serem definidos
        private String exames { get; set; }
        private String diagnostico { get; set; }
       
    }
}
