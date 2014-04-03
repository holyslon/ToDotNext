using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ToDo.Model
{
    public class TagsCache : ITagsCache
    {
        private readonly List<ITag> _tags = new List<ITag>();

        public IEnumerable<ITag> Items
        {
            get { return new ReadOnlyCollection<ITag>(_tags); }
        }

        public event Action<ITag> TagAdded;

        protected virtual void OnTagAdded(ITag obj)
        {
            var handler = TagAdded;
            if (handler != null) handler(obj);
        }

        public void Add(ITag tag)
        {
            if(_tags.Contains(tag)) return;
            _tags.Add(tag);
            OnTagAdded(tag);
        }
    }
}