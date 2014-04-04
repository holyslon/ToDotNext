using System;
using System.Linq;
using System.Windows;
using ToDo.Model;
using ToDo.ViewModel;
using Xunit;

namespace ToDo.Tests
{
    
    public class AcceptanceTests
    {
        private const string SomeNewAssigmentText = "some new assigment text";
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly string[] _tags = new[]{"work", "home", "csharp"};

        public AcceptanceTests()
        {
            _mainWindowViewModel = ApplicationFactory.CreateWindowViewModel();
        }

        [Fact]
        public void CreatesNewAssigmentOnAddAssigmentCommand()
        {
            _mainWindowViewModel.AssigmentText = SomeNewAssigmentText;

            _mainWindowViewModel.AddAssigment.Execute(null);

            Assert.Equal(SomeNewAssigmentText, _mainWindowViewModel.Assigments[0].Text);
        }

        [Fact]
        public void AddsMentionedTagsToCreatedAssigment()
        {

            var fullAssigmentString = SomeNewAssigmentText + " " + string.Join(" ", _tags.Select(s => "#" + s));

            _mainWindowViewModel.AssigmentText = fullAssigmentString;

            _mainWindowViewModel.AddAssigment.Execute(null);

            Assert.Equal(SomeNewAssigmentText, _mainWindowViewModel.Assigments[0].Text);
            Assert.Equal(_tags[0], _mainWindowViewModel.Assigments[0].Tags[0].Text);
            Assert.Equal(_tags[1], _mainWindowViewModel.Assigments[0].Tags[1].Text);
            Assert.Equal(_tags[2], _mainWindowViewModel.Assigments[0].Tags[2].Text);
        }

        [Fact]
        public void DoesNotRemoveTagFromMiddleOffString()
        {


            var fullAssigmentString = "#dotnext " + SomeNewAssigmentText + " " + string.Join(" ", _tags.Select(s => "#" + s));

            _mainWindowViewModel.AssigmentText = fullAssigmentString;

            _mainWindowViewModel.AddAssigment.Execute(null);

            Assert.Equal("dotnext " + SomeNewAssigmentText, _mainWindowViewModel.Assigments[0].Text);
            Assert.Equal("dotnext", _mainWindowViewModel.Assigments[0].Tags[0].Text);
            Assert.Equal(_tags[0], _mainWindowViewModel.Assigments[0].Tags[1].Text);
            Assert.Equal(_tags[1], _mainWindowViewModel.Assigments[0].Tags[2].Text);
            Assert.Equal(_tags[2], _mainWindowViewModel.Assigments[0].Tags[3].Text);
        }

        [Fact]
        public void NewTagsAppearsinMainTagList()
        {


            var fullAssigmentString = SomeNewAssigmentText + " " + string.Join(" ", _tags.Select(s => "#" + s));

            _mainWindowViewModel.AssigmentText = fullAssigmentString;

            _mainWindowViewModel.AddAssigment.Execute(null);

            Assert.Equal(_tags[0], _mainWindowViewModel.AvalibleTags[0].Text);
            Assert.Equal(_tags[1], _mainWindowViewModel.AvalibleTags[1].Text);
            Assert.Equal(_tags[2], _mainWindowViewModel.AvalibleTags[2].Text); 
        }

        [Fact]
        public void TagsInAvalibleTagsShouldBeUnique()
        {


            var fullAssigmentString = SomeNewAssigmentText + " " + string.Join(" ", _tags.Select(s => "#" + s));

            _mainWindowViewModel.AssigmentText = fullAssigmentString;

            _mainWindowViewModel.AddAssigment.Execute(null);

            _mainWindowViewModel.AddAssigment.Execute(null);

            Assert.Equal(_tags[0], _mainWindowViewModel.AvalibleTags[0].Text);
            Assert.Equal(_tags[1], _mainWindowViewModel.AvalibleTags[1].Text);
            Assert.Equal(_tags[2], _mainWindowViewModel.AvalibleTags[2].Text);
 
            Assert.Equal(3, _mainWindowViewModel.AvalibleTags.Count);
        }

        [Fact]
        public void CanFilterByTag()
        {
            CreateAssigment(SomeNewAssigmentText, new []{"a", "b"});
            CreateAssigment(SomeNewAssigmentText, new []{"b", "c"});
            CreateAssigment(SomeNewAssigmentText, new []{"c", "d"});

            _mainWindowViewModel.AvalibleTags[1].IsSelected = true;

            Assert.Equal(Visibility.Visible, _mainWindowViewModel.Assigments[0].Visibility);
            Assert.Equal(Visibility.Visible, _mainWindowViewModel.Assigments[1].Visibility);
            Assert.Equal(Visibility.Collapsed, _mainWindowViewModel.Assigments[2].Visibility);
        }

        private void CreateAssigment(string text, string[] tags)
        {
            var fullAssigmentString = text + " " + string.Join(" ", tags.Select(s => "#" + s));

            _mainWindowViewModel.AssigmentText = fullAssigmentString;

            _mainWindowViewModel.AddAssigment.Execute(null);
        }
    }
}
