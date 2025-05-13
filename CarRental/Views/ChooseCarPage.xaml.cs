namespace CarRental.Views;

public partial class ChooseCarPage : ContentPage
{
	public ChooseCarPage(ChooseCarViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }

    private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Car selectedCar &&
            BindingContext is ChooseCarViewModel vm)
        {
            vm.SelectCarCommand.Execute(selectedCar);
        }
    }

}
