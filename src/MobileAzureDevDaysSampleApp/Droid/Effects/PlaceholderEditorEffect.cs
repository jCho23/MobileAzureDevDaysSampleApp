using System;

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

		protected override void OnAttached()
		{
			try
			{
                backgroundColor = Android.Graphics.Color.White;
				Control.SetBackgroundColor(backgroundColor);

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