using System;
using MobileAzureDevDaysSampleApp.Controls;
using MobileAzureDevDaysSampleApp.iOS.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("Xamarin")]
[assembly: ExportEffect(typeof(PlaceholderEditorEffect), nameof(PlaceholderEditorEffect))]
namespace MobileAzureDevDaysSampleApp.iOS.Effects
{
    public class PlaceholderEditorEffect : PlatformEffect
    {
        public PlaceholderEditorEffect()
        {
        }

        UILabel placeholderText;
        UITextView nativeTextField;
        EditorWithPlaceholder formsEditor;

        protected override void OnAttached()
        {
            formsEditor = Element as EditorWithPlaceholder;
            nativeTextField = Control as UITextView;

            placeholderText = new UILabel
            {
                LineBreakMode = UILineBreakMode.WordWrap,
                Lines = 1,
                BackgroundColor = UIColor.Clear,
                TextColor = formsEditor.PlaceholderTextColor.ToUIColor(),
                Text = formsEditor.Placeholder
            };

            if (string.IsNullOrEmpty(formsEditor.Text))
                placeholderText.Alpha = 1;
            else
                placeholderText.Alpha = 0;

            nativeTextField.AddSubview(placeholderText);
        }

        protected override void OnDetached()
        {
        }

        protected override void OnElementPropertyChanged(System.ComponentModel.PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);

            switch(args.PropertyName)
            {
                case nameof(EditorWithPlaceholder.Placeholder):
                    placeholderText.Text = formsEditor.Placeholder;
                    placeholderText.SizeToFit();
                    break;
                case nameof(EditorWithPlaceholder.PlaceholderTextColor):
                    placeholderText.TextColor = formsEditor.PlaceholderTextColor.ToUIColor();
                    break;
                case nameof(EditorWithPlaceholder.Width):
                    placeholderText.Frame = new CoreGraphics.CGRect(8, 5, formsEditor.Width - 16, 30);
                    break;
                case nameof(EditorWithPlaceholder.Text):
					if (string.IsNullOrWhiteSpace(formsEditor.Text))
						placeholderText.Alpha = 1;
					else
						placeholderText.Alpha = 0;
                    break;
            }

        }
    }
}