using System;
using Xamarin.Forms;

namespace TakeCare.Droid.Controls
{
	public class PlaceholderEditor : Editor
	{
		public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(PlaceholderEditor), typeof(string), typeof(View), String.Empty);

		public PlaceholderEditor()
		{
		}

		public string Placeholder
		{
			get => (string)GetValue(PlaceholderProperty);
			set => SetValue(PlaceholderProperty, value);
		}
	}
}
