using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjectSPJ
{
    /// <summary>
    /// WinPassword.xaml 的交互逻辑
    /// </summary>
    public partial class WinPassword : Window
    {
        PasswordSet passwordSet1 = PasswordSet.SuperAdmin;
        public WinPassword()
        {
            InitializeComponent();
            lbInfo.Content = passwordSet1.LabelContent;
        }
        public WinPassword(PasswordSet passwordSet)
        {
            InitializeComponent();
            lbInfo.Content = passwordSet.LabelContent;
            passwordSet1 = passwordSet;
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                DialogResult = PWB.Password == passwordSet1.Password; 
                if ((bool)DialogResult)
                {
                    this.Close();
                    return;
                }
            }
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = PWB.Password == passwordSet1.Password;
            if ((bool)DialogResult)
            {
                this.Close();
                return;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PWB.Focus();
        }
    }

    /// <summary>
    /// 权限密码设置类
    /// </summary>
    public sealed class PasswordSet
    {
        public static PasswordSet SuperAdmin = new PasswordSet() { LabelContent = "请输入超级管理员密码:", Password = "spadmin" };
        public static PasswordSet Admin = new PasswordSet() { LabelContent = "请输入厂商密码:", Password = "stadmin" };
        public string LabelContent { get; set; } = "请输入厂商密码：";
        public string Password { get; set; } = "stadmin";
    }
}
