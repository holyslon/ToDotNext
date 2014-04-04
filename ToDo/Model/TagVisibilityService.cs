using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace ToDo.Model
{
    public class TagVisibilityService : ITagVisibilityService
    {
        private readonly HashSet<ITag> _tagList = new HashSet<ITag>();

        public void TagSelected(ITag tag)
        {
            _tagList.Add(tag);
            OnTagVisibilityChanged();
        }

        public void TagDeselected(ITag tag)
        {
            _tagList.Remove(tag);
            OnTagVisibilityChanged();
        }

        public bool IsTagSelected(ITag tag)
        {
            return _tagList.Count == 0 || _tagList.Contains(tag);
        }

        public event Action TagVisibilityChanged;

        protected virtual void OnTagVisibilityChanged()
        {
            Action handler = TagVisibilityChanged;
            if (handler != null) handler();
        }
    }
}