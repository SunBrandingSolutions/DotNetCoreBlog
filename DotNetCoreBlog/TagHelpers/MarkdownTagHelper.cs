using Markdig;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DotNetCoreBlog.TagHelpers
{
    [HtmlTargetElement("markdown")]
    public class MarkdownTagHelper : TagHelper
    {
        [HtmlAttributeName("text")]
        public string Text { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Content.SetHtmlContent(Markdown.ToHtml(Text));
            output.TagMode = TagMode.StartTagAndEndTag;
        }
    }
}
