using System;
using System.Windows;
using NSubstitute;
using ToDo.Model;
using ToDo.ViewModel;
using Xunit;

namespace ToDo.Tests.ViewModel
{
    public class AssigmentViewModelTests
    {
        private readonly AssigmentViewModel _assigmentViewModel;
        private readonly IAssigment _assigment = Substitute.For<IAssigment>();
        private readonly ITag[] _tags = {Substitute.For<ITag>(), Substitute.For<ITag>()};
        private readonly ITagViewModel[] _tagsViewModels = { Substitute.For<ITagViewModel>(), Substitute.For<ITagViewModel>() };
        private readonly Func<ITag, ITagViewModel> _tagViewModelFactory = Substitute.For<Func<ITag, ITagViewModel>>();
        private readonly ITagVisibilityService _tagVisibilityService = Substitute.For<ITagVisibilityService>();


        public AssigmentViewModelTests()
        {
            _tagVisibilityService.IsTagSelected(_tags[0]).Returns(true);
            _tagVisibilityService.IsTagSelected(_tags[1]).Returns(true);

            _tagViewModelFactory(_tags[0]).Returns(_tagsViewModels[0]);
            _tagViewModelFactory(_tags[1]).Returns(_tagsViewModels[1]);

            _assigment.Tags.Returns(_tags);

            _assigmentViewModel = CreateAssigmentViewModel();
        }

        private AssigmentViewModel CreateAssigmentViewModel()
        {
            return new AssigmentViewModel(_assigment, _tagViewModelFactory, _tagVisibilityService);
        }

        [Fact]
        public void ExposeTextFieldOfModel()
        {
            const string text = "some text";
            _assigment.Text.Returns(text);

            Assert.Equal(text, _assigmentViewModel.Text);
        }

        [Fact]
        public void ExposeTagsOfModel()
        {
            Assert.Equal(_tagsViewModels[0], _assigmentViewModel.Tags[0]);
            Assert.Equal(_tagsViewModels[1], _assigmentViewModel.Tags[1]);
        }

        [Fact]
        public void ChangeVisibilityToCollapsedIfNoneOfTagsIsSelected()
        {
            _tagVisibilityService.IsTagSelected(_tags[0]).Returns(false);
            _tagVisibilityService.IsTagSelected(_tags[1]).Returns(false);

            _tagVisibilityService.TagVisibilityChanged += Raise.Event<Action>();

            Assert.Equal(Visibility.Collapsed, _assigmentViewModel.Visibility);
        }

        [Fact]
        public void ChangeVisibilityToVisibleIfNoneOneTagsIsSelected()
        {
            _tagVisibilityService.IsTagSelected(_tags[0]).Returns(false);
            _tagVisibilityService.IsTagSelected(_tags[1]).Returns(false);

            _tagVisibilityService.TagVisibilityChanged += Raise.Event<Action>();
            _tagVisibilityService.IsTagSelected(_tags[0]).Returns(true);
            _tagVisibilityService.TagVisibilityChanged += Raise.Event<Action>();

            Assert.Equal(Visibility.Visible, _assigmentViewModel.Visibility);
        }        
        
        [Fact]
        public void VisibilityIsCollapsedIfAllTagsAreDeselectedOnCreation()
        {
            _tagVisibilityService.IsTagSelected(_tags[0]).Returns(false);
            _tagVisibilityService.IsTagSelected(_tags[1]).Returns(false);

            var assigmentViewModel = CreateAssigmentViewModel();

            Assert.Equal(Visibility.Collapsed, assigmentViewModel.Visibility);

        }

    }
}