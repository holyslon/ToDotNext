using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using ToDo.Annotations;
using ToDo.Model;

namespace ToDo.ViewModel
{
    public class AssigmentViewModel : IAssigmentViewModel, INotifyPropertyChanged
    {
        private readonly IAssigment _assigment;
        private readonly ITagVisibilityService _tagVisibilityService;
        private readonly ObservableCollection<ITagViewModel> _tags = new ObservableCollection<ITagViewModel>();
        private Visibility _visibility;

        public AssigmentViewModel(IAssigment assigment, Func<ITag, ITagViewModel> tagViewModelFactory, ITagVisibilityService tagVisibilityService)
        {
            _assigment = assigment;
            _tagVisibilityService = tagVisibilityService;
            foreach (var tagViewModel in _assigment.Tags)
            {
                _tags.Add(tagViewModelFactory(tagViewModel));
            }

            _visibility = CalculteVisibility();

            _tagVisibilityService.TagVisibilityChanged += OnTagVisibilityChanged;

            Tags = new ReadOnlyObservableCollection<ITagViewModel>(_tags);
        }

        private void OnTagVisibilityChanged()
        {
            Visibility = CalculteVisibility(); 
        }

        private Visibility CalculteVisibility()
        {
            return _assigment.Tags.Aggregate(Visibility.Collapsed,
                (visibility, tag) => _tagVisibilityService.IsTagSelected(tag) ? Visibility.Visible : visibility);
        }

        public string Text { get { return _assigment.Text; } }
        public ReadOnlyObservableCollection<ITagViewModel> Tags { get; private set; }

        public Visibility Visibility
        {
            get { return _visibility; }
            private set
            {
                if(_visibility == value) return;
                _visibility = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}