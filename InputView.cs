using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OMNI写号辅助工具
{
    public partial class InputView : Form
    {
        public InputView()
        {
            InitializeComponent();
            Text = "OMNI写号辅助工具 - 输入";
            Info.Text = $"输入字段值({Properties.Settings.Default.Field})";
        }

        private void InputValue_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(InputValue.Text))
                {
                    string CheckUrl = $"http://192.168.8.8/box_cms_v3/mes_v1/getDutFields.php?model={Properties.Settings.Default.Model}&dut_number_name={Properties.Settings.Default.Field}&dut_number_value={InputValue.Text}";
                    string result = Post(CheckUrl);
                    JObject Data = JsonConvert.DeserializeObject<JObject>(result);
                    if (Data["resultCode"].ToString() == "0")
                    {
                        if (CheckStation())
                        {
                            Properties.Settings.Default.InputValue = InputValue.Text;
                            Properties.Settings.Default.Save();
                            DialogResult = DialogResult.OK;
                            Close();
                        }
                        else
                        {
                            InputValue.Text = "";
                        }
                        
                    }
                    else
                    {
                        ShowTips($"服务器查询{Properties.Settings.Default.Field}{InputValue.Text}，结果：{Data["resultMsg"]}，请核查后重新输入！");
                        InputValue.Text = "";
                    }
                }
                else
                {
                    ShowTips("输入不能为空，请重新输入！");
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


        private void ShowTips(string tip)
        {
            Invoke(new Action(() =>
            {
                Tips.Text = tip;
                Tips.BackColor = Color.Red;
                WriteLog(tip);
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

        private void StopBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
            Close();
        }

        private bool CheckStation()
        {
            bool res = false;
            try
            {
                string url = "http://192.168.8.8/box_cms_v3/mes_v1/getDutTestFlowResult.php?model={0}&dut_number_name={1}&dut_number_value={2}";
                string model = Properties.Settings.Default.Model;
                string fields = Properties.Settings.Default.Field;
                string input = Properties.Settings.Default.InputValue;
                string station = Properties.Settings.Default.Station;
                url = string.Format(url, model, fields, input);

                string JsonStr = Post(url);
                JObject jArray = (JObject)JsonConvert.DeserializeObject(JsonStr);
                if (jArray["resultCode"].ToString() == "0" & jArray["resultMsg"].ToString() == "success")
                {
                    JObject stationinfo = (JObject)JsonConvert.DeserializeObject(jArray["stations"].ToString());
                    if (stationinfo.ContainsKey(station))
                    {
                        if (stationinfo[station].ToString() != "PASS")
                        {
                            res = true;
                        }
                        else
                        {
                            ShowTips("当前设备已通过 " + Properties.Settings.Default.Station + " 过站测试！！！不可重复测试");
                        }
                    }
                    else
                    {
                        ShowTips("当前工站：" + station + "未打开！检查厂测系统设置！");
                    }

                }
                else
                {
                    ShowTips("当前设备" + jArray["resultMsg"].ToString());
                }
            }
            catch (Exception ex)
            {
                ShowTips("检查工站异常:" + ex.Message);
            }
            return res;
        }
    }
}
