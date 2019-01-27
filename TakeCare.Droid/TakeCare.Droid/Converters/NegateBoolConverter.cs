using System;
using System.Globalization;
using Xamarin.Forms;

namespace TakeCare.Droid.Converters
{
	public class NegateBoolConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if((bool)value)
				return false;
			else
				return true;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if((bool)value)
				return false;
			else
				return true;
		}

	}
}
