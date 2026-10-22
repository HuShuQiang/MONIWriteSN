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
    public partial class SetupView : Form
    {
        public SetupView()
        {
            InitializeComponent();
            Text = "OMNI写号辅助工具 - 设置";

            OKBtn.Click += OKAction;
            CancelBtn.Click += CancelAction;
        }

        private void OKAction(object sender, EventArgs e)
        {
            Properties.Settings.Default.Model = Model.Text;
            Properties.Settings.Default.Field = Field.Text;
            Properties.Settings.Default.Station = Station.Text;
            Properties.Settings.Default.Fixid = Fixid.Text;
            Properties.Settings.Default.UpdateFilePath = UpdateFilePath.Text;
            Properties.Settings.Default.CheckFilePath = CheckFilePath.Text;
            Properties.Settings.Default.IsCheckDutState = IsCheckDutState.Checked;
            Properties.Settings.Default.IsUploadStation = IsUploadStation.Checked;
            Properties.Settings.Default.Save();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelAction(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void SetupView_Load(object sender, EventArgs e)
        {
            Model.Text = Properties.Settings.Default.Model;
            Field.Text = Properties.Settings.Default.Field;
            Station.Text = Properties.Settings.Default.Station;
            Fixid.Text = Properties.Settings.Default.Fixid;
            UpdateFilePath.Text = Properties.Settings.Default.UpdateFilePath;
            CheckFilePath.Text = Properties.Settings.Default.CheckFilePath;
            IsCheckDutState.Checked = Properties.Settings.Default.IsCheckDutState;
            IsUploadStation.Checked = Properties.Settings.Default.IsUploadStation;
        }

        private void SetupInfoBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "OEM配置文件|*.cfg";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                UpdateFilePath.Text = openFileDialog.FileName;
            }
        }

        private void StateInfoBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "ini文件|*.ini";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                CheckFilePath.Text = openFileDialog.FileName;
            }
        }
    }
}
