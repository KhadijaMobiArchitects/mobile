using System;
using System.Reflection;
using FFImageLoading.Svg.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace RHMobile
{
    [Preserve(AllMembers = true)]
    [ContentProperty(nameof(Source))]
    public class ImageResourceExtension : IMarkupExtension
    {
        public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null)
                return null;

            if (Source.EndsWith(".svg"))

                return SvgImageSource.FromResource("RHMobile.Resources.Images." + Source, typeof(ImageResourceExtension).GetTypeInfo().Assembly);

            return ImageSource.FromResource("RHMobile.Resources.Images." + Source, typeof(ImageResourceExtension).GetTypeInfo().Assembly);
        }
    }
}
