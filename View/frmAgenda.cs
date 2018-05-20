using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model;


namespace View
{
    public partial class frmAgenda : Form
    {
        Agenda agenda = new Agenda();

        public frmAgenda()
        {
            InitializeComponent();
        }

        private void frmAgenda_Load(object sender, EventArgs e)
        {

        }

        private void btnExame_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog odf = new OpenFileDialog())
                {
                    DialogResult res = odf.ShowDialog();
                    if (res == DialogResult.OK)
                    {
                        agenda.Exames = LerEConverterArquivos(odf.FileName);
                        txtExame.Text = odf.FileName;
                    }
                }
            }
            catch
            {

            }
        }

        private void btnDiagnostico_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog odf = new OpenFileDialog())
                {
                    DialogResult res = odf.ShowDialog();
                    if (res == DialogResult.OK)
                    {
                        agenda.Diagnostico = LerEConverterArquivos(odf.FileName);
                        txtDiagnostico.Text = odf.FileName;
                    }
                }
            }
            catch
            {

            }
        }

        private byte[] LerEConverterArquivos(String nomeArquivo)
        {
            byte[] buffer = null;

            StreamReader oStreamReader = new StreamReader(nomeArquivo);

            buffer = new byte[oStreamReader.BaseStream.Length];

            oStreamReader.BaseStream.Read(buffer, 0, buffer.Length);

            oStreamReader.Close();
            oStreamReader.Dispose();

            return buffer;
        }


    }
}
