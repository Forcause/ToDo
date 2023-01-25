
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using ToDoClient.DataService;
using ToDoClient.Model;
using ToDoClient.View;

namespace ToDoClient.ViewModel
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly IRestDataService _dataService;

        public ObservableCollection<ToDoModel> ToDos { get; set; } = new();

        private ToDoModel _selectedToDo;
        public ToDoModel SelectedToDo
        {
            get => _selectedToDo;
            set
            {
                if (_selectedToDo == value) return;

                _selectedToDo = value;
            }
        }

        public Command GetAllToDoCommand { get; }
        public Command AddToDoCommand { get; }
        public Command SelectionChangeCommand { get; }

        public MainPageViewModel(IRestDataService dataService)
        {
            Title = "My ToDo";
            _dataService = dataService;
            GetAllToDoCommand = new Command(GetAllToDos);
            AddToDoCommand = new Command(AddToDo);
            SelectionChangeCommand = new Command(SelectionChange);
        }

        private async void SelectionChange()
        {
            if (_selectedToDo == null) return;
            var navigationParameter = new Dictionary<string, object>()
            {
                { "EditedToDo", _selectedToDo }
            };

            await Shell.Current.GoToAsync(nameof(ManageToDoView), navigationParameter);
        }

        private async void AddToDo()
        {
            var navigationParameter = new Dictionary<string, object>()
            {
                { "EditedToDo", new ToDoModel() }
            };

            await Shell.Current.GoToAsync(nameof(ManageToDoView), navigationParameter);
        }
        private async void GetAllToDos()
        {
            var toDos = await _dataService.GetAllToDosAsync();
            if (toDos?.Count != 0)
            {
                foreach (var toDo in toDos)
                {
                    ToDos.Add(toDo);
                }
            }
        }
    }
}
