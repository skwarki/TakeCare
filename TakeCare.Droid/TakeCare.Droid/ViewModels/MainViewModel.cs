using Plugin.DeviceInfo;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TakeCare.Droid.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TakeCare.Droid.ViewModels
{
	public class MainViewModel : BaseViewModel
	{
		public string LocationText { get; set; }
		public List<string> EventTypes { get; set; }
		public string ImagePatch { get; set; }

		public MainViewModel()
		{
			IsBusy = false;
			IsDisplayed = true;
			EventTypes = new List<string>();
			ImagePatch = string.Empty;
			LocationText = string.Empty;
			PopulateEventsList();
		}

		public async Task TakePhoto()
		{
			try
			{

				await CrossMedia.Current.Initialize();

				if(!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
				{
					ErrorMessage("An error occurred while trying to start the camera");
					return;
				}

				var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
				{
					Directory = "Sample",
					Name = "test.jpg"
				});

				if(file == null)
					return;
				ImagePatch = file.Path;
				Photo.Source = ImageSource.FromStream(() =>
				{
					var stream = file.GetStream();
					return stream;
				});
				IsDisplayed = false;
			}

			catch(PermissionException)
			{
				ErrorMessage("The application does not have permission to access the device's camera");
			}
			catch(Exception)
			{
				ErrorMessage("An error occured while trying to take photo");
			}

		}

		public async Task<string> GetLocation()
		{
			try
			{
				var request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(2));
				var location = await Geolocation.GetLocationAsync(request);

				if(location != null)
					LocationText = string.Format($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
			}

			catch(PermissionException)
			{
				ErrorMessage("The application does not have permission to access the device's location");
			}
			catch(Exception)
			{
				ErrorMessage("An error occurred while trying to download the device location");
			}
			return LocationText;
		}

		void PopulateEventsList()
		{
			EventTypes.Add("agression");
			EventTypes.Add("vandalism");
			EventTypes.Add("drinking in a public place");
			EventTypes.Add("smoking in a public place");
			EventTypes.Add("littering");
		}
		public void ChangeEventTypeImage(int selectedIndex)
		{
			if(selectedIndex == 0)
				EventTypeImage = "Aggression.png";
			else if(selectedIndex == 1)
				EventTypeImage = "Vandalism.png";
			else if(selectedIndex == 2)
				EventTypeImage = "Glass.png";
			else if(selectedIndex == 3)
				EventTypeImage = "Cigarette.png";
			else if(selectedIndex == 4)
				EventTypeImage = "Poop.png";
		}

		public async Task SendEventData(int selectedIndex)
		{
			try
			{
				IsBusy = true;
				var _event = await CreateEvent(selectedIndex);
				HttpServerConnection connection = new HttpServerConnection();
				await connection.PostRequest(_event);
				IsDisplayed = true;
			}
			catch(Exception)
			{
				ErrorMessage("An error occurred while sending the request");
			}
			IsBusy = false;
			ErrorMessage("Your application has been accepted, thank you", "");
		}

		public async Task<byte[]> ReadFully(Stream input)
		{
			using(MemoryStream ms = new MemoryStream())
			{
				await input.CopyToAsync(ms);
				return ms.ToArray();
			}
		}
		async Task<byte[]> ConvertImageToByte64()
		{
			if(string.IsNullOrEmpty(ImagePatch))
				return await Task.FromResult(new byte[] { });
			FileStream fs = new FileStream(ImagePatch, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true);
			return await ReadFully(fs);
		}

		async Task<Event> CreateEvent(int selectedIndex)
		{
			Event _event = new Event
			{
				IssuedDate = DateTime.Now,
				Description = Description,
				Location = await GetLocation(),
				Photo = await ConvertImageToByte64(),
				Type = selectedIndex,
				DeviceId = CrossDeviceInfo.Current.Id,
			};
			return _event;
		}
	}
}
