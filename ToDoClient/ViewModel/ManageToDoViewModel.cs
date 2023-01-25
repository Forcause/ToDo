using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoClient.DataService;
using ToDoClient.Model;
using ToDoClient.View;

namespace ToDoClient.ViewModel
{
    [QueryProperty(nameof(SelectedToDo), nameof(SelectedToDo))]
    public class ManageToDoViewModel : BaseViewModel
    {
        private readonly IRestDataService _dataService;

        public Command ReturnCommand { get; }

        private ToDoModel _toDoModel;
        public ToDoModel SelectedToDo
        {
            get => _toDoModel;
            set
            {
                if (_toDoModel == value) return;

                _toDoModel = value;
                NotifyPropertyChanged();
            }
        }


        public ManageToDoViewModel(IRestDataService dataService)
        {
            Title = "Manage ToDo";
            _dataService = dataService;
            ReturnCommand = new Command(ReturnToMainPage);
        }

        //ToDo bug with returning to the main page
        private async void ReturnToMainPage()
        {
            await AppShell.Current.GoToAsync(nameof(MainPage));
        }
    }
}
