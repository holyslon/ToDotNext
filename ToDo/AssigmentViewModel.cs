using System.Collections.ObjectModel;
using System.Linq;

namespace ToDo
{
    public class AssigmentViewModel
    {
        private readonly ObservableCollection<TagViewModel> _tags = new ObservableCollection<TagViewModel>();

        public AssigmentViewModel(string assigmentText)
        {
            var splits = assigmentText.Split(' ');

            var tags = splits.Where(s => s.StartsWith("#")).ToArray();

            var tagsToRemove = splits.Reverse().TakeWhile(s => s.StartsWith("#")).Reverse().ToArray();

            assigmentText = tagsToRemove.Aggregate(assigmentText, (current, tag) => current.Replace(tag, "")).TrimEnd();
            assigmentText = tags.Except(tagsToRemove)
                .Aggregate(assigmentText, (current, tag) => current.Replace(tag, tag.Substring(1)));
            Text = assigmentText;
            Tags = new ReadOnlyObservableCollection<TagViewModel>(_tags);
            foreach (var tag in tags)
            {
                _tags.Add(new TagViewModel(tag.Substring(1)));
            }
        }

        public string Text { get; private set; }
        public ReadOnlyObservableCollection<TagViewModel> Tags { get; private set; }
    }
}