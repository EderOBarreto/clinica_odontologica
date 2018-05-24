using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Agenda
    {
        private int id_consulta;
        private int id_paciente;
        private int id_funcionario;
        private DateTime data_consulta;
        private DateTime hora_inicio;
        private DateTime hora_final;
        private float preco;
        private string diagnostico;
        private byte[] exames;
        [Bindable(true)]
        [Category("Data Input fields")]
        [Localizable(true)]

        public int Id_consulta { get => id_consulta; set => id_consulta = value; }
        public int Id_paciente { get => id_paciente; set => id_paciente = value; }
        public int Id_funcionario { get => id_funcionario; set => id_funcionario = value; }
        public DateTime Data_consulta { get => data_consulta; set => data_consulta = value; }
        public float Preco { get => preco; set => preco = value; }
        public byte[] Exames
        {
            get => exames;
            set
            {
                if (value.Equals(DBNull.Value) || value == null)
                {
                    exames = Encoding.UTF8.GetBytes(String.Empty);
                }
                else
                {
                    exames = value;
                }

            }
        }
        public DateTime Hora_inicio { get => hora_inicio; set => hora_inicio = value; }
        public DateTime Hora_final { get => hora_final; set => hora_final = value; }
        public string Diagnostico { get => diagnostico; set => diagnostico = value; }
    }
}
