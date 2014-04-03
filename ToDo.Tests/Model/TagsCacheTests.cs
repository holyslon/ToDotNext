using NSubstitute;
using ToDo.Model;
using Xunit;

namespace ToDo.Tests.Model
{
    public class TagsCacheTests
    {
        private readonly TagsCache _assigmentCache;

        public TagsCacheTests()
        {
            _assigmentCache = new TagsCache();
        }

        [Fact]
        public void RaisesAssigmentAddedEventOnAssigmentAdded()
        {
            ITag actualTag = null;
            _assigmentCache.TagAdded += tag => actualTag = tag;

            var expectedTag = Substitute.For<ITag>();
            _assigmentCache.Add(expectedTag);


            Assert.Same(actualTag, expectedTag);
        }

        [Fact]
        public void DoesNotAddSameTagTwice()
        {
            var expectedTag = Substitute.For<ITag>();
            _assigmentCache.Add(expectedTag);
            _assigmentCache.Add(expectedTag);

            Assert.Equal(new []{expectedTag}, _assigmentCache.Items);
        }
    }
}