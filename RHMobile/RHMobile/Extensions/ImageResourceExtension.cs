using System;
using System.Reflection;
using FFImageLoading.Svg.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace XForms
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

                return SvgImageSource.FromResource("XForms.Resources.Images." + Source, typeof(ImageResourceExtension).GetTypeInfo().Assembly);

            return ImageSource.FromResource("XForms.Resources.Images." + Source, typeof(ImageResourceExtension).GetTypeInfo().Assembly);
        }
    }
}
