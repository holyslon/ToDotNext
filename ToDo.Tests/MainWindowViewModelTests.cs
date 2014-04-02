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
    }
}
