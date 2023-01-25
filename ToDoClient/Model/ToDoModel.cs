using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoClient.Model
{
    public class ToDoModel : INotifyPropertyChanged
    {
        private int _id;
        public int Id 
        {
            get => _id;
            set
            {
                if (_id == value) return;

                _id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
            }
        }

        private string _name;
        public string ToDoName 
        {
            get => _name;
            set
            {
                if (_name == value) return;

                _name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ToDoName)));
            }
        }

        private string _toDoDescription;
        public string? ToDoDescription 
        {
            get => _toDoDescription;
            set
            {
                if (_toDoDescription == value) return;
                _toDoDescription = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ToDoDescription)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

}
