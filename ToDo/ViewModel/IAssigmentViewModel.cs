using System.Collections.ObjectModel;

namespace ToDo.ViewModel
{
    public interface IAssigmentViewModel
    {
        string Text { get; }
        ReadOnlyObservableCollection<ITagViewModel> Tags { get; }
    }
}