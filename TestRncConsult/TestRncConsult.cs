using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestRncConsult
{
    public partial class TestRncConsult : Form
    {
        public TestRncConsult()
        {
            InitializeComponent();
        }

        private void BBuscar_Click(object sender, EventArgs e)
        {
            coConsultRNC.ResultRnc result = new coConsultRNC.ResultRnc(); 
            List<coConsultRNC.ResultRnc> resultRncs = new List<coConsultRNC.ResultRnc>();
            coConsultRNC.coConsultRNC coConsultRNC = new coConsultRNC.coConsultRNC();
            result = coConsultRNC.GetNombre(tbRnc.Text);
            resultRncs.Add(result);
            dgDatos.AutoGenerateColumns = true;
            dgDatos.DataSource = resultRncs;

        }
    }
}
