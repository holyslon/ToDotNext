namespace ToDo
{
    public class MainWindowViewModel
    {
        public string ApplicationTitle { get; private set; }

        public MainWindowViewModel()
        {
            ApplicationTitle = "ToDo";
        }
    }
}