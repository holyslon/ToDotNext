using System;
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
            AddAssigment = new AddAssigmentCommand(this);

            foreach (var i in Enumerable.Range(0,10))
            {
                _avalibleTags.Add(new TagViewModel("some_tag"));
            }
        }

        public class AddAssigmentCommand : ICommand
        {
            private readonly MainWindowViewModel _mainWindowViewModel;

            public AddAssigmentCommand(MainWindowViewModel mainWindowViewModel)
            {
                _mainWindowViewModel = mainWindowViewModel;
            }

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                _mainWindowViewModel._assigments.Add(new AssigmentViewModel(_mainWindowViewModel.AssigmentText));
            }

            public event EventHandler CanExecuteChanged;
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