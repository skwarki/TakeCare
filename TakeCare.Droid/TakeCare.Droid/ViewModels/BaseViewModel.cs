using Acr.UserDialogs;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace TakeCare.Droid.ViewModels
{

	public class BaseViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs((propertyName)));
		}

		protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
		{
			if(EqualityComparer<T>.Default.Equals(storage, value))
				return false;

			storage = value;
			OnPropertyChanged(propertyName);

			return true;
		}
		protected async void ErrorMessage(string message, string title = "Error")
		{
			await UserDialogs.Instance.AlertAsync(
				 message: message,
				 title: title);
		}

		private Image _photo = new Image();
		public Image Photo
		{
			get => _photo;
			set => SetProperty(ref _photo, value);
		}
		private ImageSource _eventTypeImage;
		public ImageSource EventTypeImage
		{
			get => _eventTypeImage;
			set => SetProperty(ref _eventTypeImage, value);
		}
		private bool _isBusy;
		public bool IsBusy
		{
			get => _isBusy;
			set => SetProperty(ref _isBusy, value);
		}
		private bool _IsDisplayed;
		public bool IsDisplayed
		{
			get => _IsDisplayed;
			set => SetProperty(ref _IsDisplayed, value);
		}
		private string _description = string.Empty;
		public string Description
		{
			get => _description;
			set => SetProperty(ref _description, value);
		}
	}

}
