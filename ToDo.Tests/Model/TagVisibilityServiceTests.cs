using NSubstitute;
using ToDo.Model;
using Xunit;

namespace ToDo.Tests.Model
{
    public class TagVisibilityServiceTests
    {
        private readonly TagVisibilityService _tagVisibilityService = new TagVisibilityService();

        [Fact]
        public void AtStartAllTagsAreVisible()
        {
            Assert.True(_tagVisibilityService.IsTagSelected(Substitute.For<ITag>()));
        }

        [Fact]
        public void AfterSelectingTagThisTagIsSelected()
        {
            var tag = Substitute.For<ITag>();
            _tagVisibilityService.TagSelected(tag);
            Assert.True(_tagVisibilityService.IsTagSelected(tag));
        }
        [Fact]
        public void AfterSelectingTagOthersTagAreNotSelected()
        {
            var tag = Substitute.For<ITag>();
            _tagVisibilityService.TagSelected(tag);
            Assert.False(_tagVisibilityService.IsTagSelected(Substitute.For<ITag>()));
        }
        [Fact]
        public void SeveralTagsCanBeSelected()
        {
            var firstTag = Substitute.For<ITag>();
            var secondTag = Substitute.For<ITag>();
            _tagVisibilityService.TagSelected(firstTag);
            _tagVisibilityService.TagSelected(secondTag);
            Assert.True(_tagVisibilityService.IsTagSelected(firstTag));
            Assert.True(_tagVisibilityService.IsTagSelected(secondTag));
        }
        [Fact]
        public void IfNoTagsSelectedThenAllSelected()
        {
            var firstTag = Substitute.For<ITag>();
            var secondTag = Substitute.For<ITag>();
            _tagVisibilityService.TagSelected(firstTag);
            _tagVisibilityService.TagSelected(secondTag);
            _tagVisibilityService.TagDeselected(secondTag);
            _tagVisibilityService.TagDeselected(firstTag);
            Assert.True(_tagVisibilityService.IsTagSelected(Substitute.For<ITag>()));
        }        
        [Fact]
        public void DeselectsTag()
        {
            var firstTag = Substitute.For<ITag>();
            var secondTag = Substitute.For<ITag>();
            _tagVisibilityService.TagSelected(firstTag);
            _tagVisibilityService.TagSelected(secondTag);
            _tagVisibilityService.TagDeselected(firstTag);
            Assert.False(_tagVisibilityService.IsTagSelected(firstTag));
        }
        [Fact]
        public void RasiseEventOnTagSelecting()
        {
            var raised = false;
            _tagVisibilityService.TagVisibilityChanged += () => raised = true;
            _tagVisibilityService.TagSelected(Substitute.For<ITag>());
            Assert.True(raised);
        }
        [Fact]
        public void RaiseEventOnTagDeselecting()
        {
            var raised = false;
            _tagVisibilityService.TagVisibilityChanged += () => raised = true;
            _tagVisibilityService.TagSelected(Substitute.For<ITag>());
            Assert.True(raised);
        }

    }
}