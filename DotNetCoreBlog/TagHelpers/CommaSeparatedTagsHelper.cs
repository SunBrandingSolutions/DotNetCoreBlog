using DotNetCoreBlog.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;
using System.Linq;

namespace DotNetCoreBlog.TagHelpers
{
    [HtmlTargetElement("span")]
    public class CommaSeparatedTagsHelper : TagHelper
    {
        private readonly string _tagName;

        public CommaSeparatedTagsHelper(IOptions<AppSettings> settings)
        {
            _tagName = settings.Value.TagsHtmlTag;
        }

        [HtmlAttributeName("tags")]
        public string Tags { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (string.IsNullOrWhiteSpace(Tags))
            {
                output.SuppressOutput();
            }
            else
            {
                var tagsHtml = string.Join(" ", Tags.Split(',').Select(tag => $"<{_tagName}>{tag}</{_tagName}>"));
                output.Content.SetHtmlContent(tagsHtml);
                output.Attributes.Add("class", "tags");
                output.TagMode = TagMode.StartTagAndEndTag;
            }
        }
    }
}
