using KanbanLibrary;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace KanbanBoardCustomColumnsSample
{
    /// <summary>
    /// Interaction logic for EditKanbanStatesDialog.xaml
    /// </summary>
    public partial class EditKanbanStatesDialog : Window
    {
        public EditKanbanStatesDialog()
        {
            InitializeComponent();
        }

        public MainWindow MainWindow => Owner as MainWindow;

        public ObservableCollection<KanbanState> KanbanStates => MainWindow.KanbanStates;
        
        private void KanbanState_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var KanbanState = sender as KanbanState;
            if (KanbanState != null)
            {
                if (e.PropertyName == "Content")
                {
                    var KanbanStateDuplicate = CheckStateUnicity(KanbanState);
                    if (KanbanStateDuplicate != null)
                    {
                        MessageBox.Show("Your column names aren't unique, please check the names.");
                        KanbanState.Content = GetUniqueStateName(KanbanState.Content.ToString());
                    }
                }
            }
        }

        private KanbanState CheckStateUnicity(KanbanState kanbanState)
        {
            if (kanbanState == null)
                return null;

            string name = kanbanState.Content.ToString();
            var count = KanbanStates.Count(i => i.Content.ToString() == name && i != kanbanState);
            if (count >= 1)
            {
                return kanbanState;
            }
            return null;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var ks in KanbanStates)
                ks.PropertyChanged -= KanbanState_PropertyChanged;
            Close();
        }

        private void AddStateButton_Click(object sender, RoutedEventArgs e)
        {
            KanbanState newState = new KanbanState() { Content = "New Column" };
            newState.Content = GetUniqueStateName(newState.Content.ToString());
            KanbanStates.Insert( KanbanStates.Count - 1, newState);
            newState.PropertyChanged += KanbanState_PropertyChanged;
            Dispatcher.BeginInvoke((Action)delegate
            {
                KanbanStatesDataGrid.SelectedItem = newState;
                KanbanStatesDataGrid.CurrentColumn = KanbanStatesDataGrid.Columns.First();
                KanbanStatesDataGrid.CurrentItem = newState;
                KanbanStatesDataGrid.BeginEdit();
            }, System.Windows.Threading.DispatcherPriority.Background);
        }

        private string GetUniqueStateName(string proposedName)
        {
            while (KanbanStates.Any(s => s.Content.ToString().ToLowerInvariant() == proposedName.ToLowerInvariant()))
            {
                int indexOfLastSpace = proposedName.LastIndexOf(' ');
                int lastNumber = 0;
                if (indexOfLastSpace >= 0)
                {
                    if (int.TryParse(proposedName.Substring(indexOfLastSpace + 1), out lastNumber))
                        proposedName = proposedName.Substring(0, indexOfLastSpace);
                }
                proposedName = proposedName + " " + (lastNumber + 1);
            }
            return proposedName;
        }

        private void MoveUpButton_Click(object sender, RoutedEventArgs e)
        {
            int index = KanbanStatesDataGrid.SelectedIndex;
            if (index < 1)
                return;
            KanbanStates.Move(index, index - 1);
        }

        private void MoveDownButton_Click(object sender, RoutedEventArgs e)
        {
            int index = KanbanStatesDataGrid.SelectedIndex;
            if (index == KanbanStatesDataGrid.Items.Count - 1)
                return;
            KanbanStates.Move(index, index + 1);
        }

        private void DeleteStateButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete the selected column?", "Question", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return;
            var ks = (KanbanState)KanbanStatesDataGrid.SelectedItem;
            ks.PropertyChanged -= KanbanState_PropertyChanged;
            KanbanStates.Remove(ks);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (KanbanState kanbanState in KanbanStates)
                kanbanState.PropertyChanged += KanbanState_PropertyChanged;
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            foreach (KanbanState kanbanState in KanbanStates)
                kanbanState.PropertyChanged -= KanbanState_PropertyChanged;
        }
    }
}
