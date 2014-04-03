using System;
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


        public AssigmentViewModelTests()
        {
            _tagViewModelFactory(_tags[0]).Returns(_tagsViewModels[0]);
            _tagViewModelFactory(_tags[1]).Returns(_tagsViewModels[1]);

            _assigment.Tags.Returns(_tags);

            _assigmentViewModel = new AssigmentViewModel(_assigment, _tagViewModelFactory);
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

    }
}