using NSubstitute;
using ToDo.Model;
using Xunit;

namespace ToDo.Tests
{
    public class ApplicationFactoryTests
    {
        [Fact]
        public void TagFunctionTakesExistingTagModelIfPosible()
        {
            const string text = "some_tag_text";
            var expectedTag = Substitute.For<ITag>();
            expectedTag.Text.Returns(text);
            var tagsCache = Substitute.For<ITagsCache>();

            tagsCache.Items.Returns(new[] {expectedTag});
            var factory = ApplicationFactory.CreateTagFactory(tagsCache);

            var actualTag = factory(text);

            Assert.Same(expectedTag, actualTag);
        }
    }
}