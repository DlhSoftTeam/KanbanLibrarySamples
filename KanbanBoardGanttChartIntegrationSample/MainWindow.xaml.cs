using DlhSoft.Windows.Controls;
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

namespace KanbanBoardGanttChartIntegrationSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            GanttChartItem item0 = GanttChartDataGrid.Items[0];
            item0.AssignmentsContent = "Clarissa Candelaria";

            GanttChartItem item1 = GanttChartDataGrid.Items[1];
            item1.Start = DateTime.Today.Add(TimeSpan.Parse("08:00:00"));
            item1.Finish = DateTime.Today.AddDays(3).Add(TimeSpan.Parse("16:00:00"));
            item1.CompletedFinish = DateTime.Today.Add(TimeSpan.Parse("12:00:00"));
            item1.AssignmentsContent = "Clarissa Candelaria";

            GanttChartItem item2 = GanttChartDataGrid.Items[2];
            item2.Start = DateTime.Today.AddDays(4).Add(TimeSpan.Parse("08:00:00"));
            item2.Finish = DateTime.Today.AddDays(10).Add(TimeSpan.Parse("16:00:00"));
            item2.AssignmentsContent = "Tyson Lamberson";
            item2.Predecessors.Add(new PredecessorItem { Item = item1 });

            GanttChartItem item3 = GanttChartDataGrid.Items[3];
            item3.Predecessors.Add(new PredecessorItem { Item = item0, DependencyType = DependencyType.StartStart });

            GanttChartItem item4 = GanttChartDataGrid.Items[4];
            item4.Start = DateTime.Today.Add(TimeSpan.Parse("08:00:00"));
            item4.Finish = DateTime.Today.AddDays(4).Add(TimeSpan.Parse("12:00:00"));
            item4.AssignmentsContent = "Joanna Mcamis";

            GanttChartItem item6 = GanttChartDataGrid.Items[6];
            item6.Start = DateTime.Today.Add(TimeSpan.Parse("08:00:00"));
            item6.Finish = DateTime.Today.AddDays(5).Add(TimeSpan.Parse("12:00:00"));
            item6.AssignmentsContent = "Jed Markovitz";

            GanttChartItem item7 = GanttChartDataGrid.Items[7];
            item7.Start = DateTime.Today.AddDays(6);
            item7.IsMilestone = true;
            item7.Predecessors.Add(new PredecessorItem { Item = item4 });
            item7.Predecessors.Add(new PredecessorItem { Item = item6 });
            item7.AssignmentsContent = "Joanna Mcamis";

            // for (int i = 1; i <= 25; i++)
            // {
            //     GanttChartDataGrid.Items.Add(
            //         new GanttChartItem
            //         {
            //             Content = "Task " + i,
            //             Indentation = i % 3 == 0 ? 0 : 1,
            //             Start = DateTime.Today.AddDays(i <= 8 ? (i - 4) * 2 : i - 8),
            //             Finish = DateTime.Today.AddDays((i <= 8 ? (i - 4) * 2 + (i > 8 ? 6 : 1) : i - 2) + 2),
            //             CompletedFinish = DateTime.Today.AddDays(i <= 8 ? (i - 4) * 2 : i - 8).AddDays(i % 6 == 4 ? 3 : 0)
            //         });
            // }

            InitializeKanbanBoard();
        }

        private KanbanState newState, inProgressState, doneState;

        private void InitializeKanbanBoard()
        {
            GanttChartDataGrid.ApplyTemplate();

            newState = new KanbanState { Content = "New" };
            inProgressState = new KanbanState { Content = "In progress" };
            doneState = new KanbanState { Content = "Done" };
            KanbanBoard.States = new ObservableCollection<KanbanState>()
            {
                newState, inProgressState, doneState
            };

            var groups = new ObservableCollection<KanbanGroup>();
            foreach (var ganttChartItem in GanttChartDataGrid.Items.Where(i => i.HasChildren))
            {
                groups.Add(new KanbanGroup
                {
                    Content = ganttChartItem.Content,
                    Tag = ganttChartItem
                });
            }
            KanbanBoard.Groups = groups;
            KanbanBoard.IsGroupStatusHidden = true;
            KanbanBoard.GroupResourcesVisibility = Visibility.Collapsed;
            KanbanBoard.CollapsedGroupItemsCountVisibility = Visibility.Visible;

            var resources = new ObservableCollection<KanbanResource>();
            foreach (var resource in GanttChartDataGrid.GetAssignedResources())
            {
                resources.Add(new KanbanResource
                {
                    Content = resource,
                    Tag = resource
                });
            }
            KanbanBoard.AssignableResources = resources;
            KanbanBoard.ResourceImageVisibility = Visibility.Collapsed;

            var items = new ObservableCollection<KanbanItem>();
            foreach (var ganttChartItem in GanttChartDataGrid.GetLeaves())
            {
                items.Add(new KanbanItem
                {
                    Content = ganttChartItem.Content,
                    Group = groups.First(g => g.Tag == ganttChartItem.Parent),
                    State = ganttChartItem.CompletedFinish == ganttChartItem.Start ? newState : (ganttChartItem.CompletedFinish == ganttChartItem.Finish ? doneState : inProgressState),
                    AssignedResources = new ObservableCollection<KanbanResource>(resources.Where(r => r.Tag as string == ganttChartItem.AssignmentsContent as string)),
                    Tag = ganttChartItem
                });
            }
            KanbanBoard.Items = items;
            KanbanBoard.IsItemContentReadOnly = true;
            KanbanBoard.IsItemGroupReadOnly = true;
            KanbanBoard.AreEditItemButtonsHidden = true;
            KanbanBoard.AreEditGroupButtonsHidden = true;
            KanbanBoard.AreNewItemButtonsHidden = true;

            GanttChartDataGrid.ItemPropertyChanged += GanttChartDataGrid_ItemPropertyChanged;
            foreach (var item in items)
                item.PropertyChanged += Item_PropertyChanged;
        }

        private void GanttChartDataGrid_ItemPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (isDuringInternalPropertyChange)
                return;
            var ganttChartItem = sender as GanttChartItem;
            var item = KanbanBoard.Items.FirstOrDefault(i => i.Tag == ganttChartItem);
            if (item == null)
                return;
            isDuringInternalPropertyChange = true;
            switch (e.PropertyName)
            {
                case nameof(GanttChartItem.Completion):
                    if (ganttChartItem.Finish > ganttChartItem.Start)
                    {
                        if (ganttChartItem.Completion == 0)
                            KanbanBoard.SetItemState(item, newState);
                        else if (ganttChartItem.Completion == 1)
                            KanbanBoard.SetItemState(item, doneState);
                        else
                            KanbanBoard.SetItemState(item, inProgressState);
                    }
                    break;
                case nameof(GanttChartItem.AssignmentsContent):
                    item.AssignedResources = new ObservableCollection<KanbanResource>(KanbanBoard.AssignableResources.Where(r => r.Tag as string == ganttChartItem.AssignmentsContent as string));
                    break;
            }
            isDuringInternalPropertyChange = false;
        }

        private void Item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (isDuringInternalPropertyChange)
                return;
            var item = sender as KanbanItem;
            var ganttChartItem = item.Tag as GanttChartItem;
            isDuringInternalPropertyChange = true;
            switch (e.PropertyName)
            {
                case nameof(KanbanItem.State):
                    if (item.State == newState)
                        ganttChartItem.Completion = 0;
                    else if (item.State == doneState)
                        ganttChartItem.Completion = 1;
                    else
                        ganttChartItem.Completion = 0.5;
                    break;
                case nameof(KanbanItem.AssignedResources):
                    ganttChartItem.AssignmentsContent = string.Join(", ", item.AssignedResources.Select(r => r.Tag as string));
                    break;
            }
            isDuringInternalPropertyChange = false;
        }

        private bool isDuringInternalPropertyChange;
    }
}
