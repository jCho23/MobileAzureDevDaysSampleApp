using System;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

using MobileAzureDevDaysSampleApp.Controls;
using MobileAzureDevDaysSampleApp.Droid.Effects;

[assembly: ResolutionGroupName("Xamarin")]
[assembly: ExportEffect(typeof(PlaceholderEditorEffect), nameof(PlaceholderEditorEffect))]

namespace MobileAzureDevDaysSampleApp.Droid.Effects
{
    public class PlaceholderEditorEffect
    {
        public class PlacehoderEditorRenderer : EditorRenderer
        {
            protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
            {
                base.OnElementChanged(e);

                if (Element == null)
                    return;

                var element = (EditorWithPlaceholder)Element;

                Control.Hint = element.Placeholder;
                Control.SetHintTextColor(element.PlaceholderTextColor.ToAndroid());
            }
        }
    }
}