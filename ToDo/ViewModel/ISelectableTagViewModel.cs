namespace ToDo.ViewModel
{
    public interface ISelectableTagViewModel
    {
        string Text { get; }
        bool IsSelected { get; set; }
    }
}