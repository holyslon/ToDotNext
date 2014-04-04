using NSubstitute;
using ToDo.Model;
using ToDo.ViewModel;
using Xunit;

namespace ToDo.Tests.ViewModel
{
    public class SelectableTagViewModelTests 
    {
        private readonly SelectableTagViewModel _tagViewModel;
        private readonly ITag _tag = Substitute.For<ITag>();
        private readonly ITagVisibilityService _tagVisibilityService= Substitute.For<ITagVisibilityService>();

        public SelectableTagViewModelTests()
        {
            _tagViewModel = new SelectableTagViewModel(_tag, _tagVisibilityService);
        }

        [Fact]
        public void ExposeTextFieldOfModel()
        {
            string text = "some text";
            _tag.Text.Returns(text);

            Assert.Equal(text, _tagViewModel.Text);
        }

        [Fact]
        public void NotifyToTagVisibilityServiceAboutSelection()
        {
            _tagViewModel.IsSelected = true;

            _tagVisibilityService.Received().TagSelected(_tag);
        }

        [Fact]
        public void NotifyToTagVisibilityServiceAboutDeselection()
        {
            _tagViewModel.IsSelected = true;

            _tagViewModel.IsSelected = false;
            _tagVisibilityService.Received().TagDeselected(_tag);            
        }
    }
}