using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SharpConfig;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace OMNI写号辅助工具
{
    public partial class MainView : Form
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        private bool IsRunning = false;
        private int LastWriteNum = 0;
        private int NewWriteNum = 0;
        private Timer timer;
        private bool IsUpdateing = false;
        private bool IsWriteing = false;
        private InputView inputView;

        public MainView()
        {
            InitializeComponent();
            Text = "OMNI写号辅助工具 - " + Application.ProductVersion;

            SetupBtn.Click += SetupAction;
            RunBtn.Click += RunAction;
        }

        private void SetupAction(object sender, EventArgs e)
        {
            var setupView = new SetupView();
            DialogResult result = setupView.ShowDialog();
            if (result == DialogResult.OK)
            {
                Form1_Load(null, null);

                ShowLog("设置已保存!");

            }
            else if (result == DialogResult.Cancel)
            {
                ShowLog("设置已取消!");
            }
        }

        private void RunAction(object sender, EventArgs e)
        {
            if (!IsRunning)
            {
                Invoke(new Action(() =>
                {
                    IsRunning = true;
                    RunBtn.Text = "运行中";
                    RunBtn.BackColor = Color.GreenYellow;
                    SetupBtn.Enabled = false;
                    ShowLog("开始运行!");
                    timer = new Timer
                    {
                        Interval = 1000
                    };
                    timer.Tick += RunAction_Tick;
                    timer.Start();
                }));
            }
            else
            {
                Invoke(new Action(() =>
                {
                    IsRunning = false;
                    RunBtn.Text = "未运行";
                    RunBtn.BackColor = Color.OrangeRed;
                    SetupBtn.Enabled = true;
                    ShowLog("停止运行!");
                    timer.Stop();
                }));
            }
        }

        private void RunAction_Tick(object sender, EventArgs e)
        {
            if (IsRunning)
            {
                if (!IsUpdateing & inputView == null)
                {
                    IsWriteing = true;
                    inputView = new InputView();
                    DialogResult result = inputView.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        ShowLog($"输入内容：{Properties.Settings.Default.InputValue}与服务器数据核对匹配，写入配置文件。");
                        SimpleCfg.SetValue(Properties.Settings.Default.UpdateFilePath, "SERIL_NUM", Properties.Settings.Default.InputValue);
                        var cfg = SimpleCfg.Read(Properties.Settings.Default.CheckFilePath);
                        string sn = cfg.ContainsKey("SERIL_NUM") ? cfg["SERIL_NUM"] : "";
                        if(sn == Properties.Settings.Default.InputValue)
                        {
                            ShowLog("数据写入文件成功!");
                        }
                        IsUpdateing = true;
                        IsWriteing = false;
                        
                    }else if(result == DialogResult.Abort)
                    {
                        RunBtn.PerformClick();
                    }

                    inputView = null;
                }
                if (IsUpdateing & !IsWriteing)
                {
                    var cfg = SimpleCfg.Read(Properties.Settings.Default.CheckFilePath);
                    
                    LastWriteNum = NewWriteNum;
                    NewWriteNum = int.Parse(cfg.ContainsKey("num") ? cfg["num"] : "0");
                    if(LastWriteNum != 0)
                    {
                        if (LastWriteNum < NewWriteNum)
                        {
                            IsWriteing = true;
                            IsUpdateing = false;
                            ShowLog($"{Properties.Settings.Default.InputValue}写号成功！");
                            ShowLog($"写号成功！第{NewWriteNum}次！");
                            if (Properties.Settings.Default.IsUploadStation)
                            {
                                UploadStation(true);
                            }
                            
                            Properties.Settings.Default.InputValue = "";
                        }
                    }
                    
                }
            }
        }

        private string Post(string Url, string jsonStr = "")
        {
            //创建一个HTTP请求  
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            //Post请求方式  
            request.Method = "POST";
            //内容类型
            request.ContentType = "application/json";
            //设置参数，并进行URL编码 
            string paraUrlCoded = jsonStr;//System.Web.HttpUtility.UrlEncode(jsonParas);   
            byte[] payload;
            //将Json字符串转化为字节  
            payload = System.Text.Encoding.UTF8.GetBytes(paraUrlCoded);
            //设置请求的ContentLength   
            request.ContentLength = payload.Length;
            //发送请求，获得请求流 
            Stream writer;
            try
            {
                writer = request.GetRequestStream();//获取用于写入请求数据的Stream对象
            }
            catch (Exception ex)
            {
                writer = null;
                throw ex;
            }
            //将请求参数写入流
            writer.Write(payload, 0, payload.Length);
            writer.Close();//关闭请求流
                           // String strValue = "";//strValue为http响应所返回的字符流
            HttpWebResponse response;
            try
            {
                //获得响应流
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                response = ex.Response as HttpWebResponse;
            }
            Stream s = response.GetResponseStream();
            //  Stream postData = Request.InputStream;
            StreamReader sRead = new StreamReader(s);
            string postContent = sRead.ReadToEnd();
            sRead.Close();
            return postContent;//返回Json数据
        }

        private bool UploadStation(bool IsPass)
        {
            bool res = false;
            try
            {
                string url = "http://192.168.8.8/box_cms_v3/mes_v1/postTestDataStr.php?model={0}&dut_number_name={1}&dut_number_value={2}&station={3}&elapsed=1&is_pass={4}&fix_id={5}&line_name=&retry_cnt=1&is_misdetect=false";
                string model = Properties.Settings.Default.Model;
                string fields = Properties.Settings.Default.Field;
                string input = Properties.Settings.Default.InputValue;
                string station = Properties.Settings.Default.Station;
                string fixid = Properties.Settings.Default.Fixid;
                url = string.Format(url, model, fields, input, station, IsPass, fixid);
                string jsonStr = Post(url);
                JObject jArray = (JObject)JsonConvert.DeserializeObject(jsonStr);
                if (jArray["resultCode"].ToString() == "0")
                {
                    res = true;
                }

            }
            catch (Exception ex)
            {
                ShowLog("上传工站异常:" + ex.Message);
            }
            return res;
        }

        private void ShowLog(string log)
        {
            Invoke(new Action(() =>
            {
                if (Logs.Text.Length > 0)
                {
                    Logs.AppendText(Environment.NewLine + DateTime.Now.ToString("HH:mm:ss") + " " + log);
                }
                else
                {
                    Logs.Text = DateTime.Now.ToString("HH:mm:ss") + " " + log;
                }
                Logs.SelectionStart = Logs.Text.Length;
                Logs.ScrollToCaret();
                WriteLog(log);
            }));
        }

        private void WriteLog(string log)
        {
            string FloderPath = AppDomain.CurrentDomain.BaseDirectory;
            string FilePath = FloderPath.Replace("/", "\\") + "Logs\\";
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }

            FilePath += DateTime.Now.ToString("yyyyMMdd") + "log.txt";
            StreamWriter writer = new StreamWriter(FilePath, true, System.Text.Encoding.UTF8);
            writer.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " " + log);
            writer.Close();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            ShowLog("机型：" + Properties.Settings.Default.Model);
            ShowLog("字段：" + Properties.Settings.Default.Field);
            ShowLog("工站：" + Properties.Settings.Default.Station);
            ShowLog("工位：" + Properties.Settings.Default.Fixid);
            ShowLog("更新文件路径：" + Properties.Settings.Default.UpdateFilePath);
            ShowLog("检查文件路径：" + Properties.Settings.Default.CheckFilePath);
            ShowLog("是否检查设备状态：" + Properties.Settings.Default.IsCheckDutState);
            ShowLog("是否上传过站信息：" + Properties.Settings.Default.IsUploadStation);
        }

        private void From1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
