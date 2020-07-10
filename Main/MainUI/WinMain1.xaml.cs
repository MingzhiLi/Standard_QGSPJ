using BasicClass;
using DealCIM;
using System;
using System.Windows;
using DealPLC;
using DealRobot;
using ModulePackage;
using ProjectSPJ;
using DealConfigFile;
using System.Windows.Documents;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data;
using System.Text;
using System.IO;

namespace Main
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WinMain1 : MainWindow
    {
        #region 定义        

        #endregion 定义

        #region 其他

        #endregion 其他

        #region CIM模拟

        #endregion CIM模拟

        #region 操作设置
        private void epSetWork_Collapsed(object sender, RoutedEventArgs e)
        {

        }

        private void epSetWork_Expanded(object sender, RoutedEventArgs e)
        {

        }
        #endregion 操作设置        

        private void InsertTempComR_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                //ModelParams.InsertTempComR = Math.Round((double)InsertTempComR.Value, 2);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }




        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        public void RefreshData()
        {
            //this.Dispatcher.BeginInvoke(new Action(() =>
            //{
            //    lblProSUM.Content = string.Format("精定位产量：{0}", RegeditMain.R_I.PreciseSUM);
            //    lblPreciseNG.Content = string.Format("精定位NG：{0}", RegeditMain.R_I.PreciseNG);
            //    lblWastageNG1.Content = string.Format("残材1NG：{0}", RegeditMain.R_I.WastageNG1);
            //    lblWastageNG2.Content = string.Format("残材2NG：{0}", RegeditMain.R_I.WastageNG2);
            //}));
        }

        private void MHardwareConfig_Click(object sender, RoutedEventArgs e)
        {
            if (!EngineerReturn())
            {
                return;
            }

            WndHardwareConfig wnd = WndHardwareConfig.GetInstance();
            if (!wnd.IsVisible)
                wnd.Show();
        }

        private void BtnProductivity_Click(object sender, RoutedEventArgs e)
        {
            ProductivityReport wndProductivityReport = new ProductivityReport();
            wndProductivityReport.Show();
        }


        private void BtnSetPar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnClick(object sender, RoutedEventArgs e)
        {
            WinCalibSet.GetWinInst.Show();
        }

        private void BtnResult_Click(object sender, RoutedEventArgs e)
        {
            WinCalibResult.GetWinInst.Show();
        }

        private void BtnRestart_Click(object sender, RoutedEventArgs e)
        {
            ShowState("手动重启机器人");
            RobotReStart();
        }

        private void BtnSPJ_Click(object sender, RoutedEventArgs e)
        {
            WinPassword win = new WinPassword(PasswordSet.Admin);

            if ((int)Authority.Authority_e > 2 || (bool)win.ShowDialog())
            {
                ProjectSPJ.WinSysSet.Inst.Show();
            }
            else
            {
                MessageBox.Show("密码错误！");
            }
        }

        private void BtnPickSet_Click(object sender, RoutedEventArgs e)
        {
            OperationWnd wnd = new OperationWnd();
            wnd.Show();
        }


        private void CommandBinding_AdvancedSet_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_AdvancedSet_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            WinPassword win = new WinPassword();
            if ((int)Authority.Authority_e > 3 || (bool)win.ShowDialog())
            {
                WinAdvancedSet.GetWinInst.Show();
            }
            else
            {
                MessageBox.Show("密码错误！");
            }
        }

        private void btnProdSet_Click(object sender, RoutedEventArgs e)
        {
            ProjectSPJ.WinProductPar.Inst.Show();
        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            //List<string> data = OpenCSV("D:\\PC记录.csv");
        }

        private void BtnResultSta_Click(object sender, RoutedEventArgs e)
        {
            WinResultStatistics.Inst.Show();
        }

        public static List<string> OpenCSV(string filePath)
        {
            List<string> data = new List<string>();
            Encoding encoding = GetType(filePath); //Encoding.ASCII;//
            FileStream fs = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);

            StreamReader sr = new StreamReader(fs, encoding);
            //记录每次读取的一行记录
            string strLine = "";
            //记录每行记录中的各字段内容
            string[] aryLine = null;
            //逐行读取CSV中的数据
            while ((strLine = sr.ReadLine()) != null)
            {
                aryLine = strLine.Split(',');
                data.Add(aryLine[1]);
            }

            sr.Close();
            fs.Close();
            return data;
        }


        public static System.Text.Encoding GetType(string FILE_NAME)
        {
            System.IO.FileStream fs = new System.IO.FileStream(FILE_NAME, System.IO.FileMode.Open,
                System.IO.FileAccess.Read);
            System.Text.Encoding r = GetType(fs);
            fs.Close();
            return r;
        }

        public static System.Text.Encoding GetType(System.IO.FileStream fs)
        {
            byte[] Unicode = new byte[] { 0xFF, 0xFE, 0x41 };
            byte[] UnicodeBIG = new byte[] { 0xFE, 0xFF, 0x00 };
            byte[] UTF8 = new byte[] { 0xEF, 0xBB, 0xBF }; //带BOM
            System.Text.Encoding reVal = System.Text.Encoding.Default;

            System.IO.BinaryReader r = new System.IO.BinaryReader(fs, System.Text.Encoding.Default);
            int i;
            int.TryParse(fs.Length.ToString(), out i);
            byte[] ss = r.ReadBytes(i);
            if (IsUTF8Bytes(ss) || (ss[0] == 0xEF && ss[1] == 0xBB && ss[2] == 0xBF))
            {
                reVal = System.Text.Encoding.UTF8;
            }
            else if (ss[0] == 0xFE && ss[1] == 0xFF && ss[2] == 0x00)
            {
                reVal = System.Text.Encoding.BigEndianUnicode;
            }
            else if (ss[0] == 0xFF && ss[1] == 0xFE && ss[2] == 0x41)
            {
                reVal = System.Text.Encoding.Unicode;
            }
            r.Close();
            return reVal;
        }


        private static bool IsUTF8Bytes(byte[] data)
        {
            int charByteCounter = 1;  //计算当前正分析的字符应还有的字节数
            byte curByte; //当前分析的字节.
            for (int i = 0; i < data.Length; i++)
            {
                curByte = data[i];
                if (charByteCounter == 1)
                {
                    if (curByte >= 0x80)
                    {
                        //判断当前
                        while (((curByte <<= 1) & 0x80) != 0)
                        {
                            charByteCounter++;
                        }
                        //标记位首位若为非0 则至少以2个1开始 如:110XXXXX...........1111110X　
                        if (charByteCounter == 1 || charByteCounter > 6)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    //若是UTF-8 此时第一位必须为1
                    if ((curByte & 0xC0) != 0x80)
                    {
                        return false;
                    }
                    charByteCounter--;
                }
            }
            if (charByteCounter > 1)
            {
                throw new Exception("非预期的byte格式");
            }
            return true;
        }
    }
}
