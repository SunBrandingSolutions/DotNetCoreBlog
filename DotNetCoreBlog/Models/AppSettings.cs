namespace DotNetCoreBlog.Models
{
    public class AppSettings
    {
        public string BlogTitle { get; set; } = "The Blog With No Name";

        public int MaxPostsOnHomepage { get; set; } = 3;

        public string TagsHtmlTag { get; set; } = "mark";
    }
}
