using BasicClass;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Main
{
    /// <summary>
    /// OperationWnd.xaml 的交互逻辑
    /// </summary>
    public partial class OperationWnd : Window
    {
        public OperationWnd()
        {
            InitializeComponent();
            spRowCol.DataContext = JustForBinding.Inst;
        }

        private void Label_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (tab.SelectedIndex == 1)
                ucGridMark.BtnConfirm_Click(sender, e);
            else
                ucInputMark.BtnConfirm_Click(sender, e);
            this.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

    public class JustForBinding
    {
        public static JustForBinding Inst = new JustForBinding();
        public int Col
        {
            get
            {
                return ModelParams.PickArrayCols;
            }
            set
            {
                ModelParams.PickArrayCols = value;
            }
        }
        public int Row
        {
            get
            {
                return ModelParams.PickArrayRows;
            }
            set
            {
                ModelParams.PickArrayRows = value;
            }
        }
    }
}
