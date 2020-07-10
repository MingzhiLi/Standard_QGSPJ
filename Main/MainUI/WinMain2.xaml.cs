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
using ControlLib;
using Common;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Camera;
using HalconDotNet;
using System.Drawing;
using DealRobot;
using DealPLC;
using MahApps.Metro.Controls.Dialogs;
using System.IO;
using DealComprehensive;
using SetPar;
using SetComprehensive;
using DealConfigFile;
using BasicClass;
using DealComInterface;
using ParComprehensive;
using DealDisplay;
using BasicDisplay;
using DealTool;
using DealLog;
using DealMedia;
using DealHelp;
using System.Reflection;
using BasicComprehensive;
using DealAlgorithm;
using DealCalibrate;
using DealCommunication;
using DealDraw;
using DealGeometry;
using DealGrabImage;
using DealImageProcess;
using DealInOutput;
using DealMath;
using DealResult;
using DealWorkFlow;
using DealFile;
using Main_EX;
using NLog;
using ProjectSPJ;

namespace Main
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WinMain2 : MainWindow
    {
        #region 定义


        #endregion 定义


        #region 其他
        /// <summary>
        /// CIM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmiCim_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
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


        #region 右侧边栏

        #region 运行状态

        private void dpState_MouseEnter(object sender, MouseEventArgs e)
        {
            dpState.Background = new SolidColorBrush(Colors.LightBlue);
        }

        private void dpState_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ppState.IsOpen = !ppState.IsOpen;
            ppState.StaysOpen = ppState.IsOpen;
        }

        private void dpState_MouseLeave(object sender, MouseEventArgs e)
        {
            dpState.Background = new SolidColorBrush(Colors.White);
        }
        #endregion 运行状态

        #region 运行报警
        private void dpAlarm_MouseEnter(object sender, MouseEventArgs e)
        {
            dpAlarm.Background = new SolidColorBrush(Colors.LightBlue);
        }

        private void dpAlarm_MouseLeave(object sender, MouseEventArgs e)
        {
            dpAlarm.Background = new SolidColorBrush(Colors.White);
        }

        private void dpAlarm_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ppAlarm.IsOpen = !ppAlarm.IsOpen;
            ppAlarm.StaysOpen = ppAlarm.IsOpen;
        }

        #endregion 运行报警

        #region 操作设置

        private void dpProductSet_MouseEnter(object sender, MouseEventArgs e)
        {
            dpProductSet.Background = new SolidColorBrush(Colors.LightBlue);
        }

        private void dpProductSet_MouseLeave(object sender, MouseEventArgs e)
        {
            dpProductSet.Background = new SolidColorBrush(Colors.White);
        }

        private void dpProductSet_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ProjectSPJ.WinProductPar.Inst.Show();
        }

        #endregion 操作设置

        #region 运行数据
        private void dpData_MouseEnter(object sender, MouseEventArgs e)
        {
            dpData.Background = new SolidColorBrush(Colors.LightBlue);
        }

        private void dpData_MouseLeave(object sender, MouseEventArgs e)
        {
            dpData.Background = new SolidColorBrush(Colors.White);
        }

        private void dpData_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WinResultStatistics.Inst.Show();
        }
        #endregion 运行数据

        #region 系统设置
        private void dpSystemConfig_MouseEnter(object sender, MouseEventArgs e)
        {
            dpSystemConfig.Background = new SolidColorBrush(Colors.LightBlue);
        }

        private void dpSystemConfig_MouseLeave(object sender, MouseEventArgs e)
        {
            dpSystemConfig.Background = new SolidColorBrush(Colors.White);
        }

        private void dpSystemConfig_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(!EngineerReturn())
            {
                return;
            }
            ProjectSPJ.WinSysSet.Inst.Show();
        }

        #endregion 系统设置

        #region 相机综合设置
        private void dpCamComprehensive_MouseEnter(object sender, MouseEventArgs e)
        {
            dpCamComprehensive.Background = new SolidColorBrush(Colors.LightBlue);
        }

        private void dpCamComprehensive_MouseLeave(object sender, MouseEventArgs e)
        {
            dpCamComprehensive.Background = new SolidColorBrush(Colors.White);
        }

        private void dpCamComprehensive_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OpenWinWinComprehensive(1);
        }

        #endregion 相机综合设置

        #region  屏幕键盘
        private void dpKeyboard_MouseEnter(object sender, MouseEventArgs e)
        {
            dpKeyboard.Background = new SolidColorBrush(Colors.LightBlue);
        }

        private void dpKeyboard_MouseLeave(object sender, MouseEventArgs e)
        {
            dpKeyboard.Background = new SolidColorBrush(Colors.White);
        }

        private void dpKeyboard_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SysTool.S_I.OpenKeyboard();
        }
        #endregion 屏幕键盘

        #endregion 右侧边栏

        #region 菜单栏

        #region 产品参数
        private void dpProductPar_MouseEnter(object sender, MouseEventArgs e)
        {
            dpProductPar.Background = new SolidColorBrush(Colors.LightBlue);
        }

        private void dpProductPar_MouseLeave(object sender, MouseEventArgs e)
        {
            dpProductPar.Background = new SolidColorBrush(Colors.White);
        }

        private void dpProductPar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            bool blNew = false;
            WinConfigPar winConfigPar = WinConfigPar.GetWinInst(out blNew);
            //注册事件
            if (blNew)
            {
                LoginEvent_ConfigPar(winConfigPar);
            }
            //显示窗体
            winConfigPar.Show();
        }
        #endregion 产品参数

        #region 型号管理
        private void dpModelManager_MouseEnter(object sender, MouseEventArgs e)
        {
            dpModelManager.Background = new SolidColorBrush(Colors.LightBlue);
        }

        private void dpModelManager_MouseLeave(object sender, MouseEventArgs e)
        {
            dpModelManager.Background = new SolidColorBrush(Colors.White);
        }

        private void dpModelManager_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //权限返回
            if (!WorkerReturn())
            {
                return;
            }
            WinManageConfigPar winManageConfigPar = new WinManageConfigPar();
            winManageConfigPar.DeleteModel_event += new FdBlStrAction_del(DelModel);//删除文件响应事件
            winManageConfigPar.NewModel_event += new FdBlAction_del(NewModel);//新建型号
            winManageConfigPar.ChangeModel_event += new Action(ChangeModel_event);//换型
            winManageConfigPar.ShowDialog();
        }
        #endregion 型号管理

        #region 手动触发

        private void dpManualTrigger_MouseEnter(object sender, MouseEventArgs e)
        {
            dpManualTrigger.Background = new SolidColorBrush(Colors.LightBlue);
        }

        private void dpManualTrigger_MouseLeave(object sender, MouseEventArgs e)
        {
            dpManualTrigger.Background = new SolidColorBrush(Colors.White);
        }

        private void dpManualTrigger_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //权限返回
            if (!WorkerReturn())
            {
                return;
            }

            bool blNew = false;
            WinManualTrigger.WinInst.Show();

        }
        #endregion 手动触发

        #endregion 菜单栏

        #region 按键绑定
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
        #endregion 按键绑定


        private void btnManulPLC_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
