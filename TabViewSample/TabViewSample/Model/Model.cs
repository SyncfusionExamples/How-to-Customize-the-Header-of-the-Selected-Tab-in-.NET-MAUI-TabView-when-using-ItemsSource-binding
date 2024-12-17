using System.ComponentModel;

namespace TabViewSample
{
    public class Model : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string? propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private string? name;

        public string? Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        private Color? textColor;
        public Color? TextColor
        {
            get { return textColor; }
            set
            {
                textColor = value;
                OnPropertyChanged("TextColor");
            }
        }
    }
}
