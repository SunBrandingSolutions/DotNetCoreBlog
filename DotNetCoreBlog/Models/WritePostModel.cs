using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DotNetCoreBlog.Models
{
    public class WritePostModel : IValidatableObject
    {
        [Required]
        [RegularExpression("^[a-z0-9-]{2,32}$")]
        public string UrlSlug { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(500)]
        public string Summary { get; set; }

        [Required]
        public string Body { get; set; }

        [StringLength(500)]
        public string Tags { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var service = (BlogService)validationContext.GetService(typeof(BlogService));

            if (!string.IsNullOrWhiteSpace(UrlSlug))
            {
                var slugExists = service.DuplicateSlugExistsAsync(UrlSlug).Result;
                if (slugExists)
                {
                    yield return new ValidationResult($"The URL slug '{UrlSlug}' already exists", new string[] { "UrlSlug " });
                }
            }
        }
    }
}
