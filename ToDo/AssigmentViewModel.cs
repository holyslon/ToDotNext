namespace ToDo
{
    public class AssigmentViewModel
    {
        public AssigmentViewModel(string assigmentText)
        {
            Text = assigmentText;
        }

        public string Text { get; private set; }
    }
}