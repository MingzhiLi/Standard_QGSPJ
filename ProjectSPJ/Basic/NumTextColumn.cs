using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProjectSPJ
{
    /// <summary>
    /// 限制只能输入数字的datagrid列类,但是有问题，弃
    /// </summary>
    public class NumTextColumn:DataGridTextColumn
    {
        protected override object PrepareCellForEdit(FrameworkElement editingElement, RoutedEventArgs editingEventArgs)
        {
            var edit = editingElement as TextBox;
            edit.PreviewTextInput += Edit_PreviewTextInput;
            DataObject.AddPastingHandler(edit, OnPaste);
            return base.PrepareCellForEdit(editingElement, editingEventArgs);
        }

        private void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            var data = e.SourceDataObject.GetData(DataFormats.Text);
            if (!IsDataValid(data)) e.CancelCommand();
        }

        private void Edit_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsDataValid(e.Text);
        }

        bool IsDataValid(object data)
        {
            try
            {
                Convert.ToInt32(data);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
