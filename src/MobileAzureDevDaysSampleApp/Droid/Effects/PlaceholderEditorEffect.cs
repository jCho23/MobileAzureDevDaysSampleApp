using System;
using MobileAzureDevDaysSampleApp.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

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

                var element = (PlaceholderEditor)Element;

                Control.Hint = element.Placeholder;
                Control.SetHintTextColor(element.PlaceholderColor.ToAndroid());
            }
        }
    }
}