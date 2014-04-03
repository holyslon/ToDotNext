using System;
using System.Collections.Generic;

namespace ToDo.Model
{
    public interface ITagsCache
    {
        IEnumerable<ITag> Items { get; }
        event Action<ITag> TagAdded;
        void Add(ITag tag);
    }
}