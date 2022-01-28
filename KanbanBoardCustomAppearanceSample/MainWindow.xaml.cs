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

            var resource1 = new KanbanResource { Content = "Clarissa Candelaria", ImageSource = new BitmapImage(new Uri("pack://application:,,,/KanbanBoardCustomAppearanceSample;component/Images/Clarissa.png", UriKind.RelativeOrAbsolute)) };
            var resource2 = new KanbanResource { Content = "Tyson Lamberson", ImageSource = new BitmapImage(new Uri("pack://application:,,,/KanbanBoardCustomAppearanceSample;component/Images/Tyson.png", UriKind.RelativeOrAbsolute)) };
            var resources = new ObservableCollection<KanbanResource> { resource1, resource2 };

            var group1 = new KanbanGroup { Content = "Development", State = state2, Resources = new ObservableCollection<KanbanResource> { resource1 } };
            var group2 = new KanbanGroup { Content = "Marketing", State = state3, Resources = new ObservableCollection<KanbanResource> { resource2 } };
            var groups = new ObservableCollection<KanbanGroup> { group1, group2 };

            // Customize standard item types.
            KanbanBoard.TaskItemType.HandleBrush = Brushes.LightBlue;
            KanbanBoard.BugItemType.Background = new SolidColorBrush(Color.FromRgb(189, 217, 252));
            KanbanBoard.BugItemType.HandleBrush = new SolidColorBrush(Color.FromRgb(111, 168, 220));

            // Also, define a custom item type to be used by some items.
            var customType = new KanbanItemType { Background = Brushes.LightGreen, HandleBrush = Brushes.Green };

            var items = new ObservableCollection<KanbanItem>
            {
                new KanbanItem { Content = "Architecture", Group = group1, State = state1, Resources = new ObservableCollection<KanbanResource> { resource1, resource2 } },
                new KanbanItem { Content = "Date-times", Group = group1, State = state2, Resources = new ObservableCollection<KanbanResource> { resource1 } },
                new KanbanItem { Content = "Weekend issue", Group = group1, State = state2, Resources = new ObservableCollection<KanbanResource> { resource1 }, ItemType = KanbanBoard.BugItemType },
                new KanbanItem { Content = "Diagram", Group = group1, State = state1, Resources = new ObservableCollection<KanbanResource> { resource2 } },
                new KanbanItem { Content = "Drag and drop", Group = group1, State = state1, Resources = new ObservableCollection<KanbanResource> { resource1 } },
                new KanbanItem { Content = "Colors", Group = group2, State = state1, Resources = new ObservableCollection<KanbanResource> { resource2 } },
                new KanbanItem { Content = "Logo", Group = group2, State = state2, Resources = new ObservableCollection<KanbanResource> { resource2 } },
                new KanbanItem { Content = "Web site", Group = group2, State = state2, Resources = new ObservableCollection<KanbanResource> { resource1 } },
                new KanbanItem { Content = "Social networking", Group = group2, State = state3, Resources = new ObservableCollection<KanbanResource> { resource2 } },
            };

            KanbanBoard.States = states;
            KanbanBoard.Groups = groups;
            KanbanBoard.Items = items;
            KanbanBoard.AvailableResources = resources;
        }
    }
}
