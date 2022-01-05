using KanbanLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

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

            var state1 = new KanbanState { Content = "New" };
            var state2 = new KanbanState { Content = "In progress", AreNewItemButtonsHidden = true };
            var state3 = new KanbanState { Content = "Done", IsCollapsedByDefaultForGroups = true, AreNewItemButtonsHidden = true };
            var states = new ObservableCollection<KanbanState> { state1, state2, state3 };

            var resource1 = new KanbanResource { Content = "Resource 1", ImageSource = new BitmapImage(new Uri("pack://application:,,,/KanbanBoardSample;component/Images/Resource1.png", UriKind.RelativeOrAbsolute)) };
            var resource2 = new KanbanResource { Content = "Resource 2", ImageSource = new BitmapImage(new Uri("pack://application:,,,/KanbanBoardSample;component/Images/Resource2.png", UriKind.RelativeOrAbsolute)) };
            var resources = new ObservableCollection<KanbanResource> { resource1, resource2 };

            var group1 = new KanbanGroup { Content = "Story 1", State = state2, AssignedResources = new ObservableCollection<KanbanResource> { resource1 } };
            var group2 = new KanbanGroup { Content = "Story 2", State = state3, AssignedResources = new ObservableCollection<KanbanResource> { resource2 } };
            var groups = new ObservableCollection<KanbanGroup> { group1, group2 };

            var items = new ObservableCollection<KanbanItem>
            {
                new KanbanItem { Content = "Task 1", Group = group1, State = state1, AssignedResources = new ObservableCollection<KanbanResource> { resource1 } },
                new KanbanItem { Content = "Task 2", Group = group1, State = state2, AssignedResources = new ObservableCollection<KanbanResource> { resource1 } },
                new KanbanItem { Content = "Bug 1", Group = group1, State = state2, AssignedResources = new ObservableCollection<KanbanResource> { resource1 }, ItemType = KanbanBoard.BugItemType },
                new KanbanItem { Content = "Task 3", Group = group1, State = state1, AssignedResources = new ObservableCollection<KanbanResource> { resource2 } },
                new KanbanItem { Content = "Task 4", Group = group1, State = state1, AssignedResources = new ObservableCollection<KanbanResource> { resource1 } },
                new KanbanItem { Content = "Task 5", Group = group2, State = state1, AssignedResources = new ObservableCollection<KanbanResource> { resource2 } },
                new KanbanItem { Content = "Task 6", Group = group2, State = state2, AssignedResources = new ObservableCollection<KanbanResource> { resource2 } },
                new KanbanItem { Content = "Task 7", Group = group2, State = state2, AssignedResources = new ObservableCollection<KanbanResource> { resource1 } },
                new KanbanItem { Content = "Task 8", Group = group2, State = state3, AssignedResources = new ObservableCollection<KanbanResource> { resource2 } },
            };

            // Uncomment the following code lines to load more groups and items.
            //for (var i = 3; i <= 10; i++)
            //{
            //    var group = new KanbanGroup { Content = "Story " + i, AssignedResource = resource1 };
            //    groups.Add(group);
            //    for (var j = 1; j <= 10; j++)
            //    {
            //        var item = new KanbanItem { Content = "Item " + i + "." + j, Group = group, State = j % 2 == 0 ? state1 : state2, AssignedResource = resource1 };
            //        items.Add(item);
            //    }
            //}

            KanbanBoard.States = states;
            KanbanBoard.Groups = groups;
            KanbanBoard.Items = items;
            KanbanBoard.AssignableResources = resources;

            KanbanBoard.NewItemAdded += (sender, e) =>
            {
                e.Item.AssignedResources = new ObservableCollection<KanbanResource> { resource1 };
                Console.WriteLine("A new item was created.");
            };

            KanbanBoard.UsePopupMenus = false;
            KanbanBoard.EditItemButtonContent = "×";
            KanbanBoard.EditItemButtonToolTipContent = "Delete item";
            KanbanBoard.EditingItem += (sender, e) =>
            {
                var item = e.Item;
                if (MessageBox.Show("Are you sure you want to delete item " + item.Content + "?", "Kanban", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    items.Remove(item);
                    Console.WriteLine("Item " + item.Content + " was deleted.");
                }
            };
            KanbanBoard.EditingGroup += (sender, e) =>
            {
                MessageBox.Show("Editing group " + e.Group.Content + "...");
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
