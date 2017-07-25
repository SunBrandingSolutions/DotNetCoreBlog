using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DotNetCoreBlog.TagHelpers
{
    [HtmlTargetElement(Attributes = "if")]
    public class ConditionalTagHelper : TagHelper
    {
        [HtmlAttributeName("if")]
        public bool? IsVisible { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (IsVisible ?? true)
            {
                base.Process(context, output);
            }
            else
            {
                output.SuppressOutput();
            }
        }
    }
}
