using KanbanLibrary;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;
using System.Windows.Controls;

namespace KanbanBoardCustomColumnsSample.Converters
{
    public class IsEnabledKanbanStatesEditorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            KanbanState kanbanState = (KanbanState)values[0];
            DataGrid KanbanStatesDataGrid = (DataGrid)values[1];
            var mainWindow = Application.Current.MainWindow as MainWindow;
          
            return KanbanStatesDataGrid.SelectedItems.Count > 0 && kanbanState != mainWindow.NewState && kanbanState != mainWindow.InProgressState && kanbanState != mainWindow.DoneState;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
