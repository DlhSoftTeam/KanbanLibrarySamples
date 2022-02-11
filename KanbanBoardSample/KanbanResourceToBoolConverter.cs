using KanbanLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace KanbanBoardSample
{
    public class KanbanResourceToBoolConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<KanbanResource> resources = (ObservableCollection<KanbanResource>)values[0];
            KanbanResource resource = (KanbanResource)values[1];

            return resources.Contains(resource);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
