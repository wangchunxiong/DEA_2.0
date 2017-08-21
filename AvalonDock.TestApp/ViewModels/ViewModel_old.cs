using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DEA3
{
    public class ViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Node> nodeList;
        public ObservableCollection<Node> NodeList
        {
            get
            {
                return this.nodeList;
            }
            set
            {
                if (this.nodeList != value)
                {
                    this.nodeList = value;
                    OnPropertyChanged("NodeList");
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
