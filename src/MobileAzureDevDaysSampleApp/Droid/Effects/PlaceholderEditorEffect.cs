using System;

using Android.Widget;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

using MobileAzureDevDaysSampleApp.Controls;
using MobileAzureDevDaysSampleApp.Droid.Effects;

[assembly: ResolutionGroupName("Xamarin")]
[assembly: ExportEffect(typeof(PlaceholderEditorEffect), nameof(PlaceholderEditorEffect))]
namespace MobileAzureDevDaysSampleApp.Droid.Effects
{
    public class PlaceholderEditorEffect : PlatformEffect
    {
        Android.Graphics.Color backgroundColor;

        public PlaceholderEditorEffect()
        {
            
        }

		TextView placeholderText;
        EditText nativeTextField;
        EditorWithPlaceholder formsEditor;

		protected override void OnAttached()
		{
            formsEditor = Element as EditorWithPlaceholder;
            nativeTextField = Control as EditText;

            try
            {
				placeholderText = new TextView(Forms.Context)
                {
                    Text= formsEditor.Placeholder,
                    
                };
                placeholderText.SetBackgroundColor(Android.Graphics.Color.Transparent);
                placeholderText.SetTextColor(formsEditor.PlaceholderTextColor.ToAndroid());
                placeholderText.SetLines(1);


                backgroundColor = Android.Graphics.Color.White;
				Control.SetBackgroundColor(backgroundColor);

				if (string.IsNullOrEmpty(formsEditor.Text))
					placeholderText.Alpha = 1;
				else
					placeholderText.Alpha = 0;

				nativeTextField.Add(placeholderText);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
			}
		}

		protected override void OnDetached()
		{
		}

		protected override void OnElementPropertyChanged(System.ComponentModel.PropertyChangedEventArgs args)
		{
			base.OnElementPropertyChanged(args);
			try
			{
				if (args.PropertyName == "IsFocused")
				{
					if (((Android.Graphics.Drawables.ColorDrawable)Control.Background).Color == backgroundColor)
					{
                        Control.SetBackgroundColor(Android.Graphics.Color.White);
					}
					else
					{
						Control.SetBackgroundColor(backgroundColor);
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
			}
		}
	}
}