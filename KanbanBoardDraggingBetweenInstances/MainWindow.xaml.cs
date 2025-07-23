using KanbanLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace KanbanBoardDraggingBetweenInstances
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            SourceKanbanBoard.States = new ObservableCollection<KanbanState>
            {
                new KanbanState { Content = "Orders" },
                new KanbanState { Content = "Pickorders" },
            };
            SourceKanbanBoard.Items = new ObservableCollection<KanbanItem>
            {
                new KanbanItem { Content = "Black skirt", State = SourceKanbanBoard.States.Last() },
                new KanbanItem { Content = "White dress", State = SourceKanbanBoard.States.Last() },
                new KanbanItem { Content = "Pink dress", State = SourceKanbanBoard.States.Last() },
                new KanbanItem { Content = "Red T-shirt" },
                new KanbanItem { Content = "Red skirt" },
            };
            SourceKanbanBoard.AreNewItemButtonsHidden = true;

            TargetKanbanBoard.Groups = new ObservableCollection<KanbanGroup>
            {
                new KanbanGroup { Content = "July" },
                new KanbanGroup { Content = "August" },
                new KanbanGroup { Content = "September" },
                new KanbanGroup { Content = "October" },
                new KanbanGroup { Content = "November" },
                new KanbanGroup { Content = "December" },
            };
            TargetKanbanBoard.Items = new ObservableCollection<KanbanItem>
            {
                new KanbanItem { Content = "Yellow dress" },
            };
            TargetKanbanBoard.AreNewItemButtonsHidden = true;
        }

        private Point DraggingPosition;
        private KanbanItem? DraggingItem;
        private KanbanGroup? DraggingTargetGroup;
        private KanbanState? DraggingTargetState;

        private void Thumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            var element = sender as Thumb;
            DraggingItem = element!.DataContext as KanbanItem;
            var panel = VisualTreeHelper.GetParent(element) as Panel;
            var container = VisualTreeHelper.GetParent(panel) as FrameworkElement;
            DraggingPosition = container!.TranslatePoint(new Point(0, 0), Root);
            DraggingItemControl.DataContext = DraggingItem;
            DraggingItemControl.Width = container.ActualWidth;
            DraggingItemControl.Height = container.ActualHeight;
            Canvas.SetLeft(DraggingItemControl, DraggingPosition.X);
            Canvas.SetTop(DraggingItemControl, DraggingPosition.Y);
            DraggingItemControl.Visibility = Visibility.Visible;
        }

        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            var pos = new Point(DraggingPosition.X + e.HorizontalChange, DraggingPosition.Y + e.VerticalChange);
            Canvas.SetLeft(DraggingItemControl, pos.X);
            Canvas.SetTop(DraggingItemControl, pos.Y);
            if (DraggingTargetGroup != null)
                DraggingTargetGroup.Background = null;
            DraggingTargetGroup = null;
            DraggingTargetState = null;
            pos.X += DraggingItemControl.ActualWidth / 2;
            pos.Y += DraggingItemControl.ActualHeight / 2;
            var targetPos = Root.TranslatePoint(pos, TargetKanbanBoard);
            if (targetPos.X < 0 || targetPos.Y < 0 || targetPos.X >= TargetKanbanBoard.ActualWidth || targetPos.Y >= TargetKanbanBoard.ActualHeight)
                return;
            VisualTreeHelper.HitTest(TargetKanbanBoard,
                new HitTestFilterCallback((obj) => obj is ScrollViewer ? HitTestFilterBehavior.ContinueSkipSelf : HitTestFilterBehavior.Continue),
                new HitTestResultCallback((result) => {
                    var element = result.VisualHit as FrameworkElement;
                    while (element != null)
                    {
                        var item = element?.DataContext as KanbanItem;
                        if (item is KanbanGroup)
                            item = null;
                        DraggingTargetGroup = item?.Group ?? DraggingTargetGroup;
                        DraggingTargetGroup = element?.DataContext as KanbanGroup ?? DraggingTargetGroup;
                        DraggingTargetState = item?.State ?? DraggingTargetState;
                        DraggingTargetState = element?.DataContext as KanbanState ?? DraggingTargetState;
                        element = VisualTreeHelper.GetParent(element) as FrameworkElement;
                    }
                    return HitTestResultBehavior.Continue;
                }),
                new PointHitTestParameters(targetPos));
            if (DraggingTargetGroup != null)
                DraggingTargetGroup.Background = Brushes.AliceBlue;
        }

        private void Thumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            DraggingItemControl.Visibility = Visibility.Collapsed;
            if (DraggingTargetGroup != null)
                DraggingTargetGroup.Background = null;
            if (TargetKanbanBoard.Items.Count > 0 && (DraggingTargetGroup == null || DraggingTargetState == null))
                return;
            SourceKanbanBoard.Items.Remove(DraggingItem);
            DraggingItem!.Group = DraggingTargetGroup ?? TargetKanbanBoard.Groups.First();
            DraggingItem!.State = DraggingTargetState ?? TargetKanbanBoard.States.First();
            TargetKanbanBoard.Items.Add(DraggingItem);
        }
    }
}
