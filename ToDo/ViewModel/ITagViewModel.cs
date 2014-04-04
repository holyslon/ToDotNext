namespace ToDo.ViewModel
{
    public interface ITagViewModel
    {
        string Text { get; }
        bool IsSelected { get; set; }
    }
}