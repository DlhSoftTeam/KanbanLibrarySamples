using KanbanLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace KanbanBoardSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var state1 = new KanbanState { Content = "New", IsCollapsed = true };
            var state2 = new KanbanState { Content = "In progress", AreNewItemButtonsHidden = true, IsCollapsible = false };
            var state3 = new KanbanState { Content = "Ready to test", AreNewItemButtonsHidden = true };
            var state4 = new KanbanState { Content = "In testing", AreNewItemButtonsHidden = true };
            var state5 = new KanbanState { Content = "Done", IsCollapsedByDefaultForGroups = true, AreNewItemButtonsHidden = true };
            var states = new ObservableCollection<KanbanState> { state1, state2, state3, state4, state5 };
            var groupStates = new ObservableCollection<KanbanState> { state1, state2, state5 };

            var category1 = new KanbanCategory { Content = "Questions", Background = Brushes.CornflowerBlue, BorderBrush = Brushes.Blue, Foreground = Brushes.White };
            var category2 = new KanbanCategory { Content = "Important", Background = Brushes.LightPink, BorderBrush = Brushes.DarkRed };
            var categories = new ObservableCollection<KanbanCategory> { category1, category2 };

            var resource1 = new KanbanResource { Content = "Clarissa Candelaria", ImageSource = new BitmapImage(new Uri("pack://application:,,,/KanbanBoardSample;component/Images/Clarissa.png", UriKind.RelativeOrAbsolute)) };
            var resource2 = new KanbanResource { Content = "Tyson Lamberson", ImageSource = new BitmapImage(new Uri("pack://application:,,,/KanbanBoardSample;component/Images/Tyson.png", UriKind.RelativeOrAbsolute)) };
            var resource3 = new KanbanResource { Content = "Joanna Mcamis", ImageSource = new BitmapImage(new Uri("pack://application:,,,/KanbanBoardSample;component/Images/Joanna.png", UriKind.RelativeOrAbsolute)) };
            var resource4 = new KanbanResource { Content = "Jed Markovitz", ImageSource = new BitmapImage(new Uri("pack://application:,,,/KanbanBoardSample;component/Images/Jed.png", UriKind.RelativeOrAbsolute)) };
            var resources = new ObservableCollection<KanbanResource> { resource1, resource2, resource3, resource4 };

            var group1 = new KanbanGroup { Content = "Development", State = state2, Resources = new ObservableCollection<KanbanResource> { resource1, resource2 } };
            var group2 = new KanbanGroup { Content = "Marketing", State = state2, Resources = new ObservableCollection<KanbanResource> { resource3 }, ImageSource = new BitmapImage(new Uri("pack://application:,,,/KanbanBoardSample;component/Images/Marketing.png", UriKind.RelativeOrAbsolute)) };
            var groups = new ObservableCollection<KanbanGroup> { group1, group2 };
            
            var items = new ObservableCollection<KanbanItem>
            {
                new KanbanItem { Content = "Architecture", Group = group1, State = state5, Resources = new ObservableCollection<KanbanResource> { resource2 }, ImageSource = new BitmapImage(new Uri("pack://application:,,,/KanbanBoardSample;component/Images/Architecture.png", UriKind.RelativeOrAbsolute)), Categories = new ObservableCollection<KanbanCategory> { category2 } },
                new KanbanItem { Content = "Date-times", Group = group1, State = state4, Resources = new ObservableCollection<KanbanResource> { resource3 }, ImageSource = new BitmapImage(new Uri("pack://application:,,,/KanbanBoardSample;component/Images/DateTimes.png", UriKind.RelativeOrAbsolute)) },
                new KanbanItem { Content = "Schedules", Group = group1, State = state2, Resources = new ObservableCollection<KanbanResource> { resource3, resource4 }, Categories = new ObservableCollection<KanbanCategory> { category1, category2 } },
                new KanbanItem { Content = "Weekend issue", Group = group1, State = state2, Resources = new ObservableCollection<KanbanResource> { resource4 }, ItemType = KanbanBoard.BugItemType, ImageSource = new BitmapImage(new Uri("pack://application:,,,/KanbanBoardSample;component/Images/Bug.png", UriKind.RelativeOrAbsolute)) },
                new KanbanItem { Content = "Diagram", Group = group1, State = state2, Resources = new ObservableCollection<KanbanResource> { resource1 }, Categories = new ObservableCollection<KanbanCategory> { category2 } },
                new KanbanItem { Content = "Bars", Group = group1, State = state1, Resources = new ObservableCollection<KanbanResource> { resource1 }, Categories = new ObservableCollection<KanbanCategory> { category2 } },
                new KanbanItem { Content = "Nonworking time highlighting", Group = group1, State = state3, Resources = new ObservableCollection<KanbanResource> { resource4 } },
                new KanbanItem { Content = "Background", Group = group1, State = state3 },
                new KanbanItem { Content = "Milestone bug", Group = group1, State = state3, Resources = new ObservableCollection<KanbanResource> { resource1 }, ItemType = KanbanBoard.BugItemType },
                new KanbanItem { Content = "Drag and drop", Group = group1, State = state3, Resources = new ObservableCollection<KanbanResource> { resource2 }, Categories = new ObservableCollection<KanbanCategory> { category1 } },
                new KanbanItem { Content = "Preparations", Group = group2, State = state5, Resources = new ObservableCollection<KanbanResource> { resource1 } },
                new KanbanItem { Content = "Colors", Group = group2, State = state5, Resources = new ObservableCollection<KanbanResource> { resource3 } },
                new KanbanItem { Content = "Logo", Group = group2, State = state2, Resources = new ObservableCollection<KanbanResource> { resource3 } },
                new KanbanItem { Content = "Sample apps", Group = group2, State = state3, Resources = new ObservableCollection<KanbanResource> { resource4 }, Categories = new ObservableCollection<KanbanCategory> { category2 } },
                new KanbanItem { Content = "Screenshots", Group = group2, State = state2, Resources = new ObservableCollection<KanbanResource> { resource4 } },
                new KanbanItem { Content = "Web site", Group = group2, State = state1, Resources = new ObservableCollection<KanbanResource> { resource2, resource4 }, ImageSource = new BitmapImage(new Uri("pack://application:,,,/KanbanBoardSample;component/Images/WebSite.png", UriKind.RelativeOrAbsolute)) },
                new KanbanItem { Content = "Social networking", Group = group2, State = state1, Resources = new ObservableCollection<KanbanResource> { resource1, resource3 } },
            };

            // Uncomment the following code lines to load more groups and items.
            // for (var i = 1; i <= 10; i++)
            // {
            //     var group = new KanbanGroup { Content = "Story " + i, Resource = resource1 };
            //     groups.Add(group);
            //     for (var j = 1; j <= 10; j++)
            //     {
            //         var item = new KanbanItem { Content = "Item " + i + "." + j, Group = group, State = j % 2 == 0 ? state1 : state2, Resource = resource1 };
            //         items.Add(item);
            //     }
            // }

            KanbanBoard.States = states;
            KanbanBoard.Groups = groups;
            KanbanBoard.Items = items;
            KanbanBoard.AvailableCategories = categories;
            KanbanBoard.AvailableResources = resources;
            KanbanBoard.GroupStates = groupStates;

            KanbanBoard.AreStateExpandersVisible = true;

            KanbanBoard.NewItemAdded += (sender, e) =>
            {
                e.Item.Resources = new ObservableCollection<KanbanResource> { resource1 };
                Console.WriteLine("A new item was created.");
            };

            // Optionally, define a custom "edit" button and behavior:
            // KanbanBoard.UsePopupMenus = false;
            // KanbanBoard.EditItemButtonContent = "×";
            // KanbanBoard.EditItemButtonToolTipContent = "Delete item";
            // KanbanBoard.EditingItem += (sender, e) =>
            // {
            //     var item = e.Item;
            //     if (MessageBox.Show("Are you sure you want to delete item " + item.Content + "?", "Kanban", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            //     {
            //         items.Remove(item);
            //         Console.WriteLine("Item " + item.Content + " was deleted.");
            //     }
            // };
            // KanbanBoard.EditingGroup += (sender, e) =>
            // {
            //     MessageBox.Show("Editing group " + e.Group.Content + "...");
            // };

            // Or, customize behavior for Edit item menu command (when UsePopupMenus is true):
            KanbanBoard.EditingItem += (sender, e) =>
            {
                var item = e.Item;
                var editItemDialog = new EditItemDialog { Owner = Application.Current.MainWindow, Item = item, KanbanBoard = KanbanBoard };
                editItemDialog.Show();
            };
            KanbanBoard.AreEditGroupMenuItemsHidden = true;

            KanbanBoard.DeletingItem += (sender, e) =>
            {
                var item = e.Item;
                if (MessageBox.Show("Are you sure you want to delete item " + item.Content + "?", "Kanban", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes)
                    e.Cancel = true;
            };
            KanbanBoard.DeletingGroup += (sender, e) =>
            {
                var group = e.Group;
                if (MessageBox.Show("Are you sure you want to delete group " + group.Content + "?", "Kanban", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes)
                    e.Cancel = true;
            };

            KanbanBoard.ItemStateChanged += (sender, e) =>
            {
                Console.WriteLine("State of " + e.Item.Content + " was changed to " + e.State.Content + ".");
            };
            KanbanBoard.ItemGroupChanged += (sender, e) =>
            {
                Console.WriteLine("Group of " + e.Item.Content + " was changed to " + e.Group.Content + ".");
            };
        }
    }
}
