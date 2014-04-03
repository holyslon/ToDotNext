using System;
using System.Linq;
using Xunit;

namespace ToDo.Tests
{
    
    public class MainWindowViewModelTests
    {
        [Fact]
        public void CreatesNewAssigmentOnAddAssigmentCommand()
        {
            var mainWindowViewModel = new MainWindowViewModel();

            const string newAssigmentText = "some new assigment text";
            mainWindowViewModel.AssigmentText = newAssigmentText;

            mainWindowViewModel.AddAssigment.Execute(null);

            Assert.Equal(newAssigmentText, mainWindowViewModel.Assigments[0].Text);
        }

        [Fact]
        public void AddsMentionedTagsToCreatedAssigment()
        {
            var mainWindowViewModel = new MainWindowViewModel();

            const string newAssigmentText = "some new assigment text";
            var tags = new[]{"work", "home", "csharp"};

            var fullAssigmentString = newAssigmentText +" " + string.Join(" ", tags.Select(s => "#" + s));

            mainWindowViewModel.AssigmentText = fullAssigmentString;

            mainWindowViewModel.AddAssigment.Execute(null);

            Assert.Equal(newAssigmentText, mainWindowViewModel.Assigments[0].Text);
            Assert.Equal(tags[0], mainWindowViewModel.Assigments[0].Tags[0].Text);
            Assert.Equal(tags[1], mainWindowViewModel.Assigments[0].Tags[1].Text);
            Assert.Equal(tags[2], mainWindowViewModel.Assigments[0].Tags[2].Text);
        }

        [Fact]
        public void DoesNotRemoveTagFromMiddleOffString()
        {
            var mainWindowViewModel = new MainWindowViewModel();

            const string newAssigmentText = "some new assigment text";
            var tags = new[] { "work", "home", "csharp" };

            var fullAssigmentString = "#dotnext " + newAssigmentText + " " + string.Join(" ", tags.Select(s => "#" + s));

            mainWindowViewModel.AssigmentText = fullAssigmentString;

            mainWindowViewModel.AddAssigment.Execute(null);

            Assert.Equal("dotnext " + newAssigmentText, mainWindowViewModel.Assigments[0].Text);
            Assert.Equal("dotnext", mainWindowViewModel.Assigments[0].Tags[0].Text);
            Assert.Equal(tags[0], mainWindowViewModel.Assigments[0].Tags[1].Text);
            Assert.Equal(tags[1], mainWindowViewModel.Assigments[0].Tags[2].Text);
            Assert.Equal(tags[2], mainWindowViewModel.Assigments[0].Tags[3].Text);
            
        }
    }
}
