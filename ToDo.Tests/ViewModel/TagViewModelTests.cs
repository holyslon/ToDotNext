using NSubstitute;
using NSubstitute.Core;
using ToDo.Model;
using ToDo.ViewModel;
using Xunit;

namespace ToDo.Tests.ViewModel
{
    public class TagViewModelTests
    {
        private readonly TagViewModel _tagViewModel;
        private readonly ITag _tag = Substitute.For<ITag>();

        public TagViewModelTests()
        {
            _tagViewModel = new TagViewModel(_tag);
        }

        [Fact]
        public void ExposeTextFieldOfModel()
        {
            string text = "some text";
            _tag.Text.Returns(text);

            Assert.Equal(text, _tagViewModel.Text);
        }

    }
}