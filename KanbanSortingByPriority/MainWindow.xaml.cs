using KanbanLibrary;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KanbanBoardSortingByPriority
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public KanbanState NewState, InProgressState, DoneState;
        public ObservableCollection<KanbanState> KanbanStates => KanbanBoard.States;

        private ObservableCollection<KanbanItem>? KanbanItems = null;

        public MainWindow()
        {
            InitializeComponent();

            NewState = new KanbanState { Content = "New" };
            InProgressState = new KanbanState { Content = "In progress" };
            DoneState = new KanbanState { Content = "Done" };
            KanbanBoard.States = new ObservableCollection<KanbanState>()
            {
                NewState, InProgressState, DoneState
            };

            var backgroundPriority = new SolidColorBrush();
            backgroundPriority.Color = Color.FromRgb(238, 238, 238);

            var priorities = KanbanBoard.DefaultPriorities;

            var lowPriority = priorities.Single(p => p.Number == 10);
            lowPriority.Background = backgroundPriority;
            lowPriority.Foreground = Brushes.Green;

            var mediumPriority = priorities.Single(p => p.Number == 100);
            mediumPriority.Background = backgroundPriority;
            mediumPriority.Foreground = Brushes.CornflowerBlue;

            var highPriority = priorities.Single(p => p.Number == 1000);
            highPriority.Background = backgroundPriority;
            highPriority.Foreground = Brushes.DarkRed;

            var ClarissaResource = new KanbanResource { Content = "Clarissa Candelaria", ImageSource = new BitmapImage(new Uri("pack://application:,,,/KanbanBoardSortingByPriority;component/Images/Clarissa.png", UriKind.RelativeOrAbsolute)), };
            var TysonResource = new KanbanResource { Content = "Tyson Lamberson", ImageSource = new BitmapImage(new Uri("pack://application:,,,/KanbanBoardSortingByPriority;component/Images/Tyson.png", UriKind.RelativeOrAbsolute)) };
            var JoannaResource = new KanbanResource { Content = "Joanna Mcamis", ImageSource = new BitmapImage(new Uri("pack://application:,,,/KanbanBoardSortingByPriority;component/Images/Joanna.png", UriKind.RelativeOrAbsolute)) };
            var JedResource = new KanbanResource { Content = "Jed Markovitz", ImageSource = new BitmapImage(new Uri("pack://application:,,,/KanbanBoardSortingByPriority;component/Images/Jed.png", UriKind.RelativeOrAbsolute)) };
            var resources = new ObservableCollection<KanbanResource> { ClarissaResource, TysonResource, JoannaResource, JedResource };

            var developmentGroup = new KanbanGroup { Content = "Development", State = InProgressState, HandleBrush = Brushes.LightSlateGray, Resources = new ObservableCollection<KanbanResource> { ClarissaResource, TysonResource } };
            var marketingGroup = new KanbanGroup { Content = "Marketing", State = NewState, HandleBrush = Brushes.DarkRed, Resources = new ObservableCollection<KanbanResource> { JoannaResource }, ImageSource = new BitmapImage(new Uri("pack://application:,,,/KanbanBoardSortingByPriority;component/Images/alert.png", UriKind.RelativeOrAbsolute)) };
            var groups = new ObservableCollection<KanbanGroup> { developmentGroup, marketingGroup };

            KanbanBoard.TaskItemType.HandleBrush = Brushes.LightBlue;

            KanbanItems = new ObservableCollection<KanbanItem> () 
            {
                new KanbanItem { Content = "Architecture", State = DoneState, Progress = 1.0, Priority = lowPriority, Resources = new ObservableCollection<KanbanResource> { TysonResource }, Date = DateTime.Today.Add(TimeSpan.Parse("08:00:00")), DueDate = DateTime.Today.Add(TimeSpan.Parse("16:00:00")), ImageSource = new BitmapImage(new Uri("pack://application:,,,/KanbanBoardSortingByPriority;component/Images/Architecture.png", UriKind.RelativeOrAbsolute)) },
                new KanbanItem { Content = "Date-times", State = InProgressState, Progress = 0.50, Resources = new ObservableCollection<KanbanResource> { JoannaResource }, Date = DateTime.Today.Add(TimeSpan.Parse("08:00:00")), DueDate = DateTime.Today.Add(TimeSpan.Parse("16:00:00")), ImageSource = new BitmapImage(new Uri("pack://application:,,,/KanbanBoardSortingByPriority;component/Images/DateTimes.png", UriKind.RelativeOrAbsolute)) },
                new KanbanItem { Content = "Schedules", State = InProgressState, Progress = 0.30, Priority = mediumPriority, Date = DateTime.Today.Add(TimeSpan.Parse("08:00:00")), DueDate = DateTime.Today.Add(TimeSpan.Parse("16:00:00")), Resources = new ObservableCollection<KanbanResource> { JoannaResource, JedResource } },
                new KanbanItem { Content = "Weekend issue", State = InProgressState, Progress = 0.70, Priority = highPriority, Date = DateTime.Today.Add(TimeSpan.Parse("08:00:00")), DueDate = DateTime.Today.Add(TimeSpan.Parse("16:00:00")), Resources = new ObservableCollection<KanbanResource> { JedResource }, ImageSource = new BitmapImage(new Uri("pack://application:,,,/KanbanBoardSortingByPriority;component/Images/flag.png", UriKind.RelativeOrAbsolute)) },
                new KanbanItem { Content = "Diagram", State = InProgressState, Progress = 0.20, Date = DateTime.Today.Add(TimeSpan.Parse("08:00:00")), DueDate = DateTime.Today.Add(TimeSpan.Parse("16:00:00")), Resources = new ObservableCollection<KanbanResource> { ClarissaResource } },
                new KanbanItem { Content = "Bars", State = NewState, Progress = 0.40, Date = DateTime.Today.Add(TimeSpan.Parse("08:00:00")), DueDate = DateTime.Today.Add(TimeSpan.Parse("16:00:00")), Resources = new ObservableCollection<KanbanResource> { ClarissaResource } },
                new KanbanItem { Content = "Nonworking time highlighting", State = DoneState, Date = DateTime.Today.Add(TimeSpan.Parse("08:00:00")), DueDate = DateTime.Today.Add(TimeSpan.Parse("16:00:00")), Resources = new ObservableCollection<KanbanResource> { JedResource } },
                new KanbanItem { Content = "Background", State = DoneState, Date = DateTime.Today.Add(TimeSpan.Parse("08:00:00")), DueDate = DateTime.Today.Add(TimeSpan.Parse("16:00:00")) },
                new KanbanItem { Content = "Milestone bug", State = InProgressState, Date = DateTime.Today.Add(TimeSpan.Parse("08:00:00")), DueDate = DateTime.Today.Add(TimeSpan.Parse("16:00:00")), Resources = new ObservableCollection<KanbanResource> { ClarissaResource } },
                new KanbanItem { Content = "Drag and drop", State = InProgressState, Priority = highPriority, Date = DateTime.Today.Add(TimeSpan.Parse("08:00:00")), DueDate = DateTime.Today.Add(TimeSpan.Parse("16:00:00")), Resources = new ObservableCollection<KanbanResource> { TysonResource } },
                new KanbanItem { Content = "Preparations", State = DoneState, Date = DateTime.Today.Add(TimeSpan.Parse("08:00:00")), DueDate = DateTime.Today.Add(TimeSpan.Parse("16:00:00")), Resources = new ObservableCollection<KanbanResource> { ClarissaResource } },
                new KanbanItem { Content = "Colors", Group = marketingGroup, State = DoneState, Date = DateTime.Today.Add(TimeSpan.Parse("08:00:00")), DueDate = DateTime.Today.Add(TimeSpan.Parse("16:00:00")), Resources = new ObservableCollection<KanbanResource> { JoannaResource } },
                new KanbanItem { Content = "Logo", Group = marketingGroup, State = InProgressState, Date = DateTime.Today.Add(TimeSpan.Parse("08:00:00")), DueDate = DateTime.Today.Add(TimeSpan.Parse("16:00:00")), Resources = new ObservableCollection<KanbanResource> { JoannaResource } },
                new KanbanItem { Content = "Sample apps", Group = marketingGroup, State = InProgressState, Progress = 0.20, Date = DateTime.Today.Add(TimeSpan.Parse("08:00:00")), DueDate = DateTime.Today.Add(TimeSpan.Parse("16:00:00")), Resources = new ObservableCollection<KanbanResource> { JedResource } },
                new KanbanItem { Content = "Screenshots", Group = marketingGroup, State = InProgressState, Progress = 0.40, Date = DateTime.Today.Add(TimeSpan.Parse("08:00:00")), DueDate = DateTime.Today.Add(TimeSpan.Parse("16:00:00")), Resources = new ObservableCollection<KanbanResource> { JedResource } },
                new KanbanItem { Content = "Web site", Group = marketingGroup, State = NewState, Progress = 0.50, Date = DateTime.Today.Add(TimeSpan.Parse("08:00:00")), DueDate = DateTime.Today.Add(TimeSpan.Parse("16:00:00")), Resources = new ObservableCollection<KanbanResource> { TysonResource, JedResource }, ImageSource = new BitmapImage(new Uri("pack://application:,,,/KanbanBoardSortingByPriority;component/Images/WebSite.png", UriKind.RelativeOrAbsolute)) },
                new KanbanItem { Content = "Social networking", Group = marketingGroup, State = NewState, Date = DateTime.Today.Add(TimeSpan.Parse("08:00:00")), DueDate = DateTime.Today.Add(TimeSpan.Parse("16:00:00")), Resources = new ObservableCollection<KanbanResource> { ClarissaResource, JoannaResource } }
            };
            KanbanBoard.Items = KanbanItems;

            KanbanBoard.AvailableResources = resources;
            KanbanBoard.Groups = groups;
            KanbanBoard.States = KanbanStates;

            KanbanBoard.ItemProgressVisibility = Visibility.Visible;
            KanbanBoard.ItemProgressBrush = Brushes.CornflowerBlue;
            KanbanBoard.ItemProgressHeight = 4.0;
            KanbanBoard.ItemProgressLabelVisibility = Visibility.Visible;

            KanbanBoard.ItemDateVisibility = Visibility.Visible;
            KanbanBoard.ItemDueDateVisibility = Visibility.Visible;

            KanbanBoard.PriorityVisibility = Visibility.Visible;
            KanbanBoard.PriorityLabelVisibility = Visibility.Visible;

            KanbanBoard.AreEditMenuItemsHidden = true;
            KanbanBoard.AreStateExpandersVisible = true;
        }

        private void SortingKanban_SelectionChanged(object sender, SelectionChangedEventArgs? e)
        {
            ComboBox? combobox = sender as ComboBox;
            string? sortType = (combobox.SelectedItem as ComboBoxItem).Tag as string;

            switch (sortType)
            {
                case "PriorityAsc":
                    IOrderedEnumerable<KanbanItem> sortedItems = KanbanBoard.Items.OrderBy(i => i.Priority.Number);
                    KanbanBoard.Items = new ObservableCollection<KanbanItem>(sortedItems);
                    break;
                case "PriorityDesc":
                    IOrderedEnumerable<KanbanItem> sortedItemsDesc = KanbanBoard.Items.OrderByDescending(i => i.Priority.Number);
                    KanbanBoard.Items = new ObservableCollection<KanbanItem>(sortedItemsDesc);
                    break;
                case "NoSorting":
                    if (KanbanBoard != null)
                        KanbanBoard.Items = KanbanItems;
                    break;
            }
        }

        private void SearchInContent()
        {
            if (SearchTextBox == null) return;
            var textForSearching = SearchTextBox.Text.Trim();
            if (string.IsNullOrEmpty(textForSearching))
            {
                KanbanBoard.Items = KanbanItems;
                if (SortingKanban.SelectedIndex > 0)
                    SortingKanban_SelectionChanged(SortingKanban, null);
                return;
            }
            KanbanBoard.Items = new ObservableCollection<KanbanItem>(KanbanItems.Where(i => i.Content.ToString().Contains(textForSearching)));
            if (SortingKanban.SelectedIndex > 0)
                SortingKanban_SelectionChanged(SortingKanban, null);
        }

        private void SearchColumnsButton_Click(object sender, RoutedEventArgs e)
        {
            SearchInContent();
        }

        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SearchInContent();
            }
        }
    }
}