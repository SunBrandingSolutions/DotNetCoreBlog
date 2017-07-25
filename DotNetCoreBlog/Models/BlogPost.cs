using System;

namespace DotNetCoreBlog.Models
{
    public class BlogPost
    {
        public int Id { get; set; }

        public string UrlSlug { get; set; }

        public string Title { get; set; }

        public string Summary { get; set; }

        public string Body { get; set; }

        public string Tags { get; set; }

        public DateTimeOffset PubDate { get; set; } = DateTimeOffset.UtcNow;
    }
}
