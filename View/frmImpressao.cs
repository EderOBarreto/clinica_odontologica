using CrystalDecisions.CrystalReports.Engine;
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
    public partial class frmImpressao : Form
    {
        public frmImpressao(ReportClass relatorio)
        {
            InitializeComponent();

            try
            {
                crViewer.ReportSource = relatorio;
                crViewer.Refresh();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
