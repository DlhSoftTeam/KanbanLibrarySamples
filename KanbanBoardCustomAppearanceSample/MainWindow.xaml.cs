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

namespace KanbanBoardCustomAppearanceSample
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

            var resource1 = new KanbanResource { Content = "Resource 1", ImageSource = new BitmapImage(new Uri("pack://application:,,,/KanbanBoardCustomAppearanceSample;component/Images/Resource1.png", UriKind.RelativeOrAbsolute)) };
            var resource2 = new KanbanResource { Content = "Resource 2", ImageSource = new BitmapImage(new Uri("pack://application:,,,/KanbanBoardCustomAppearanceSample;component/Images/Resource2.png", UriKind.RelativeOrAbsolute)) };
            var resources = new ObservableCollection<KanbanResource> { resource1, resource2 };

            var group1 = new KanbanGroup { Content = "Story 1", State = state2, AssignedResource = resource1 };
            var group2 = new KanbanGroup { Content = "Story 2", State = state3, AssignedResource = resource2 };
            var groups = new ObservableCollection<KanbanGroup> { group1, group2 };

            // Customize standard item types.
            KanbanBoard.TaskItemType.Handle = Brushes.LightBlue;
            KanbanBoard.BugItemType.Background = Brushes.LightYellow;
            KanbanBoard.BugItemType.Handle = Brushes.Orange;

            // Also, define a custom item type to be used by some items.
            var customType = new KanbanItemType { Background = Brushes.LightGreen, Handle = Brushes.Green };

            var items = new ObservableCollection<KanbanItem>
            {
                new KanbanItem { Content = "Task 1", Group = group1, State = state1, AssignedResource = resource1 },
                new KanbanItem { Content = "Task 2", Group = group1, State = state2, AssignedResource = resource1, ItemType = customType },
                new KanbanItem { Content = "Bug 1", Group = group1, State = state2, AssignedResource = resource1, ItemType = KanbanBoard.BugItemType },
                new KanbanItem { Content = "Task 3", Group = group1, State = state1, AssignedResource = resource2, ItemType = customType },
                new KanbanItem { Content = "Task 4", Group = group1, State = state1, AssignedResource = resource1, ItemType = customType },
                new KanbanItem { Content = "Task 5", Group = group2, State = state1, AssignedResource = resource2 },
                new KanbanItem { Content = "Task 6", Group = group2, State = state2, AssignedResource = resource2 },
                new KanbanItem { Content = "Task 7", Group = group2, State = state2, AssignedResource = resource1 },
                new KanbanItem { Content = "Task 8", Group = group2, State = state3, AssignedResource = resource2 },
            };

            KanbanBoard.States = states;
            KanbanBoard.Groups = groups;
            KanbanBoard.Items = items;
            KanbanBoard.AssignableResources = resources;
        }
    }
}
