using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ToDoClient.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private string _title;
        public string Title 
        {
            get => _title;
            set
            {
                if (_title == value) return;

                _title = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Title)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        internal void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
