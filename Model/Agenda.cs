using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Agenda
    {
        private int id_consulta;
        private int id_paciente;
        //pode ser mais de 1 funcionario?
        private int id_funcionario;
        private DateTime data_agendamento;
        private DateTime data_consulta;
        private DateTime data_retorno;
        private DateTime hora_inicio;
        private DateTime hora_final;
        private float preco;
        //ainda a serem definidos
        private byte [] exames;
        private byte [] diagnostico;

        public int Id_consulta { get => id_consulta; set => id_consulta = value; }
        public int Id_paciente { get => id_paciente; set => id_paciente = value; }
        public int Id_funcionario { get => id_funcionario; set => id_funcionario = value; }
        public DateTime Data_agendamento { get => data_agendamento; set => data_agendamento = value; }
        public DateTime Data_consulta { get => data_consulta; set => data_consulta = value; }
        public DateTime Data_retorno { get => data_retorno; set => data_retorno = value; }
        public float Preco { get => preco; set => preco = value; }
        public byte[] Exames { get => exames; set => exames = value; }
        public byte[] Diagnostico { get => diagnostico; set => diagnostico = value; }
        public DateTime Hora_inicio { get => hora_inicio; set => hora_inicio = value; }
        public DateTime Hora_final { get => hora_final; set => hora_final = value; }
    }
}
