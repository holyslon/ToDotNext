using System;
using System.Collections.ObjectModel;
using System.Linq;
using ToDo.Model;

namespace ToDo.ViewModel
{
    public class AssigmentViewModel : IAssigmentViewModel
    {
        private readonly IAssigment _assigment;
        private readonly ObservableCollection<ITagViewModel> _tags = new ObservableCollection<ITagViewModel>();

        public AssigmentViewModel(IAssigment assigment, Func<ITag, ITagViewModel> tagViewModelFactory)
        {
            _assigment = assigment;
            foreach (var tagViewModel in _assigment.Tags)
            {
                _tags.Add(tagViewModelFactory(tagViewModel));
            }
            Tags = new ReadOnlyObservableCollection<ITagViewModel>(_tags);
        }

        public string Text { get { return _assigment.Text; } }
        public ReadOnlyObservableCollection<ITagViewModel> Tags { get; private set; }
    }
}