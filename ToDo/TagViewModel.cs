namespace ToDo
{
    public class TagViewModel
    {
        public TagViewModel(string text)
        {
            Text = text;
        }

        public string Text { get; private set; }
    }
}