using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OMNI写号辅助工具
{
    public partial class SelectResultView : Form
    {
        public SelectResultView()
        {
            InitializeComponent();
            Text = "选择烧录结果";
            OKBtn.Click += OKAction;
            NGBtn.Click += NGAction;
        }

        private void OKAction(object sender,EventArgs e)
        {
            DialogResult = DialogResult.Yes;
        }

        private void NGAction(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }
    }
}
