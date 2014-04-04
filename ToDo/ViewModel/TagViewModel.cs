using ToDo.Model;

namespace ToDo.ViewModel
{
    public class TagViewModel : ITagViewModel
    {
        private readonly ITag _tag;

        public TagViewModel(ITag tag)
        {
            _tag = tag;
        }

        public string Text
        {
            get { return _tag.Text; }
        }

        public bool IsSelected { get; set; }
    }
}