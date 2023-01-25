using ToDoClient.DataService;
using ToDoClient.ViewModel;

namespace ToDoClient.View
{
    public partial class MainPage : ContentPage
    {
        private readonly MainPageViewModel _viewModel;
        public MainPage(MainPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _viewModel.ToDos.Clear();

            _viewModel.GetAllToDoCommand.Execute(null);
        }
    }
}