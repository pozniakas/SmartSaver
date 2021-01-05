using System;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace ProjectName.MarkupExtensions
{
    [ContentProperty(nameof(Source))]
    public class EmbeddedImage : IMarkupExtension
    {
        public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return string.IsNullOrWhiteSpace(Source)
                ? null 
                : ImageSource.FromResource(Source, typeof(EmbeddedImage).GetTypeInfo().Assembly);
        }
    }
}