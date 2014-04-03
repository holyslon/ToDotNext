using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ToDo.Annotations;
using ToDo.Model;

namespace ToDo.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly IAddAssigmentService _addAssigmentService;
        private readonly Func<IAssigment, IAssigmentViewModel> _assigmentViewModelFactory;
        private readonly Func<ITag, ITagViewModel> _tagViewModelFactory;
        private string _assigmentText;
        private readonly ObservableCollection<IAssigmentViewModel> _assigments = new ObservableCollection<IAssigmentViewModel>();
        private readonly ObservableCollection<ITagViewModel> _avalibleTags = new ObservableCollection<ITagViewModel>();
        public string ApplicationTitle { get; private set; }

        public ReadOnlyObservableCollection<IAssigmentViewModel> Assigments { get; private set; }

        public ReadOnlyObservableCollection<ITagViewModel> AvalibleTags { get; private set; }

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


        public MainWindowViewModel(
            IAssigmentsCache assigmentsCache, 
            ITagsCache tagsCache,
            IAddAssigmentService addAssigmentService,
            Func<IAssigment, IAssigmentViewModel> assigmentViewModelFactory,
            Func<ITag, ITagViewModel> tagViewModelFactory)
        {
            ApplicationTitle = "ToDo";
            Assigments = new ReadOnlyObservableCollection<IAssigmentViewModel>(_assigments);
            AvalibleTags = new ReadOnlyObservableCollection<ITagViewModel>(_avalibleTags);
            AddAssigment = new AddAssigmentCommand(this);
            _addAssigmentService = addAssigmentService;
            _assigmentViewModelFactory = assigmentViewModelFactory;
            _tagViewModelFactory = tagViewModelFactory;
            foreach (var assigment in assigmentsCache.Items)
            {
                _assigments.Add(assigmentViewModelFactory(assigment));
            }
            assigmentsCache.AssimentAdded += OnAssimentAdded;
            foreach (var tag in tagsCache.Items)
            {
                _avalibleTags.Add(_tagViewModelFactory(tag));
            }
            tagsCache.TagAdded += OnTagAdded;
        }

        private void OnTagAdded(ITag obj)
        {
           _avalibleTags.Add(_tagViewModelFactory(obj));
        }

        private void OnAssimentAdded(IAssigment assigment)
        {
            _assigments.Add(_assigmentViewModelFactory(assigment));
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
                _mainWindowViewModel._addAssigmentService.AddAssigment(_mainWindowViewModel.AssigmentText);
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