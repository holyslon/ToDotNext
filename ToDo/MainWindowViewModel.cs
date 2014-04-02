using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ToDo.Annotations;

namespace ToDo
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _assigmentText;
        private readonly ObservableCollection<AssigmentViewModel> _assigments = new ObservableCollection<AssigmentViewModel>();
        private readonly ObservableCollection<TagViewModel> _avalibleTags = new ObservableCollection<TagViewModel>();
        public string ApplicationTitle { get; private set; }

        public ReadOnlyObservableCollection<AssigmentViewModel> Assigments { get; private set; }

        public ReadOnlyObservableCollection<TagViewModel> AvalibleTags { get; private set; }

        public ICommand AddAssigment { get; private set; }

        public string AssigmentText
        {
            get { return _assigmentText; }
            set
            {
                if(_assigmentText == value) return;
                _assigmentText = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            ApplicationTitle = "ToDo";
            Assigments = new ReadOnlyObservableCollection<AssigmentViewModel>(_assigments);
            AvalibleTags = new ReadOnlyObservableCollection<TagViewModel>(_avalibleTags);

            foreach (var i in Enumerable.Range(0,10))
            {
                _assigments.Add(new AssigmentViewModel());
                _avalibleTags.Add(new TagViewModel());
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}