using KanbanLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KanbanBoardSample
{
    /// <summary>
    /// Interaction logic for EditItemDialog.xaml
    /// </summary>
    public partial class EditItemDialog : Window
    {
        public EditItemDialog()
        {
            InitializeComponent();
        }

        public KanbanItem Item { get; set; } 
        public KanbanBoard KanbanBoard { get; set; }

        private void ItemGroupComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var group = ItemGroupComboBox.SelectedItem as KanbanGroup;
            KanbanBoard.SetItemGroup(Item, group);
        }

        private void ItemStateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var state = ItemStateComboBox.SelectedItem as KanbanState;
            KanbanBoard.SetItemState(Item, state);
        }

        private void ItemResourceCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            KanbanResource resource = (KanbanResource)((CheckBox)sender).DataContext;
            if (!Item.Resources.Contains(resource))
                Item.Resources.Add(resource);
        }

        private void ItemResourceCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            KanbanResource resource = (KanbanResource)((CheckBox)sender).DataContext;
            if (Item.Resources.Contains(resource))
                Item.Resources.Remove(resource);
        }

        private void ItemCategoryCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            KanbanCategory category = (KanbanCategory)((CheckBox)sender).DataContext;
            if (!Item.Categories.Contains(category))
                Item.Categories.Add(category);
        }

        private void ItemCategoryCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            KanbanCategory category = (KanbanCategory)((CheckBox)sender).DataContext;
            if (Item.Categories.Contains(category))
                Item.Categories.Remove(category);
        }
    }
}
