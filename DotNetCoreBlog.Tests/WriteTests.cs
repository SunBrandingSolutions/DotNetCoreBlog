using DotNetCoreBlog.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace DotNetCoreBlog.Tests
{
    public class WriteTests : IClassFixture<Fixture.BlogTestFixture>
    {
        private const string TargetUrl = "/Home/Write";

        private Fixture.BlogTestFixture _fixture;

        public WriteTests(Fixture.BlogTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Should_CreateBlogPost_IfParamsValid()
        {
            // arrange
            var context = _fixture.Context;
            var model = new WritePostModel
            {
                UrlSlug = "test-post",
                Title = "Test post",
                Summary = "Test post summary.",
                Body = "Test post body.",
                Tags = "test,tags"
            };
            
            // act
            await _fixture.Client.PostAsync("/Home/Write", model.ToFormContent());

            // assert
            var newPost = await context.Posts.FirstOrDefaultAsync(post => post.UrlSlug == model.UrlSlug);
            Assert.NotNull(newPost);
        }
    }
}
