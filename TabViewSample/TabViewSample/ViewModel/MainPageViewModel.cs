using Syncfusion.Maui.TabView;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace TabViewSample
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string? propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<Model>? tabItems;
        public ObservableCollection<Model>? TabItems
        {
            get { return tabItems; }
            set
            {
                tabItems = value;
                OnPropertyChanged("TabItems");
            }
        }
        private Command<object>? selectionChangedCommand;
        public Command<object>? SelectionChangedCommand
        {
            get { return selectionChangedCommand; }
            set
            {
                selectionChangedCommand = value;
                OnPropertyChanged("SelectionChangedCommand");
            }
        }
        public MainPageViewModel()
        {
            TabItems = new ObservableCollection<Model>();
            TabItems.Add(new Model() { Name = "Alexandar", TextColor = Colors.Red });
            TabItems.Add(new Model() { Name = "Gabriella", TextColor = Colors.Black });
            TabItems.Add(new Model() { Name = "Clara", TextColor = Colors.Black });
            TabItems.Add(new Model() { Name = "Tye", TextColor = Colors.Black });
            TabItems.Add(new Model() { Name = "Nora", TextColor = Colors.Black });
            TabItems.Add(new Model() { Name = "Sebastian", TextColor = Colors.Black });
            SelectionChangedCommand = new Command<object>(ExecuteSelectionChangedCommand);
        }

        public void ExecuteSelectionChangedCommand(object obj)
        {
            if (obj is SfTabView tabItem)
            {
                if (TabItems != null)
                {
                    // Ensure the selected index is within the valid range
                    if (tabItem.SelectedIndex >= 0 && tabItem.SelectedIndex < TabItems.Count)
                    {
                        for (int i = 0; i < TabItems.Count; i++)
                        {
                            // Set TextColor for selected tab to red, others to black
                            TabItems[i].TextColor = (i == (int)tabItem.SelectedIndex) ? Colors.Red : Colors.Black;
                        }
                    }
                }
            }
        }

    }
}
