using KanbanLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace KanbanBoardSample
{
    public class KanbanCategoryToBoolConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<KanbanCategory> categories = (ObservableCollection<KanbanCategory>)values[0];
            KanbanCategory category = (KanbanCategory)values[1];

            return categories.Contains(category);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
