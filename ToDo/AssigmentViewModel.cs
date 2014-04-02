using System.Collections.ObjectModel;

namespace ToDo
{
    public class AssigmentViewModel
    {
        private readonly ObservableCollection<TagViewModel> _tags = new ObservableCollection<TagViewModel>();

        public AssigmentViewModel(string assigmentText)
        {
            Text = assigmentText;
            Tags = new ReadOnlyObservableCollection<TagViewModel>(_tags);
        }

        public string Text { get; private set; }
        public ReadOnlyObservableCollection<TagViewModel> Tags { get; private set; }
    }
}