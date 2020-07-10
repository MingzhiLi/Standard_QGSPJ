using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BasicClass;
using System.Xml;

namespace DealPLC
{
    /// <summary>
    /// UCSetPLC.xaml 的交互逻辑
    /// </summary>
    public partial class UCSetPLC : BaseUCPLC
    {
        public UCSetPLC()
        {
            InitializeComponent();
        }

        private void BaseUCPLC_Loaded(object sender, RoutedEventArgs e)
        {
            InitTV();//初始化Treeview
            ShowPar();
        }

        private const string NameClass = "UCSetPLC";

        #region 定义
        #region Path
        string PathXml = "SetPLC.UI.XmlTemplate.XmlSetPLC.xml";
        #endregion Path

        BaseUCPLC g_BaseUCPLC = null;
        #endregion 定义

        #region 初始化

        void InitTV()
        {
            try
            {
                List<PLCPar> PLCPar_L = new List<PLCPar>();

                XmlDocument xDoc = LoadXmlStream(PathXml);
                XmlElement xmlRoot = xDoc.DocumentElement;
                XmlNodeList xmlModes = ReadNodes(xmlRoot);

                foreach (XmlElement item in xmlModes)
                {
                    PLCPar basic = new PLCPar();
                    basic.Name = item.GetAttribute("Name");
                    basic.DispName_EN = item.GetAttribute("Name_EN");

                    PLCPar_L.Add(basic);
                }

                tvSetPLC.ItemsSource = PLCPar_L;
                tvSetPLC.Items.Refresh();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 初始化

        #region 选择显示
        private void tvSetPLC_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                //移除前一个UI
                for (int i = this.gdLayout.Children.Count; i > 1; i--)
                {
                    this.gdLayout.Children.RemoveAt(i - 1);
                }
                string name = ((PLCPar)this.tvSetPLC.SelectedItem).Name.ToString();

                //选择功能
                CreateFunShow(name);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        //选择功能
        void CreateFunShow(string name)
        {
            try
            {
                int intHeight = 0;
                int intWidth = 0;
                switch (name)
                {
                    case "PLC基本参数":
                        g_BaseUCPLC = new UCSetTypePLC();
                        intHeight = 520;
                        intWidth = 600;
                        break;

                    case "PLC寄存器":
                        g_BaseUCPLC = new UCSetRegConfigPar();
                        break;

                    case "配置参数寄存器":
                        g_BaseUCPLC = new UCSetRegConfigPar();
                        break;

                    case "监控寄存器":
                        g_BaseUCPLC = new UCSetRegMonitor();
                        ((UCSetRegMonitor)g_BaseUCPLC).SaveReg_event += new Action(WinSetPLC_SaveReg_event);
                        break;

                    case "相机寄存器":
                        g_BaseUCPLC = new UCSetRegCameraData();
                        ((UCSetRegCameraData)g_BaseUCPLC).SaveReg_event += new Action(WinSetPLC_SaveReg_event);
                        break;

                    case "数据寄存器1":
                        g_BaseUCPLC = new UCSetRegData();
                        break;

                    case "数据寄存器2":
                        g_BaseUCPLC = new UCSetRegData2();
                        break;

                    case "数据寄存器3":
                        g_BaseUCPLC = new UCSetRegData3();
                        break;

                    case "数据寄存器4":
                        g_BaseUCPLC = new UCSetRegData4();
                        break;

                    case "数据寄存器5":
                        g_BaseUCPLC = new UCSetRegData5();
                        break;

                    case "数据寄存器6":
                        g_BaseUCPLC = new UCSetRegData6();
                        break;

                    case "循环读取寄存器":
                        g_BaseUCPLC = new UCSetRegCycle();
                        intHeight = 325;
                        intWidth = 485;
                        break;
                }
                //添加控件显示
                AddChildCtr(intHeight, intWidth);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }



        //添加控件
        void AddChildCtr(int intHeight, int intWidth)
        {
            try
            {
                //移除前一个UI
                if (this.gdLayout.Children.Count > 2)
                {
                    this.gdLayout.Children.RemoveAt(2);
                }
                if (g_BaseUCPLC == null)
                {
                    return;
                }
                //设定布局方式
                if (intHeight != 0)
                {
                    g_BaseUCPLC.HorizontalAlignment = HorizontalAlignment.Stretch;
                    g_BaseUCPLC.VerticalAlignment = VerticalAlignment.Stretch;
                    //g_BaseUCPLC.Height = intHeight;
                    //g_BaseUCPLC.Width = intWidth;
                }
                else
                {
                    g_BaseUCPLC.HorizontalAlignment = HorizontalAlignment.Stretch;
                    g_BaseUCPLC.VerticalAlignment = VerticalAlignment.Stretch;
                }
                g_BaseUCPLC.Margin = new Thickness(2, 2, 2, 2);
                Grid.SetColumn(g_BaseUCPLC, 1);
                Grid.SetRowSpan(g_BaseUCPLC, 2);

                //初始化加载              
                g_BaseUCPLC.Init();

                //添加控件
                this.gdLayout.Children.Add(g_BaseUCPLC);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 选择显示

        #region 监控寄存器和相机寄存器保存的时候，触发修改循环读取寄存器
        void WinSetPLC_SaveReg_event()
        {
            try
            {
                UCSetRegCycle inst = new UCSetRegCycle();
                if (inst.CreateCycReg())
                {
                    MessageBox.Show("保存此寄存器时，会修改和保存循环读取寄存器，已保存成功！");
                }
                else
                {
                    MessageBox.Show("保存此寄存器时，会修改和保存循环读取寄存器，但保存异常，请检查相机寄存器和监控寄存器设置！");
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 监控寄存器和相机寄存器保存的时候，触发修改循环读取寄存器

        #region 显示
        public void ShowPar()
        {
            CreateFunShow("PLC基本参数");
        }
        #endregion 显示

    }
}
