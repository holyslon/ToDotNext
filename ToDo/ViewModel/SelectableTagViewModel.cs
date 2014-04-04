using System.ComponentModel;
using System.Runtime.CompilerServices;
using ToDo.Annotations;
using ToDo.Model;

namespace ToDo.ViewModel
{
    public class SelectableTagViewModel : ISelectableTagViewModel, INotifyPropertyChanged
    {
        private readonly ITag _tag;
        private readonly ITagVisibilityService _tagVisibilityService;
        private bool _isSelected;

        public string Text
        {
            get { return _tag.Text; }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if(_isSelected == value) return;
                _isSelected = value;
                if (_isSelected)
                {
                    _tagVisibilityService.TagSelected(_tag);
                }
                else
                {
                    _tagVisibilityService.TagDeselected(_tag);
                }
                OnPropertyChanged();
            }
        }

        public SelectableTagViewModel(ITag tag, ITagVisibilityService tagVisibilityService)
        {
            _tag = tag;
            _tagVisibilityService = tagVisibilityService;
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