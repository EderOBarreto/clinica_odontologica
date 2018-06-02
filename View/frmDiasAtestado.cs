using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace View
{
    public partial class frmDiasAtestado : Form
    {
        private string _cod_paciente;
        public frmDiasAtestado(string cod_paciente)
        {
            InitializeComponent();
            this._cod_paciente = cod_paciente;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                int dias = (int)nudDias.Value;

                RelatorioAtestado atestado = new RelatorioAtestado();

                atestado.SetParameterValue("id_paciente", _cod_paciente);
                atestado.SetParameterValue("dias_atestado", dias);

                frmImpressao imprimir = new frmImpressao(atestado);
                imprimir.Show();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Parece que algo estranho aconteceu...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
