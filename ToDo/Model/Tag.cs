namespace ToDo.Model
{
    public class Tag : ITag
    {
        public Tag(string text)
        {
            Text = text;
        }

        public string Text { get; private set; }
    }
}