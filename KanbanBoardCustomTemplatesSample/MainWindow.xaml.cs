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

namespace KanbanBoardCustomTemplatesSample
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

            var resource1 = new KanbanResource { Content = "Clarissa Candelaria", ImageSource = new BitmapImage(new Uri("pack://application:,,,/KanbanBoardCustomTemplatesSample;component/Images/Clarissa.png", UriKind.RelativeOrAbsolute)) };
            var resource2 = new KanbanResource { Content = "Tyson Lamberson", ImageSource = new BitmapImage(new Uri("pack://application:,,,/KanbanBoardCustomTemplatesSample;component/Images/Tyson.png", UriKind.RelativeOrAbsolute)) };
            var resources = new ObservableCollection<KanbanResource> { resource1, resource2 };

            var group1 = new KanbanGroup { Content = "Development", State = state2, AssignedResources = new ObservableCollection<KanbanResource> { resource1 } };
            var group2 = new KanbanGroup { Content = "Marketing", State = state3, AssignedResources = new ObservableCollection<KanbanResource> { resource2 } };
            var groups = new ObservableCollection<KanbanGroup> { group1, group2 };

            // Customize standard item templates.
            KanbanBoard.TaskItemType.ContentTemplate = Resources["TaskTemplate"] as DataTemplate;
            KanbanBoard.BugItemType.ContentTemplate = Resources["BugTemplate"] as DataTemplate;

            // Also, define custom item types to be used by some items.
            var customType1 = new KanbanItemType { ContentTemplate = Resources["GreenTaskContentTemplate"] as DataTemplate };
            var customType2 = new KanbanItemType { Template = Resources["SpecialTaskTemplate"] as DataTemplate };

            var items = new ObservableCollection<KanbanItem>
            {
                new KanbanItem { Content = "Architecture", Group = group1, State = state1, AssignedResources = new ObservableCollection<KanbanResource> { resource1, resource2 } },
                new KanbanItem { Content = "Date-times", Group = group1, State = state2, AssignedResources = new ObservableCollection<KanbanResource> { resource1 }, ItemType = customType2 },
                new KanbanItem { Content = "Weekend issue", Group = group1, State = state2, AssignedResources = new ObservableCollection<KanbanResource> { resource1 }, ItemType = KanbanBoard.BugItemType },
                new KanbanItem { Content = "Diagram", Group = group1, State = state1, AssignedResources = new ObservableCollection<KanbanResource> { resource2 }, ItemType = customType1 },
                new KanbanItem { Content = "Drag and drop", Group = group1, State = state1, AssignedResources = new ObservableCollection<KanbanResource> { resource1 }, ItemType = customType2 },
                new KanbanItem { Content = "Colors", Group = group2, State = state1, AssignedResources = new ObservableCollection<KanbanResource> { resource2 } },
                new KanbanItem { Content = "Logo", Group = group2, State = state2, AssignedResources = new ObservableCollection<KanbanResource> { resource2 } },
                new KanbanItem { Content = "Web site", Group = group2, State = state2, AssignedResources = new ObservableCollection<KanbanResource> { resource1 } },
                new KanbanItem { Content = "Social networking", Group = group2, State = state3, AssignedResources = new ObservableCollection<KanbanResource> { resource2 } },
            };

            items[1].Tag = new TimeInterval { Start = DateTime.Today, Finish = DateTime.Today.AddDays(7) };

            KanbanBoard.States = states;
            KanbanBoard.Groups = groups;
            KanbanBoard.Items = items;
            KanbanBoard.AssignableResources = resources;

            KanbanBoard.NewItemAdded += (sender, e) =>
            {
                e.Item.AssignedResources = new ObservableCollection<KanbanResource> { resource1 };
            };

            KanbanBoard.UsePopupMenus = false;
            KanbanBoard.EditingItem += (sender, e) =>
            {
                MessageBox.Show("Editing item " + e.Item.Content, "Kanban");
            };
        }
    }

    public struct TimeInterval
    {
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
    }
}
