using Android.Content;
using System.ComponentModel;
using TakeCare.Droid.Controls;
using TakeCare.Droid.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(PlaceholderEditor), typeof(PlaceholderEditorRenderer))]
namespace TakeCare.Droid.Droid.Renderers
{
	public class PlaceholderEditorRenderer : EditorRenderer
	{
		public PlaceholderEditorRenderer(Context context) : base(context)
		{
		}

		protected override void OnElementChanged(
			ElementChangedEventArgs<Editor> e)
		{
			base.OnElementChanged(e);

			if(e.NewElement != null)
			{
				var element = e.NewElement as PlaceholderEditor;
				Control.Hint = element.Placeholder;
			}
		}

		protected override void OnElementPropertyChanged(
			object sender,
			PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if(e.PropertyName == PlaceholderEditor.PlaceholderProperty.PropertyName)
			{
				var element = Element as PlaceholderEditor;
				Control.Hint = element.Placeholder;
			}
		}
	}
}