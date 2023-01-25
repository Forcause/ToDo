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
    [QueryProperty(nameof(EditedToDo), nameof(EditedToDo))]
    public class ManageToDoViewModel : BaseViewModel
    {
        private readonly IRestDataService _dataService;

        public Command ReturnCommand { get; }
        public Command SaveCommand { get; }
        public Command DeleteCommand { get; }

        private bool _isNew;

        private ToDoModel _toDoModel;
        public ToDoModel EditedToDo
        {
            get => _toDoModel;
            set
            {
                _isNew = IsNew(value);
                _toDoModel = value;
                NotifyPropertyChanged();
            }
        }


        public ManageToDoViewModel(IRestDataService dataService)
        {
            Title = "Manage ToDo";
            _dataService = dataService;
            ReturnCommand = new Command(ReturnToMainPage);
            SaveCommand = new Command(SaveChanges);
            DeleteCommand = new Command(DeleteToDo);
        }

        private async void DeleteToDo()
        {
            await _dataService.DeleteToDoAsync(EditedToDo.Id);

            ReturnToMainPage();
        }

        private async void SaveChanges()
        {
            if (_isNew)
            {
                await _dataService.AddToDoAsync(EditedToDo);
            }
            else await _dataService.UpdateToDoModelAsync(EditedToDo);
            
            ReturnToMainPage();
        }

        private async void ReturnToMainPage()
        {
            await Shell.Current.GoToAsync("..");
        }

        private bool IsNew(ToDoModel toDo) => toDo.Id == 0;
    }
}
