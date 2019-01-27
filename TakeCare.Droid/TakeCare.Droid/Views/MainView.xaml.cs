using TakeCare.Droid.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TakeCare.Droid.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainView : ContentPage
	{
		private MainViewModel _viewModel;
		public MainView()
		{
			InitializeComponent();
			BindingContext = _viewModel = new MainViewModel();
		}

		private async void TakePhotoEntry_Clicked(object sender, System.EventArgs e)
		{
			await _viewModel.TakePhoto();
		}

		private async void SubmitButton_Clicked(object sender, System.EventArgs e)
		{
			await _viewModel.SendEventData(EventTypePicker.SelectedIndex);
		}


		private void EventTypePicker_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			var picker = (Picker)sender;
			int selectedIndex = picker.SelectedIndex;

			if(selectedIndex != -1)
			{
				_viewModel.LocationText = picker.Items[selectedIndex];
				_viewModel.ChangeEventTypeImage(selectedIndex);
			}
		}
	}
}