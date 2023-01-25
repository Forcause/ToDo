using ToDoClient.ViewModel;

namespace ToDoClient.View;

public partial class ManageToDoView : ContentPage
{
	public ManageToDoView(ManageToDoViewModel _viewModel)
	{
		InitializeComponent();

		BindingContext = _viewModel;
	}
}