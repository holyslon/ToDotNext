using System.Collections.ObjectModel;
using System.Windows;

namespace ToDo.ViewModel
{
    public interface IAssigmentViewModel
    {
        string Text { get; }
        ReadOnlyObservableCollection<ITagViewModel> Tags { get; }
        Visibility Visibility { get; }
    }
}